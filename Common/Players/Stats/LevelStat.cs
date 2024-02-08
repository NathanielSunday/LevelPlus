// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.Configs.Stats;
using LevelPlus.Common.Systems;
using LevelPlus.Common.UI.SpendUI;
using LevelPlus.Content.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class LevelStat : StatPlayer
{
  private static LevelConfig Config => ModContent.GetInstance<LevelConfig>();

  private long xp;

  public long Xp
  {
    get => xp;
    private set
    {
      if (XpToLevel(value) >= Value && !Main.dedServ)
      {
        SoundEngine.PlaySound(new SoundStyle("LevelPlus/Assets/Sounds/LevelUp"));
        CombatText.NewText(Player.getRect(), Color.GreenYellow, Language.GetTextValue("Mods.LevelPlus.Popup.LevelUp"),
          true);
      }

      xp = value;
      base.Add(Config.Points);

      Player.statLife = Player.statLifeMax2;
      Player.statMana = Player.statManaMax2;
    }
  }

  public override int Value => XpToLevel(xp);
  protected override string Id => "Level";
  protected override object[] DescriptionArgs => new object[] { Value / 2, Value / 2 };

  public void StatInitialize()
  {
    xp = 0;
    StatReset();
  }

  public void StatReset()
  {
    PointsAvailable = XpToLevel(Xp) * ServerConfig.Instance.Level_Points + ServerConfig.Instance.Level_StartingPoints;
    for (int i = 0; i < Enum.GetValues(typeof(Stat)).Length; ++i)
      Stats[i] = 0;
  }

  public void AddStat(Stat stat, int amount)
  {
    Stats[(int)stat] = Math.Clamp(Stats[(int)stat] + amount,
      0,
      int.MaxValue);
  }

  public void SetStat(Stat stat, int value)
  {
    Stats[(int)stat] = value;
  }

  public void AddXp(long amount, bool addRaw = false)
  {
    Xp = Math.Clamp(amount + Xp,
      0,
      long.MaxValue);
    if (!Main.dedServ)
    {
      CombatText.NewText(Player.getRect(), Color.Yellow, Language.GetTextValue("Mods.LevelPlus.Popup.XpGain", amount));
    }
  }

  public void SetXp(long value)
  {
    Xp = value;
  }

  public void AddLevel(int amount)
  {
  }

  public void SetLevel(int value)
  {
  }

  public void AddPoints(int amount)
  {
  }

  public void SetPoints(int value)
  {
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
  
  protected override void OnLoadData(TagCompound tag)
  {
    throw new System.NotImplementedException();
  }

  protected override void OnSaveData(TagCompound tag)
  {
    throw new System.NotImplementedException();
  }

  /// <returns>True if stats between two players match</returns>
  public override bool Match(StatPlayer compare)
  {
    base.Match(compare);
    return (compare as LevelStat)?.Xp == Xp;
  }

  public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
  {
    if (mediumCoreDeath) return;

    Item respec = new Item();
    respec.SetDefaults(ModContent.ItemType<Respec>());
    itemsByMod["Terraria"].Add(respec);

    if(!Config.RandomStartingWeapon) return;
    
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
      default:
        throw new System.NotImplementedException("Case not found for player starting item.");
    }
  }

  public override void SaveData(TagCompound tag)
  {
    tag.Set("XP", Xp, true);
    tag.Set("Stats", Stats, true);
  }

  public override void LoadData(TagCompound tag)
  {
    Stats = new int[Enum.GetValues(typeof(Stat)).Length];
    if (tag.TryGet("XP", out xp))
    {
      StatInitialize();
      return;
    }

    int[] tagStats = tag.GetIntArray("Stats");
    for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i)
    {
      Stats[i] = tagStats[i];
    }
    //Validate();
  }

  public override void OnRespawn()
  {
    if (!ServerConfig.Instance.Level_LossEnabled) return;
    Xp = (long)Math.Max(Xp - LevelToXp(Level) * ServerConfig.Instance.Level_LossAmount, LevelToXp(Level));
  }

  public override void PostUpdateMiscEffects()
  {
    Player.statLifeMax2 += Level * Config.Level_Life;
    Player.statManaMax2 += Level * Config.Level_Mana;
  }

  public override void CopyClientState(ModPlayer targetCopy)
  {
    LevelStat clone = targetCopy as LevelStat;

    clone.Xp = Xp;
    clone.Stats = Stats;
  }

  public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
  {
    //LevelPlus.Network.Packet.PlayerSyncPacket.WritePacket(this);
  }

  public override void SendClientChanges(ModPlayer clientPlayer)
  {
    if (StatsMatch(this, clientPlayer as LevelStat)) return;
    //LevelPlus.Network.Packet.StatsChangedPacket.WritePacket(Player.whoAmI);
  }

  public override void ProcessTriggers(TriggersSet triggersSet)
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
}

