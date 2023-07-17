// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using LevelPlus.UI;
using Terraria.Audio;
using Terraria.GameInput;
using LevelPlus.Config;

namespace LevelPlus.Core
{
    class LevelPlusModPlayer : ModPlayer {
    //hard saved values
    public long XP { get; private set; }
    public int[] Stats { get; private set; }
    //runtime values
    public int Level { get; private set; }
    public int Points { get; private set; }
    public enum Stat : byte {
      ENDURANCE,
      STRENGTH,
      DEXTERITY,
      INTELLIGENCE,
      WISDOM,
      CHARISMA,
      LUCK,
      CONSTRUCTION,
      AGILITY,
    }
    public enum Weapon {
      SWORD,
      YOYO,
      SUMMON,
      SPEAR,
      BOOMERANG,
      MAGIC,
      BOW,
      GUN,
      THROWN
    }
    public void Spend( Stat stat, int howMuch = 1) {
      if (Points == 0) return;
      if (Points < howMuch) howMuch = Points;
      if (Stats[(int)stat] + howMuch > int.MaxValue)
        howMuch = int.MaxValue - Stats[(int)stat];
      Points -= howMuch;
      AddStat(stat, howMuch);
    }
    public void StatInitialize() {
      XP = 0;
      StatReset();
    }
    public void StatReset() {
      Points = XPToLevel(XP) * ServerConfig.Instance.POINTS_PER_LEVEL + STARTING_POINTS;
      for (int i = 0; i < Enum.GetValues(typeof(Stat)).Length; ++i)
        Stats[i] = 0;
    }
    public void AddStat(Stat stat, int amount) {
      Stats[(int)stat] = (int)Math.Clamp((uint)Stats[(int)stat] + (uint)amount,
        0,
        int.MaxValue);
    }
    public void SetStat(Stat stat, int value) {
      Stats[(int)stat] = value;
    }
    public void AddXP(long amount, bool addRaw = false) {
      if (!addRaw) amount *= 1 + (long)(Stats[(int)Stat.LUCK] * SystemHelper.XPPerPoint);
      XP = (long)Math.Clamp((ulong)amount + (ulong)XP,
        0,
        long.MaxValue);
      XPModified();
    }

