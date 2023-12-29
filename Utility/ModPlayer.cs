using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using LevelPlus.UI;
using LevelPlus.Utility;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Localization;

namespace LevelPlus {
  internal enum Weapon {
    SWORD,//
    YOYO,//
    SUMMON,//
    SPEAR,//
    BOOMERANG,//
    MAGIC,//
    BOW,//
    GUN,
    THROWN
  }

  class LevelPlusModPlayer : ModPlayer {

    //currently private/unused variables
    private ushort talentUnspent;
    private string talents;

    public ulong currentXP { get; private set; }
    public ulong neededXP { get; private set; }
    public ushort level { get; set; }
    public ushort statPoints { get; private set; }

    public ushort constitution { get; set; } //buff to max health, base defense
    public ushort strength { get; set; } //buff to melee damage
    public ushort intelligence { get; set; } //buff to max mana and magic damage
    public ushort charisma { get; set; } //buff to summon damage (maybe shop price)
    public ushort dexterity { get; set; } //buff to ranged

    public ushort mobility { get; set; } //movement speed and such
    public ushort excavation { get; set; } //pick speed
    public ushort animalia { get; set; } //fishing power and minion extras
    public ushort luck { get; set; } //xp gain and whatnot
    public ushort mysticism { get; set; } //max mana and regen


    public void spend(Stat whichStat, ushort howMuch = 1, int givenStatPoints = -1) {
      int statPointsInt;
      if (givenStatPoints == -1) {
        statPointsInt = statPoints;
      }
      else {
        statPointsInt = givenStatPoints;
      }
      if (statPointsInt == 0)
        return;
      ushort theStat = 0;
      switch (whichStat) {
        case Stat.CONSTITUTION:
          theStat = constitution;
          break;
        case Stat.STRENGTH:
          theStat = strength;
          break;
        case Stat.INTELLIGENCE:
          theStat = intelligence;
          break;
        case Stat.CHARISMA:
          theStat = charisma;
          break;
        case Stat.DEXTERITY:
          theStat = dexterity;
          break;
        case Stat.MOBILITY:
          theStat = mobility;
          break;
        case Stat.EXCAVATION:
          theStat = excavation;
          break;
        case Stat.ANIMALIA:
          theStat = animalia;
          break;
        case Stat.LUCK:
          theStat = luck;
          break;
        case Stat.MYSTICISM:
          theStat = mysticism;
          break;
      }
      ushort canFit = (ushort)Math.Min(ushort.MaxValue - theStat, Math.Min(statPointsInt, howMuch));
      statPoints = (ushort)Math.Min(ushort.MaxValue, statPointsInt - canFit);
      switch (whichStat) {
        case Stat.CONSTITUTION:
          constitution += canFit;
          break;
        case Stat.STRENGTH:
          strength += canFit;
          break;
        case Stat.INTELLIGENCE:
          intelligence += canFit;
          break;
        case Stat.CHARISMA:
          charisma += canFit;
          break;
        case Stat.DEXTERITY:
          dexterity += canFit;
          break;
        case Stat.MOBILITY:
          mobility += canFit;
          break;
        case Stat.EXCAVATION:
          excavation += canFit;
          break;
        case Stat.ANIMALIA:
          animalia += canFit;
          break;
        case Stat.LUCK:
          luck += canFit;
          break;
        case Stat.MYSTICISM:
          mysticism += canFit;
          break;
      }
      SetLevel(level, false);
    }

    private ushort IntToUShortNoOverflow(int num) {
      return (ushort)Math.Clamp(num, 0, ushort.MaxValue);
    }

    private ushort IntToUShortNoOverflow(uint num) {
      return (ushort)Math.Clamp(num, 0, ushort.MaxValue);
    }

    public void initialize() {
      level = 0;
      currentXP = 0;
      neededXP = CalculateNeededXP(level);

      StatReset();
    }

    public void StatReset() {
      statPoints = (ushort)(level * LevelPlusConfig.Instance.PointsPerLevel + LevelPlusConfig.Instance.PointsBase);
      talents = "--------";
      talentUnspent = 0;
      constitution = 0;
      strength = 0;
      intelligence = 0;
      charisma = 0;
      dexterity = 0;
      mysticism = 0;
      mobility = 0;
      animalia = 0;
      luck = 0;
      excavation = 0;
    }

