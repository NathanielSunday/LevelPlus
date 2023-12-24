// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using LevelPlus.Common.Systems;
using LevelPlus.Common.UI.SpendUI;
using LevelPlus.Content.Items;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players {
  public enum Stat : byte {
    ENDURANCE,
    BRAWN,
    DEFT,
    INTELLECT,
    CHARM,
    LUCK,
    ADROIT
  }

  public class LevelPlayer : ModPlayer {
    //hard saved values
    public long Xp {
      get => _xp;
      private set {
        if (XpToLevel(value) >= Level && !Main.dedServ) {
          SoundEngine.PlaySound(new SoundStyle("LevelPlus/Assets/Sounds/Level"));
          CombatText.NewText(Player.getRect(), Color.GreenYellow, Language.GetTextValue("Mods.LevelPlus.Popup.LevelUp"));
        }
        _xp = value;
        Points += ServerConfig.Instance.Level_Points;

        Player.statLife = Player.statLifeMax2;
        Player.statMana = Player.statManaMax2;
      }
    }
    private long _xp;
    public int[] Stats { get; private set; }
    //runtime values
    public int Level => XpToLevel(Xp);
    public int Points { get; private set; }

    public static int GetStatIndex(string name) {
      int index = -1;
      Stat stat;
      if (Enum.TryParse(name, out stat)) {
        index = (int)stat;
      }
      return index;
    }

    public void Spend(Stat stat, int howMuch = 1) {
      if (Points == 0) return;
      if (Points < howMuch) howMuch = Points;
      if (Stats[(int)stat] + howMuch > int.MaxValue)
        howMuch = int.MaxValue - Stats[(int)stat];
      Points -= howMuch;
      AddStat(stat, howMuch);
    }

    public void StatInitialize() {
      _xp = 0;
      StatReset();
    }

    public void StatReset() {
      Points = XpToLevel(Xp) * ServerConfig.Instance.Level_Points + ServerConfig.Instance.Level_StartingPoints;
      for (int i = 0; i < Enum.GetValues(typeof(Stat)).Length; ++i)
        Stats[i] = 0;
    }

    public void AddStat(Stat stat, int amount) {
      Stats[(int)stat] = Math.Clamp(Stats[(int)stat] + amount,
        0,
        int.MaxValue);
    }

    public void SetStat(Stat stat, int value) {
      Stats[(int)stat] = value;
    }

    public void AddXp(long amount, bool addRaw = false) {
      Xp = Math.Clamp(amount + Xp,
        0,
        long.MaxValue);
      if (!Main.dedServ)
      {
        CombatText.NewText(Player.getRect(), Color.Yellow, Language.GetTextValue("Mods.LevelPlus.Popup.XpGain", amount), true);
      }
    }

    public void SetXp(long value) {
      Xp = value;
    }

    public void AddLevel(int amount) {
    }

    public void SetLevel(int value) {

    }

    public void AddPoints(int amount) {

    }

    public void SetPoints(int value) {

    }

    /// <summary>
    /// Internal xp/level modification.
    /// This should be called at the end of whatever modified XP.
    /// </summary>

    //add a Verify/Validate player to check stats, and if theyre above what should be
    //possible, subtract one from each until its in bounds, then give the
    //difference in points possible back
    private void Validate() {
      if (ServerConfig.Instance.Commands_Enabled) return;
      return;
      //use percentages to figure this out
      int spent = 0;
      for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i) {
        spent += Stats[i];
      }
      int possiblePoints = Math.Min(Level, ServerConfig.Instance.Level_MaxLevel) * ServerConfig.Instance.Level_Points + ServerConfig.Instance.Level_StartingPoints;
      if (spent > possiblePoints) {
        for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i) {

        }
      }
      while (spent > possiblePoints) {
        for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i) {
          if (Stats[i] > 0) {
            --Stats[i];
            --spent;
          }
        }
      }
      Points = possiblePoints - spent;
    }

    /// <returns>The amount of XP needed for next level</returns>
    public static long CalculateNeededXp(long currentXp) {
      return CalculateNeededXP(XpToLevel(currentXp));
    }

    /// <returns>The amount of XP needed for next level</returns>
    public static long CalculateNeededXP(int level) {
      return LevelToXp(level + 1);
    }

    /// <returns>What level you should be at for xp</returns>
    public static int XpToLevel(long xp) {
      return (int)MathF.Pow(xp / 100, 5 / 11);
    }

    /// <returns>The amount of XP required to be at level</returns>
    public static long LevelToXp(int level) {
      return (long)(100 * MathF.Pow(level, 11 / 5));
    }

    /// <returns>True if stats between two players match</returns>
    public static bool StatsMatch(LevelPlayer player, LevelPlayer compare) {
      if (compare.Xp != player.Xp)
        return false;
      foreach (int i in Enum.GetValues(typeof(Stat))) {
        if (compare.Stats[i] != player.Stats[i]) return false;
      }
      return true;
    }

    public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath) {
      if (mediumCoreDeath) return;

      Random rand = new Random();

      Item respec = new Item();
      respec.SetDefaults(ModContent.ItemType<Respec>());
      itemsByMod["Terraria"].Add(respec);

      switch (new Random().Next(0, 9)) {
        case 0:
          itemsByMod["Terraria"].Insert(0, new Item(ItemID.CopperBroadsword));
          break;
        case 1:
          itemsByMod["Terraria"].Insert(0, new Item(ItemID.WoodenBoomerang));
          break;
        case 2:
          itemsByMod["Terraria"].Insert(0, new Item(ItemID.CopperBow));
          Item arrows = new Item();
          switch (rand.Next(3)) {
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
          itemsByMod["Terraria"].Add(arrows);
          break;
        case 3:
          itemsByMod["Terraria"].Insert(0, new Item(ItemID.WandofSparking));
          if (!mediumCoreDeath) {
            Item manaCrystal = new Item();
            manaCrystal.SetDefaults(ItemID.ManaCrystal, true);
            itemsByMod["Terraria"].Add(manaCrystal);
          }
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
          break;
      }
    }

    public override void OnEnterWorld() {
      Validate();
    }

    public override void SaveData(TagCompound tag) {
      tag.Set("XP", Xp, true);
      tag.Set("Stats", Stats, true);
    }

    public override void LoadData(TagCompound tag) {
      Stats = new int[Enum.GetValues(typeof(Stat)).Length];
      if (tag.TryGet("XP", out _xp)) {
        StatInitialize();
        return;
      }
      int[] tagStats = tag.GetIntArray("Stats");
      for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i) {
        Stats[i] = tagStats[i];
      }
      Validate();
    }

    public override void OnRespawn() {
      if (!ServerConfig.Instance.Level_LossEnabled) return;
      Xp = (long)Math.Max(Xp - LevelToXp(Level) * ServerConfig.Instance.Level_LossAmount, LevelToXp(Level));
    }

    public override void ResetEffects() {
      /*
      //constitution
      Player.statLifeMax2 += (Utility.HealthPerLevel * Level) + (Utility.HealthPerPoint * constitution);
      Player.lifeRegen += constitution / Utility.HRegenPerPoint;
      Player.statDefense += constitution / Utility.DefensePerPoint;
      //intelligence
      Player.GetDamage(DamageClass.Magic) *= 1.00f + (intelligence * Utility.MagicDamagePerPoint);
      Player.GetCritChance(DamageClass.Magic) += intelligence / Utility.MagicCritPerPoint;
      //strength
      Player.GetDamage(DamageClass.Melee) *= 1.00f + (strength * Utility.MeleeDamagePerPoint);
      Player.GetCritChance(DamageClass.Melee) += strength / Utility.MeleeCritPerPoint;
      //dexterity
      Player.GetDamage(DamageClass.Ranged) *= 1.00f + (dexterity * Utility.RangedDamagePerPoint);
      Player.GetCritChance(DamageClass.Ranged) += dexterity / Utility.RangedCritPerPoint;
      //charisma
      Player.GetDamage(DamageClass.Summon) *= 1.00f + (charisma * Utility.SummonDamagePerPoint);
      Player.GetCritChance(DamageClass.Summon) += charisma / Utility.SummonCritPerPoint;
      //excavation
      Player.pickSpeed *= 1.00f - (excavation * Utility.PickSpeedPerPoint);

      player.tileSpeed *= 1.00f + (modPlayer.StatUtility.BuildSpeedPerPoint);
      player.wallSpeed *= 1.00f + (excavation * Utility.BuildSpeedPerPoint);
      player.blockRange += excavation / Utility.RangePerPoint;

      //mobility
      Player.maxRunSpeed *= 1.00f + (mobility * Utility.RunSpeedPerPoint);
      Player.runAcceleration *= 1.00f + (mobility * Utility.AccelPerPoint);
      Player.wingTimeMax += (int)(Player.wingTimeMax * (mobility * Utility.WingPerPoint));
      //mysticism
      Player.statManaMax2 += (Utility.ManaPerLevel * Level) + (Utility.ManaPerPoint * mysticism);
      Player.manaRegen += mysticism / Utility.ManaRegPerPoint;
      */
    }

    public override void GetFishingLevel(Item fishingRod, Item bait, ref float fishingLevel) {
      /*
      //animalia
      fishingLevel *= Stat[Utils.ANIMALIA] * Utils.FishSkillPerPoint;
      */
    }

    public override void PostUpdateEquips() {
      //Player.maxMinions += animalia / Utility.MinionPerPoint;
    }

    public override void ModifyManaCost(Item item, ref float reduce, ref float mult) {
      //mult *= Math.Clamp(1.0f - (mysticism * Utility.ManaCostPerPoint), 0.1f, 1.0f);
    }

    public override bool CanConsumeAmmo(Item weapon, Item ammo) {
      Random rand = new();
      /*
      if (rand.Next(1, 100) <= Utility.AmmoPerPoint * Stats[(int)Utility.Stat.LUCK] * 100) {
          return false;
      }
      */
      return true;
    }

    public override void CopyClientState(ModPlayer targetCopy) {
      LevelPlayer clone = targetCopy as LevelPlayer;

      clone.Xp = Xp;
      clone.Stats = Stats;
    }

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {
      //LevelPlus.Network.Packet.PlayerSyncPacket.WritePacket(this);
    }

    public override void SendClientChanges(ModPlayer clientPlayer) {
      if (StatsMatch(this, clientPlayer as LevelPlayer)) return;
      //LevelPlus.Network.Packet.StatsChangedPacket.WritePacket(Player.whoAmI);
    }

    public override void ProcessTriggers(TriggersSet triggersSet) {
      if (KeybindSystem.OpenSpendUI.JustPressed) {
        if (Main.netMode != NetmodeID.Server) {
          SoundEngine.PlaySound(SoundID.MenuTick);
          SpendUISystem.Instance.Toggle();
        }
      }
    }
  }
}
