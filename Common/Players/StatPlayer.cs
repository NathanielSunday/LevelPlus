// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using LevelPlus.Common.Configs;
using LevelPlus.Common.Players.Stats;
using LevelPlus.Common.Systems;
using LevelPlus.Common.UI.SpendUI;
using LevelPlus.Content.Items;
using LevelPlus.Network.Packets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players;

public class StatPlayer : ModPlayer
{
  private PlayerConfig Config => ModContent.GetInstance<PlayerConfig>();
  public Dictionary<string, BaseStat> Stats { get; } = new();

  public LocalizedText LevelDescription =>
    Language.GetText(
        LevelPlus.Instance.LocalizationPrefix +
        "Stats.Level.Tooltip" +
        (Main.netMode == NetmodeID.MultiplayerClient
          ? "Multiplayer"
          : ""))
      .WithFormatArgs(new object[]
      {
        Level,
        Level * Config.Life,
        Level * Config.Mana,
        Points,
        ActivePlayerCount(),
        AverageLevel()
      });

  /// The amount of points available to spend
  public int Points { get; set; }

  /// The level of the current player
  public int Level => Math.Min(XpToLevel(Xp), Config.MaxLevel);

  /// The Xp of the player
  public long Xp { get; private set; }

  /// Register a stat to the player to be able to be used on the player
  public void Register(BaseStat stat)
  {
    Stats.TryAdd(stat.Id, stat);
  }

  private void Validate()
  {
    if (CommandConfig.Instance.CommandsEnabled) return;

    var spent = 0;
    var possiblePoints = Math.Min(Level, Config.MaxLevel) * Config.Points + Config.StartingPoints;

    foreach (BaseStat stat in Stats.Values) spent += stat.Value;

    while (spent > possiblePoints)
    {
      foreach (BaseStat stat in Stats.Values)
      {
        if (stat.Value <= 0) continue;
        --stat.Value;
        --spent;
      }
    }

    Points = possiblePoints - spent;
  }

  /// Add to Xp
  public void AddXp(long amount, bool command = false)
  {
    SetXp(Xp + amount);
    if (Main.dedServ) return;
    CombatText.NewText(Player.getRect(), Color.Aqua,
      Language.GetTextValue(LevelPlus.Instance.LocalizationPrefix + "Popup.XpGain", amount));
  }

  /// Set Xp
  public void SetXp(long value, bool command = false)
  {
    // Instantiate a "Level Up" procedure
    bool levelUp = XpToLevel(value) > Level && XpToLevel(value) < Config.MaxLevel && !Main.dedServ;
    Xp = value;

    if (!levelUp) return;
    Points += Config.Points;
    Player.statLife = Player.statLifeMax2;
    Player.statMana = Player.statManaMax2;

    if (command) return;
    SoundEngine.PlaySound(new SoundStyle("LevelPlus/Assets/Sounds/LevelUp"));
    CombatText.NewText(Player.getRect(), Color.GreenYellow,
      Language.GetTextValue(LevelPlus.Instance.LocalizationPrefix + "Popup.LevelUp"), true);
  }

  public void AddLevel(int amount)
  {
    Xp += LevelToXp(amount) - LevelToXp(Level);
  }

  public void SetLevel(int value)
  {
    Xp = LevelToXp(value);
  }

  /// Add to a stat <br/>
  /// Returns false when the stat was not found, or points were insufficient and not a command
  public bool AddStat(string key, int amount = 1, bool command = false)
  {
    if (!command)
    {
      if (Points == 0) return false;
      if (Points < amount) amount = Points;
    }

    if (!Stats.TryGetValue(key, out BaseStat stat)) return false;
    if (stat.Value + amount < 0)
      amount = int.MaxValue - stat.Value;
    stat.Value += amount;
    Points -= amount;
    return true;
  }

  /// Set a stat without modifying a players points <br/>
  /// Returns false when the stat was not found
  public bool SetStat(string key, int value)
  {
    if (!Stats.TryGetValue(key, out BaseStat stat)) return false;
    if (value < 0) value = int.MaxValue;
    stat.Value = value;
    return true;
  }

  /// <returns>True if stats between two players match</returns>
  /// <remarks>Is only checked against level, not xp, since exact xp amount doesnt matter in all cases</remarks>
  public bool Match(StatPlayer compare)
  {
    if (compare.Level != Level) return false;

    foreach (var stat in Stats)
    {
      if (!compare.Stats.TryGetValue(stat.Key, out BaseStat compareStat) ||
          stat.Value.Value != compareStat.Value)
        return false;
    }

    return true;
  }

  // Validate has to be called "OnEnterWorld" to get server-side configs
  public override void OnEnterWorld() => Validate();

  public override void SetStaticDefaults()
  {
    StatProviderSystem.Instance.Register(this);
  }

  public override void LoadData(TagCompound tag)
  {
    Xp = tag.GetAsLong("Xp");

    LevelPlus.Instance.Logger.DebugFormat($"Loading {Player.name}");
    LevelPlus.Instance.Logger.DebugFormat("---------------------------------");
    //StatProviderSystem.Instance.Register(Player);

    foreach (BaseStat stat in Stats.Values)
    {
      stat.LoadData(tag);
    }
  }

  public override void SaveData(TagCompound tag)
  {
    tag.Set("Xp", Xp, true);

    LevelPlus.Instance.Logger.DebugFormat($"Saving {Player.name}");
    LevelPlus.Instance.Logger.DebugFormat("---------------------------------");
    //StatProviderSystem.Instance.Register(Player);

    foreach (BaseStat stat in Stats.Values)
    {
      stat.SaveData(tag);
    }
  }