    public void SetXP(long value) {
      XP = value;
      XPModified();
    }
    /// <summary>
    /// Internal xp/level modification.
    /// This should be called at the end of whatever modified XP.
    /// </summary>
    private void XPModified() {

      //int spent
      foreach (int stat in Stats) {

      }
      Points += SystemHelper.POINTS_PER_LEVEL;

      Player.statLife = Player.statLifeMax2;
      Player.statMana = Player.statManaMax2;

      //run levelup again if XP is still higher, otherwise, play the level up noise
      if (XP >= NeededXP)
        XPModified();
      else if (!Main.dedServ)
        SoundEngine.PlaySound(new SoundStyle("LevelPlus/Sounds/level"));
    }
    //add a Verify/Validate player to check stats, and if theyre above what should be
    //possible, subtract one from each until its in bounds, then give the
    //difference in points possible back
    private void Validate() {
      int spent = 0;
      for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i) {
        spent += Stats[i];
      }
      int possiblePoints = Math.Min(XPToLevel(XP), ServerConfig.Instance.Level_MaxLevel) * SystemHelper.POINTS_PER_LEVEL + SystemHelper.STARTING_POINTS;
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
    public static long CalculateNeededXP(long currentXp) {
      return CalculateNeededXP(XPToLevel(currentXp));
    }
    /// <returns>The amount of XP needed for next level</returns>
    public static long CalculateNeededXP(int level) {
      return LevelToXP(level + 1);
    }
    /// <returns>What level you should be at for xp</returns>
    public static int XPToLevel(long xp) {
      return (int)MathF.Pow(xp / 100, 5 / 11);
    }
    /// <returns>The amount of XP required to be at level</returns>
    public static long LevelToXP(int level) {
      return (long)(100 * MathF.Pow(level, 11 / 5));
    }
    /// <returns>True if stats between two players match</returns>
    public static bool PlayerStatsMatch(LevelPlusModPlayer player, LevelPlusModPlayer compare) {
      if (compare.XP != player.XP)
        return false;
      foreach (int i in Enum.GetValues(typeof(Stat))) {
        if (compare.Stats[i] != player.Stats[i]) return false;
      }
      return true;
    }
    public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath) {
      base.ModifyStartingInventory(itemsByMod, mediumCoreDeath);

      if (mediumCoreDeath) { return; }

      Random rand = new Random();

      StatInitialize();

      Item respec = new Item();
      respec.SetDefaults(ModContent.ItemType<Items.Respec>());
      itemsByMod["Terraria"].Add(respec);

      switch ((Weapon)new Random().Next(0, Enum.GetNames(typeof(Weapon)).Length)) {
        case Weapon.SWORD:
          itemsByMod["Terraria"].Insert(0, new Item(ItemID.CopperBroadsword));
          break;
        case Weapon.BOOMERANG:
          itemsByMod["Terraria"].Insert(0, new Item(ItemID.WoodenBoomerang));
          break;
        case Weapon.BOW:
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
        case Weapon.MAGIC:
          itemsByMod["Terraria"].Insert(0, new Item(ItemID.WandofSparking));
          if (!mediumCoreDeath) {
            Item manaCrystal = new Item();
            manaCrystal.SetDefaults(ItemID.ManaCrystal, true);
            itemsByMod["Terraria"].Add(manaCrystal);
          }
          break;
        case Weapon.SUMMON:
          itemsByMod["Terraria"].Insert(0, new Item(ItemID.BabyBirdStaff));
          break;
        case Weapon.SPEAR:
          itemsByMod["Terraria"].Insert(0, new Item(ItemID.Spear));
          break;
        case Weapon.YOYO:
          itemsByMod["Terraria"].Insert(0, new Item(ItemID.WoodYoyo));
          break;
        case Weapon.GUN:
          Item bullets = new(ItemID.MusketBall, 100 + rand.Next(101));
          itemsByMod["Terraria"].Insert(0, new Item(ItemID.FlintlockPistol));
          itemsByMod["Terraria"].Add(bullets);
          break;
        case Weapon.THROWN:
          itemsByMod["Terraria"].Insert(0, new Item(ItemID.Shuriken, 100 + rand.Next(101)));
          break;
        default:
          break;
      }
    }
    public override void OnEnterWorld(Player player) {
      base.OnEnterWorld(player);
      if (!ServerConfig.Instance.Commands_Enabled) Validate();
    }
    public override void SaveData(TagCompound tag) {
      base.SaveData(tag);
      tag.Set("1.2.0", true, true);
      tag.Set("XP", XP, true);
      tag.Set("Stats", Stats, true);
    }
    public override void LoadData(TagCompound tag) {
      base.LoadData(tag);
      Stats = new int[Enum.GetValues(typeof(Stat)).Length];
      if (tag.GetBool("1.2.0")) {
        XP = tag.GetAsLong("XP");
        int[] tagStats = tag.GetIntArray("Stats");
        long spent = 0;
        for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i) {
          Stats[i] = tagStats[i];
          spent += tagStats[i];
        }
        Level = XPToLevel(XP);
        Points = Level * POINTS_PER_LEVEL + STARTING_POINTS - (int)spent;
        if (!ServerConfig.Instance.Commands_Enabled) Validate();
      }
      else StatInitialize();
    }

    public override void OnRespawn(Player player) {
      base.OnRespawn(player);
      //lose a quarter of your current xp on death
      if (!ServerConfig.Instance.PunishmentEnabled) return;
      XP = (long)Math.Max(XP - SystemHelper.LevelToXP(SystemHelper.XPToLevel(XP)) * ServerConfig.Instance.PunishmentAmount, SystemHelper.LevelToXP(SystemHelper.XPToLevel(XP)));
    }

    public override void ResetEffects() {
      base.ResetEffects();
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
      Player.tileSpeed *= 1.00f + (excavation * Utility.BuildSpeedPerPoint);
      Player.wallSpeed *= 1.00f + (excavation * Utility.BuildSpeedPerPoint);
      Player.blockRange += excavation / Utility.RangePerPoint;
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
      base.GetFishingLevel(fishingRod, bait, ref fishingLevel);
      /*
      //animalia
      fishingLevel *= Stat[Utils.ANIMALIA] * Utils.FishSkillPerPoint;
      */
    }

    public override void PostUpdateEquips() {
      base.PostUpdateEquips();
      //Player.maxMinions += animalia / Utility.MinionPerPoint;
    }

    public override void ModifyManaCost(Item item, ref float reduce, ref float mult) {
      base.ModifyManaCost(item, ref reduce, ref mult);
      //mult *= Math.Clamp(1.0f - (mysticism * Utility.ManaCostPerPoint), 0.1f, 1.0f);
    }

    public override bool CanConsumeAmmo(Item weapon, Item ammo) {
      base.CanConsumeAmmo(weapon, ammo);
      Random rand = new();
      /*
      if (rand.Next(1, 100) <= Utility.AmmoPerPoint * Stats[(int)Utility.Stat.LUCK] * 100) {
          return false;
      }
      */
      return true;
    }



    public void InvestParticularAmount(ushort whichStat, ushort howMuch = ushort.MaxValue, int givenStatPoints = -1) {
      // The order is starting from the top and going right, around the circle.
      int statPointsInt;
      if (givenStatPoints == -1) {
        statPointsInt = Points;
      }
      else {
        statPointsInt = givenStatPoints;
      }
      if (statPointsInt == 0)
        return;
      switch (whichStat) {
        case 0:
          Spend(SystemHelper.Stat.CONSTITUTION, howMuch, statPointsInt);
          break;
        case 1:
          Spend(SystemHelper.Stat.MOBILITY, howMuch, statPointsInt);
          break;
        case 2:
          Spend(SystemHelper.Stat.DEXTERITY, howMuch, statPointsInt);
          break;
        case 3:
          Spend(SystemHelper.Stat.LUCK, howMuch, statPointsInt);
          break;
        case 4:
          Spend(SystemHelper.Stat.CHARISMA, howMuch, statPointsInt);
          break;
        case 5:
          Spend(SystemHelper.Stat.ANIMALIA, howMuch, statPointsInt);
          break;
        case 6:
          Spend(SystemHelper.Stat.INTELLIGENCE, howMuch, statPointsInt);
          break;
        case 7:
          Spend(SystemHelper.Stat.MYSTICISM, howMuch, statPointsInt);
          break;
        case 8:
          Spend(SystemHelper.Stat.STRENGTH, howMuch, statPointsInt);
          break;
        case 9:
          Spend(SystemHelper.Stat.EXCAVATION, howMuch, statPointsInt);
          break;
      }
    }

    public void SetInvestmentToParticularAmount(ushort whichStat, ushort howMuch = 0) {
      // The order is starting from the top and going right, around the circle.
      int statPointsInt = Points;
      switch (whichStat) {
        case 0:
          statPointsInt += constitution;
          constitution = 0;
          break;
        case 1:
          statPointsInt += mobility;
          mobility = 0;
          break;
        case 2:
          statPointsInt += dexterity;
          dexterity = 0;
          break;
        case 3:
          statPointsInt += luck;
          luck = 0;
          break;
        case 4:
          statPointsInt += charisma;
          charisma = 0;
          break;
        case 5:
          statPointsInt += animalia;
          animalia = 0;
          break;
        case 6:
          statPointsInt += intelligence;
          intelligence = 0;
          break;
        case 7:
          statPointsInt += mysticism;
          mysticism = 0;
          break;
        case 8:
          statPointsInt += strength;
          strength = 0;
          break;
        case 9:
          statPointsInt += excavation;
          excavation = 0;
          break;
      }
      if (howMuch == 0)
        Points = IntToUShortNoOverflow(statPointsInt);
      else if (howMuch > statPointsInt) {
        statPointsInt = howMuch;
        InvestParticularAmount(whichStat, howMuch, statPointsInt);
        return;
      }
      InvestParticularAmount(whichStat, howMuch, statPointsInt);
    }
    public override void clientClone(ModPlayer clientClone) {
      base.clientClone(clientClone);
      LevelPlusModPlayer clone = clientClone as LevelPlusModPlayer;

      clone.XP = XP;
      clone.Stats = Stats;
    }

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {
      base.SyncPlayer(toWho, fromWho, newPlayer);
      LevelPlus.Network.Packet.PlayerSyncPacket.WritePacket(this);
    }

    public override void SendClientChanges(ModPlayer clientPlayer) {
      base.SendClientChanges(clientPlayer);
      if (!SystemHelper.PlayerStatsMatch(this, clientPlayer as LevelPlusModPlayer)) {
        //in this case, I'm going to have to modify 
        LevelPlus.Network.Packet.StatsChangedPacket.WritePacket(Player.whoAmI);
      }
    }

    public override void ProcessTriggers(TriggersSet triggersSet) {
      base.ProcessTriggers(triggersSet);
      if (LevelPlus.SpendUIHotKey.JustPressed) {
        if (Main.netMode != NetmodeID.Server) {
          SoundEngine.PlaySound(SoundID.MenuTick);
          SpendUI.ToggleVisible();
        }
      }
    }
  }
}