    public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath) {
      Random rand = new Random();

      if (!mediumCoreDeath) {
        initialize();

        Item respec = new Item();
        respec.SetDefaults(ModContent.ItemType<Items.Respec>());
        itemsByMod["Terraria"].Add(respec);
      }

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

    public override void SaveData(TagCompound tag) {
      tag.Set("initialized", true, true);
      tag.Set("level", level, true);
      tag.Set("currentXP", currentXP, true);
      tag.Set("points", statPoints, true);
      tag.Set("talents", talents, true);
      tag.Set("talentPoints", talentUnspent, true);
      tag.Set("con", constitution, true);
      tag.Set("str", strength, true);
      tag.Set("int", intelligence, true);
      tag.Set("cha", charisma, true);
      tag.Set("dex", dexterity, true);
      tag.Set("mob", mobility, true);
      tag.Set("exc", excavation, true);
      tag.Set("ani", animalia, true);
      tag.Set("luc", luck, true);
      tag.Set("mys", mysticism, true);

      base.SaveData(tag);
    }

    public override void LoadData(TagCompound tag) {
      if (tag.GetBool("initialized")) {
        level = (ushort)tag.GetAsShort("level");
        currentXP = (ulong)tag.GetAsLong("currentXP");
        neededXP = CalculateNeededXP(level);
        statPoints = (ushort)tag.GetAsShort("points");
        talents = tag.Get<string>("talents");
        talentUnspent = (ushort)tag.GetAsShort("talentPoints");
        constitution = (ushort)tag.GetAsShort("con");
        strength = (ushort)tag.GetAsShort("str");
        intelligence = (ushort)tag.GetAsShort("int");
        charisma = (ushort)tag.GetAsShort("cha");
        dexterity = (ushort)tag.GetAsShort("dex");
        mobility = (ushort)tag.GetAsShort("mob");
        excavation = (ushort)tag.GetAsShort("exc");
        animalia = (ushort)tag.GetAsShort("ani");
        luck = (ushort)(tag.ContainsKey("gra") ? tag.GetAsShort("gra") : tag.GetAsShort("luc"));
        mysticism = (ushort)tag.GetAsShort("mys");

        if (currentXP > neededXP) {
          LevelUp();
        }
      }
      else {
        initialize();
      }

      base.LoadData(tag);
    }

    public override void OnRespawn() {
      base.OnRespawn();
      //lose a quarter of your xp on death
      currentXP = (ulong)(currentXP * .75);
    }

    public override void ResetEffects() {
      base.ResetEffects();
      //constitution
      Player.statLifeMax2 += (LevelPlusConfig.Instance.HealthPerLevel * level) + (LevelPlusConfig.Instance.HealthPerPoint * constitution);
      Player.lifeRegen += constitution / LevelPlusConfig.Instance.HRegenPerPoint;
      Player.statDefense += constitution / LevelPlusConfig.Instance.DefensePerPoint;
      //intelligence
      Player.GetDamage(DamageClass.Magic) *= 1.00f + (intelligence * LevelPlusConfig.Instance.MagicDamagePerPoint);
      Player.GetCritChance(DamageClass.Magic) += intelligence / LevelPlusConfig.Instance.MagicCritPerPoint;
      //strength
      Player.GetDamage(DamageClass.Melee) *= 1.00f + (strength * LevelPlusConfig.Instance.MeleeDamagePerPoint);
      Player.GetCritChance(DamageClass.Melee) += strength / LevelPlusConfig.Instance.MeleeCritPerPoint;
      //dexterity
      Player.GetDamage(DamageClass.Ranged) *= 1.00f + (dexterity * LevelPlusConfig.Instance.RangedDamagePerPoint);
      Player.GetCritChance(DamageClass.Ranged) += dexterity / LevelPlusConfig.Instance.RangedCritPerPoint;
      //charisma
      Player.GetDamage(DamageClass.Summon) *= 1.00f + (charisma * LevelPlusConfig.Instance.SummonDamagePerPoint);
      Player.GetCritChance(DamageClass.Summon) += charisma / LevelPlusConfig.Instance.SummonCritPerPoint;
      //animalia
      Player.fishingSkill += (int)(Player.fishingSkill * (animalia * LevelPlusConfig.Instance.FishSkillPerPoint));
      //excavation
      Player.pickSpeed *= 1.00f - (excavation * LevelPlusConfig.Instance.PickSpeedPerPoint);
      Player.tileSpeed *= 1.00f + (excavation * LevelPlusConfig.Instance.BuildSpeedPerPoint);
      Player.wallSpeed *= 1.00f + (excavation * LevelPlusConfig.Instance.BuildSpeedPerPoint);
      Player.blockRange += excavation / LevelPlusConfig.Instance.RangePerPoint;
      //mobility
      Player.maxRunSpeed *= 1.00f + (mobility * LevelPlusConfig.Instance.RunSpeedPerPoint);
      Player.runAcceleration *= 1.00f + (mobility * LevelPlusConfig.Instance.AccelPerPoint);
      Player.wingTimeMax += (int)(Player.wingTimeMax * (mobility * LevelPlusConfig.Instance.WingPerPoint));
      //mysticism
      Player.statManaMax2 += (LevelPlusConfig.Instance.ManaPerLevel * level) + (LevelPlusConfig.Instance.ManaPerPoint * mysticism);
      Player.manaRegen += mysticism / LevelPlusConfig.Instance.ManaRegPerPoint;

    }

    public override void PostUpdateEquips() {
      base.PostUpdateEquips();
      Player.maxMinions += animalia / LevelPlusConfig.Instance.MinionPerPoint;
    }

    public override void ModifyManaCost(Item item, ref float reduce, ref float mult) {
      mult *= Math.Clamp(1.0f - (mysticism * LevelPlusConfig.Instance.ManaCostPerPoint), 0.1f, 1.0f);
      base.ModifyManaCost(item, ref reduce, ref mult);
    }

    public override bool CanConsumeAmmo(Item weapon, Item ammo) {
      Random rand = new();

      if (rand.Next(1, LevelPlusConfig.Instance.AmmoPerPoint) <= luck) {
        return false;
      }

      base.CanConsumeAmmo(weapon, ammo);
      return true;
    }

    public void AddLevel(ushort addThisToLevel) {
      SetLevel((uint)(level + addThisToLevel));
    }

    public void SetLevel(uint setLevelToThis, bool resetXp = true) {
      level = IntToUShortNoOverflow(setLevelToThis);
      uint statPointsInt = (uint)(LevelPlusConfig.Instance.PointsPerLevel * level + LevelPlusConfig.Instance.PointsBase);
      statPointsInt -= constitution;
      statPointsInt -= strength;
      statPointsInt -= intelligence;
      statPointsInt -= charisma;
      statPointsInt -= dexterity;
      statPointsInt -= mysticism;
      statPointsInt -= mobility;
      statPointsInt -= animalia;
      statPointsInt -= luck;
      statPointsInt -= excavation;
      if (LevelPlusConfig.Instance.PointsPerLevel * level + LevelPlusConfig.Instance.PointsBase < statPointsInt)
        statPointsInt = 0;
      statPoints = IntToUShortNoOverflow(statPointsInt);
      if (resetXp)
        currentXP = 0;
      neededXP = CalculateNeededXP(level);
    }

    public void AddXp(ulong amountToAdd, bool addRaw = false) {
      if (level == ushort.MaxValue) {
        currentXP = 0;
        return;
      }

      if (Main.netMode != NetmodeID.Server && ClientConfig.Instance.enablePopups) {
        CombatText.NewText(Player.getRect(), Color.Yellow, Language.GetTextValue("Mods.LevelPlus.Popups.XpGain", amountToAdd));
      }
      
      if (addRaw)
        currentXP += amountToAdd;
      else
        currentXP += (ulong)(amountToAdd * (luck * LevelPlusConfig.Instance.XPPerPoint + 1));
      if (currentXP >= neededXP) {
        LevelUp();
      }
    }

    public void SetXp(ulong amountToSetTo) {
      if (level == ushort.MaxValue) {
        currentXP = 0;
        return;
      }
      currentXP = amountToSetTo;
      if (currentXP >= neededXP) {
        LevelUp();
      }
    }

    //public void AddPoints(int amountToAdd) {
    //    statPoints += IntToUShortNoOverflow(Math.Min(ushort.MaxValue - statPoints, amountToAdd));
    //}
    //
    //public void SetPoints(int amountToSetTo) {
    //    statPoints = IntToUShortNoOverflow(amountToSetTo);
    //}

    public void InvestParticularAmount(ushort whichStat, ushort howMuch = ushort.MaxValue, int givenStatPoints = -1) {
      // The order is starting from the top and going right, around the circle.
      int statPointsInt;
      if (givenStatPoints == -1) {
        statPointsInt = statPoints;
      }
      else {
        statPointsInt = givenStatPoints;
      }
      if (statPointsInt == 0)
        return;
      switch (whichStat) {
        case 0:
          spend(Stat.CONSTITUTION, howMuch, statPointsInt);
          break;
        case 1:
          spend(Stat.MOBILITY, howMuch, statPointsInt);
          break;
        case 2:
          spend(Stat.DEXTERITY, howMuch, statPointsInt);
          break;
        case 3:
          spend(Stat.LUCK, howMuch, statPointsInt);
          break;
        case 4:
          spend(Stat.CHARISMA, howMuch, statPointsInt);
          break;
        case 5:
          spend(Stat.ANIMALIA, howMuch, statPointsInt);
          break;
        case 6:
          spend(Stat.INTELLIGENCE, howMuch, statPointsInt);
          break;
        case 7:
          spend(Stat.MYSTICISM, howMuch, statPointsInt);
          break;
        case 8:
          spend(Stat.STRENGTH, howMuch, statPointsInt);
          break;
        case 9:
          spend(Stat.EXCAVATION, howMuch, statPointsInt);
          break;
      }
    }

    public void SetInvestmentToParticularAmount(ushort whichStat, ushort howMuch = 0) {
      // The order is starting from the top and going right, around the circle.
      int statPointsInt = statPoints;
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
        statPoints = IntToUShortNoOverflow(statPointsInt);
      else if (howMuch > statPointsInt) {
        statPointsInt = howMuch;
        InvestParticularAmount(whichStat, howMuch, statPointsInt);
        return;
      }
      InvestParticularAmount(whichStat, howMuch, statPointsInt);
    }

    private void LevelUp() {
      if (level == ushort.MaxValue) {
        currentXP = 0;
        return;
      }
      currentXP -= neededXP;
      ++level;
      statPoints += (ushort)LevelPlusConfig.Instance.PointsPerLevel;

      neededXP = CalculateNeededXP(level);

      Player.statLife = Player.statLifeMax2;
      Player.statMana = Player.statManaMax2;

      if (level == ushort.MaxValue) {
        currentXP = 0;
        if (!Main.dedServ)
          SoundEngine.PlaySound(new SoundStyle("levelplus/Sounds/Custom/level"));
        return;
      }

      if (Main.netMode != NetmodeID.Server && ClientConfig.Instance.enablePopups) {
        CombatText.NewText(Player.getRect(), Color.YellowGreen, Language.GetTextValue("Mods.LevelPlus.Popups.LevelUp"), true, false);
      }
      
      //run levelup again if XP is still higher, otherwise, play the level up noise
      if (currentXP >= neededXP)
        LevelUp();
      else if (!Main.dedServ)
        SoundEngine.PlaySound(new SoundStyle("levelplus/Sounds/Custom/level"));
    }

    public ulong CalculateNeededXP(ushort level) {
      return (ulong)(LevelPlusConfig.Instance.XPIncrease * Math.Pow(level, LevelPlusConfig.Instance.XPRate) + LevelPlusConfig.Instance.XPBase);
    }

    public override void CopyClientState(ModPlayer targetCopy) {
      base.CopyClientState(targetCopy);
      LevelPlusModPlayer clone = targetCopy as LevelPlusModPlayer;

      clone.level = level;
      clone.constitution = constitution;
      clone.strength = strength;
      clone.intelligence = intelligence;
      clone.charisma = charisma;
      clone.dexterity = dexterity;
      clone.mysticism = mysticism;
      clone.mobility = mobility;
      clone.animalia = animalia;
      clone.luck = luck;
      clone.excavation = excavation;
    }

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {
      base.SyncPlayer(toWho, fromWho, newPlayer);

      ModPacket packet = Mod.GetPacket();
      packet.Write((byte)PacketType.PlayerSync);
      AddSyncToPacket(packet);
      packet.Send();
    }

    public override void SendClientChanges(ModPlayer clientPlayer) {
      base.SendClientChanges(clientPlayer);
      if (!StatsMatch(clientPlayer as LevelPlusModPlayer)) {
        ModPacket packet = Mod.GetPacket();
        packet.Write((byte)PacketType.StatsChanged);
        AddSyncToPacket(packet);
        packet.Send();
      }
    }

    public void AddSyncToPacket(ModPacket packet) {
      packet.Write((byte)Player.whoAmI);
      packet.Write(level);
      packet.Write(constitution);
      packet.Write(strength);
      packet.Write(intelligence);
      packet.Write(charisma);
      packet.Write(dexterity);
      packet.Write(mysticism);
      packet.Write(mobility);
      packet.Write(animalia);
      packet.Write(luck);
      packet.Write(excavation);
    }

    public bool StatsMatch(LevelPlusModPlayer compare) { //returns true if stats match
      if (compare.level != level ||
      compare.constitution != constitution ||
      compare.strength != strength ||
      compare.intelligence != intelligence ||
      compare.charisma != charisma ||
      compare.dexterity != dexterity ||
      compare.mysticism != mysticism ||
      compare.mobility != mobility ||
      compare.animalia != animalia ||
      compare.luck != luck ||
      compare.excavation != excavation)
        return false;
      else
        return true;
    }

    public override void ProcessTriggers(TriggersSet triggersSet) {
      base.ProcessTriggers(triggersSet);
      if (LevelPlus.SpendUIHotKey.JustPressed) {
        if (Main.netMode != NetmodeID.Server) {
          SoundEngine.PlaySound(SoundID.MenuTick);
          SpendUI.visible = !SpendUI.visible;
        }
      }
    }
  }
}