  public override void PostUpdateMiscEffects()
  {
    Player.statLifeMax2 += Level * Config.Life;
    Player.statManaMax2 += Level * Config.Mana;

    foreach (BaseStat stat in Stats.Values)
    {
      stat.ModifyPlayer(Player);
    }
  }

  public override void GetFishingLevel(Item fishingRod, Item bait, ref float fishingLevel)
  {
    foreach (BaseStat stat in Stats.Values)
    {
      stat.ModifyFishingLevel(fishingRod, bait, ref fishingLevel);
    }
  }

  public override void PostUpdateRunSpeeds()
  {
    foreach (BaseStat stat in Stats.Values)
    {
      stat.ModifyRunSpeeds(Player);
    }
  }

  public override void UpdateLifeRegen()
  {
    foreach (BaseStat stat in Stats.Values)
    {
      stat.ModifyLifeRegen(Player);
    }
  }

  public override void OnConsumeMana(Item item, int manaConsumed)
  {
    foreach (BaseStat stat in Stats.Values)
    {
      stat.ModifyOnConsumeMana(Player, item, manaConsumed);
    }
  }

  public override bool CanConsumeAmmo(Item weapon, Item ammo)
  {
    foreach (BaseStat stat in Stats.Values)
    {
      if (!stat.CanConsumeAmmo(weapon, ammo)) return false;
    }

    return true;
  }

  public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
  {
    if (mediumCoreDeath) return;

    Item respec = new Item();
    respec.SetDefaults(ModContent.ItemType<Respec>());
    itemsByMod["Terraria"].Add(respec);

    if (!Config.RandomStartingWeapon) return;

    Random rand = new Random(DateTime.Now.Millisecond);
    itemsByMod["Terraria"].Insert(0, rand.Next(9) switch
    {
      0 => new(ItemID.WoodenBoomerang),
      1 => new(ItemID.CopperBow),
      2 => new(ItemID.WandofSparking),
      3 => new(ItemID.BabyBirdStaff),
      4 => new(ItemID.Spear),
      5 => new(ItemID.WoodYoyo),
      6 => new(ItemID.FlintlockPistol),
      7 => new(ItemID.Shuriken, 200 + rand.Next(101)),
      _ => new(ItemID.CopperBroadsword)
    });

    itemsByMod["Terraria"].Add(itemsByMod["Terraria"][0].type switch
    {
      ItemID.CopperBow => new(rand.Next(3) switch
      {
        0 => ItemID.BoneArrow,
        1 => ItemID.FlamingArrow,
        _ => ItemID.WoodenArrow
      }, 100 + rand.Next(101)),
      ItemID.WandofSparking => new(ItemID.ManaCrystal),
      ItemID.FlintlockPistol => new(ItemID.MusketBall, 100 + rand.Next(101)),
      _ => null
    });
  }

  public override void OnRespawn()
  {
    if (!Config.LossEnabled) return;
    Xp = (long)Math.Max(Xp - LevelToXp(Level) * Config.LossAmount, LevelToXp(Level));
  }

  public override void CopyClientState(ModPlayer targetCopy)
  {
    var target = targetCopy as StatPlayer;

    target!.Xp = Xp;
    foreach (string key in Stats.Keys)
    {
      if (!Stats.TryGetValue(key, out BaseStat stat)) return;
      if (!target!.Stats.TryGetValue(key, out BaseStat targetStat)) return;
      targetStat.Value = stat.Value;
    }
  }

  public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
  {
    if (!newPlayer) return;

    PlayerSyncPacket packet = new();
    packet.Level = Level;

    foreach (var stat in Stats)
    {
      packet.Stats.Add(stat.Key, stat.Value.Value);
    }

    packet.Send();
  }

  public override void SendClientChanges(ModPlayer clientPlayer)
  {
    var statPlayer = clientPlayer as StatPlayer;
    if (Match(statPlayer)) return;

    PlayerSyncPacket packet = new();
    packet.Level = Level;

    foreach (var stat in Stats)
    {
      if (!statPlayer.Stats.TryGetValue(stat.Key, out BaseStat clientStat)) continue;
      if (clientStat.Value == stat.Value.Value) continue;
      packet.Stats.Add(stat.Key, stat.Value.Value);
    }

    packet.Send();
  }

  public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
  {
    if (KeybindSystem.OpenSpendUI.JustPressed)
    {
      if (Main.netMode != NetmodeID.Server)
      {
        SoundEngine.PlaySound(SoundID.MenuTick);
        SpendUISystem.Instance.Toggle();
      }
    }
  }

  public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn,
    ref AdvancedPopupRequest sonar,
    ref Vector2 sonarPosition)
  {
    var xpToAdd = 2 * (int)Math.Pow(ItemLoader.GetItem(attempt.rolledItemDrop).Item.rare, 2) + 2;
    Xp += xpToAdd;
  }

  public static List<Player> ActivePlayers() => Main.player.Where(player => player.active).ToList();

  public static int ActivePlayerCount() => ActivePlayers().Count;

  /// <returns>What level you should be at per xp</returns>
  public static int XpToLevel(long xp) => (int)MathF.Pow(xp / 100f, 5f / 11f);

  /// <returns>The amount of XP required to be at level</returns>
  public static long LevelToXp(int level) => (long)(100f * MathF.Pow(level, 11f / 5f));

  public static int AverageLevel()
  {
    var active = ActivePlayers();
    int averageLevel = (int)active.Average(player => player.GetModPlayer<StatPlayer>().Level);
    averageLevel /= active.Count;
    return averageLevel;
  }
}