// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using LevelPlus.Common.Configs;
using LevelPlus.Common.Configs.Stats;
using LevelPlus.Common.Players.Stats;
using LevelPlus.Common.Systems;
using LevelPlus.Common.UI.SpendUI;
using LevelPlus.Content.Items;
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
  /// Use this to not play the level up sound on setting <br/>
  /// e.g. loading the Xp from the tag
  private long xp;

  private PlayerConfig Config => ModContent.GetInstance<PlayerConfig>();
  public Dictionary<string, BaseStat> Stats { get; private set; }

  /// The amount of points available to spend
  public int Points { get; set; }

  /// The Xp of the player
  public long Xp
  {
    get => xp;
    set
    {
      if (XpToLevel(value) >= XpToLevel(xp) && !Main.dedServ)
      {
        SoundEngine.PlaySound(new SoundStyle("LevelPlus/Assets/Sounds/LevelUp"));
        CombatText.NewText(Player.getRect(), Color.GreenYellow, Language.GetTextValue("Mods.LevelPlus.Popup.LevelUp"),
          true);
      }

      var xpDifferance = value - xp;

      CombatText.NewText(Player.getRect(), Color.Aqua, (int)xpDifferance);
      
      xp = value;
      Points += ModContent.GetInstance<LevelConfig>().Points;
      Player.statLife = Player.statLifeMax2;
      Player.statMana = Player.statManaMax2;
    }
  }
  
  public void RegisterStat(BaseStat stat) => Stats.Add(stat.Id, stat);

  private void Validate()
  {
    if (CommandConfig.Instance.CommandsEnabled) return;
    return;
    /*
    int spent = 0;
    for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i)
    {
      spent += Stats[i];
    }

    int possiblePoints = Math.Min(Level, ServerConfig.Instance.Level_MaxLevel) * ServerConfig.Instance.Level_Points +
      ServerConfig.Instance.Level_StartingPoints;
    if (spent > possiblePoints)
    {
      for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i)
      {
      }
    }

    while (spent > possiblePoints)
    {
      for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i)
      {
        if (Stats[i] > 0)
        {
          --Stats[i];
          --spent;
        }
      }
    }

    PointsAvailable = possiblePoints - spent;
    */
  }

  public void Add(string key, int amount = 1)
  {
    if (Points == 0) return;
    if (!Stats.TryGetValue(key, out BaseStat stat)) return;
    if (Points < amount) amount = Points;
    if (stat.Value + amount < 0)
      amount = int.MaxValue - stat.Value;
    stat.Value = amount;
    Points -= amount;
  }

  public void Set(string key, int value)
  {
    if (!Stats.TryGetValue(key, out BaseStat stat)) return;
    if (value < 0) value = int.MaxValue;
    stat.Value = value;
  }

  /// <returns>True if stats between two players match</returns>
  public bool Match(StatPlayer compare)
  {
    foreach (string key in Stats.Keys)
    {
      if (!compare.Stats.TryGetValue(key, out BaseStat compareStat) ||
          !Stats.TryGetValue(key, out BaseStat stat) ||
          stat.Value != compareStat.Value)
        return false;
    }

    return true;
  }

  public override void LoadData(TagCompound tag)
  {
    int totalPoints = 0;
    foreach (BaseStat stat in Stats.Values)
    {
      stat.LoadData(tag);
      totalPoints += stat.Value;
    }
    
    // Give the Player their left over points
  }

  public override void SaveData(TagCompound tag)
  {
    // Inject whatever my be needed by other classes here, then pass it on

    foreach (BaseStat stat in Stats.Values)
    {
      stat.SaveData(tag);
    }
  }
  
  public override void PostUpdateMiscEffects()
  {
    foreach (BaseStat stat in Stats.Values)
    {
      stat.ModifyPlayer();
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
      stat.ModifyRunSpeeds();
    }
  }

  public override void UpdateLifeRegen()
  {
    foreach (BaseStat stat in Stats.Values)
    {
      stat.ModifyLifeRegen();
    }
  }

  public override void OnConsumeMana(Item item, int manaConsumed)
  {
    foreach (BaseStat stat in Stats.Values)
    {
      stat.ModifyOnConsumeMana(item, manaConsumed);
    }
  }

  public override bool CanConsumeAmmo(Item weapon, Item ammo)
  {
    foreach (BaseStat stat in Stats.Values)
    {
      if(!stat.CanConsumeAmmo(weapon, ammo)) return false;
    }
    return true;
  }

  /// Validate has to be called "OnEnterWorld" to get server-side configs
  public override void OnEnterWorld() => Validate();

  public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
  {
    if (mediumCoreDeath) return;

    Item respec = new Item();
    respec.SetDefaults(ModContent.ItemType<Respec>());
    itemsByMod["Terraria"].Add(respec);

    if (!Config.RandomStartingWeapon) return;

    System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
    switch (rand.Next(0, 9))
    {
      case 0:
        itemsByMod["Terraria"].Insert(0, new Item(ItemID.CopperBroadsword));
        break;
      case 1:
        itemsByMod["Terraria"].Insert(0, new Item(ItemID.WoodenBoomerang));
        break;
      case 2:
        Item arrows = new Item();
        switch (rand.Next(3))
        {
          default:
            arrows.SetDefaults(ItemID.WoodenArrow, true);
            break;
          case 1:
            arrows.SetDefaults(ItemID.BoneArrow, true);
            break;
          case 2:
            arrows.SetDefaults(ItemID.FlamingArrow, true);
            break;
        }

        arrows.stack = 100 + rand.Next(101);
        itemsByMod["Terraria"].Insert(0, new Item(ItemID.CopperBow));
        itemsByMod["Terraria"].Add(arrows);
        break;
      case 3:
        Item manaCrystal = new Item();
        manaCrystal.SetDefaults(ItemID.ManaCrystal, true);
        itemsByMod["Terraria"].Insert(0, new Item(ItemID.WandofSparking));
        itemsByMod["Terraria"].Add(manaCrystal);
        break;
      case 4:
        itemsByMod["Terraria"].Insert(0, new Item(ItemID.BabyBirdStaff));
        break;
      case 5:
        itemsByMod["Terraria"].Insert(0, new Item(ItemID.Spear));
        break;
      case 6:
        itemsByMod["Terraria"].Insert(0, new Item(ItemID.WoodYoyo));
        break;
      case 7:
        Item bullets = new(ItemID.MusketBall, 100 + rand.Next(101));
        itemsByMod["Terraria"].Insert(0, new Item(ItemID.FlintlockPistol));
        itemsByMod["Terraria"].Add(bullets);
        break;
      case 8:
        itemsByMod["Terraria"].Insert(0, new Item(ItemID.Shuriken, 100 + rand.Next(101)));
        break;
    }
  }

  public override void OnRespawn()
  {
    if (!Config.LossEnabled) return;
    Stats.TryGetValue("Level", out BaseStat levelStat);
    Xp = (long)System.Math.Max(Xp - LevelToXp(levelStat.Value) * Config.LossAmount, LevelToXp(levelStat.Value));
  }

  public override void CopyClientState(ModPlayer targetCopy)
  {
    StatPlayer target = targetCopy as StatPlayer;

    foreach (string key in Stats.Keys)
    {
      Stats.TryGetValue(key, out BaseStat stat);
      target.Stats.TryGetValue(key, out BaseStat targetStat);
      targetStat.Value = stat.Value;
    }
  }

  public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
  {
    //LevelPlus.Network.Packet.PlayerSyncPacket.WritePacket(this);
  }

  public override void SendClientChanges(ModPlayer clientPlayer)
  {
    if (Match(clientPlayer as StatPlayer)) return;
    //LevelPlus.Network.Packet.StatsChangedPacket.WritePacket(Player.whoAmI);
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
  
  public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar,
    ref Vector2 sonarPosition)
  {
    var xpToAdd = 2 * (int)Math.Pow(ItemLoader.GetItem(attempt.rolledItemDrop).Item.rare, 2) + 2;
    Xp += xpToAdd;
  }

  /// <returns>The amount of XP needed for next level</returns>
  public static long CalculateNeededXp(long currentXp)
  {
    return CalculateNeededXP(XpToLevel(currentXp));
  }

  /// <returns>The amount of XP needed for next level</returns>
  public static long CalculateNeededXP(int level)
  {
    return LevelToXp(level + 1);
  }

  /// <returns>What level you should be at for xp</returns>
  public static int XpToLevel(long xp)
  {
    return (int)System.MathF.Pow(xp / 100f, 5f / 11f);
  }

  /// <returns>The amount of XP required to be at level</returns>
  public static long LevelToXp(int level)
  {
    return (long)(100 * System.MathF.Pow(level, 11f / 5f));
  }
}