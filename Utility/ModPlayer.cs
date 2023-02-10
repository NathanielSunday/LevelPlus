using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using LevelPlus.UI;
using Terraria.Audio;
using Terraria.GameInput;


namespace LevelPlus {

    class LevelPlusModPlayer : ModPlayer {

        public int Level { get; set; }
        public ulong XP { get; set; }
        public uint Points { get; set; }
        public uint[] Stats { get; set; }

        public void Spend(Utility.Stat stat, uint howMuch = 1/*, int givenStatPoints = -1*/) {
            if (Points == 0) return;
            if (Points < howMuch) howMuch = Points;
            Points -= howMuch;
            Stats[(int)stat] += howMuch;
        }

        public void StatInitialize() {
            XP = 0;
            StatReset();
        }

        public void StatReset() {
            Points = (uint)(Utility.XpToLevel(XP) * Utility.PointsPerLevel + Utility.StartingPoints);
            for (int i = 0; i < Enum.GetValues(typeof(Utility.Stat)).Length; ++i)
                Stats[i] = 0;
        }

        public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath) {
            Random rand = new Random();

            if (!mediumCoreDeath) {
                StatInitialize();

                Item respec = new Item();
                respec.SetDefaults(ModContent.ItemType<Items.Respec>());
                itemsByMod["Terraria"].Add(respec);
            }

            switch ((Utility.Weapon)new Random().Next(0, Enum.GetNames(typeof(Utility.Weapon)).Length)) {
                case Utility.Weapon.SWORD:
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.CopperBroadsword));
                    break;
                case Utility.Weapon.BOOMERANG:
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.WoodenBoomerang));
                    break;
                case Utility.Weapon.BOW:
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
                case Utility.Weapon.MAGIC:
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.WandofSparking));
                    if (!mediumCoreDeath) {
                        Item manaCrystal = new Item();
                        manaCrystal.SetDefaults(ItemID.ManaCrystal, true);
                        itemsByMod["Terraria"].Add(manaCrystal);
                    }
                    break;
                case Utility.Weapon.SUMMON:
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.BabyBirdStaff));
                    break;
                case Utility.Weapon.SPEAR:
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.Spear));
                    break;
                case Utility.Weapon.YOYO:
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.WoodYoyo));
                    break;
                case Utility.Weapon.GUN:
                    Item bullets = new(ItemID.MusketBall, 100 + rand.Next(101));
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.FlintlockPistol));
                    itemsByMod["Terraria"].Add(bullets);
                    break;
                case Utility.Weapon.THROWN:
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.Shuriken, 100 + rand.Next(101)));
                    break;
                default:
                    break;
            }
        }

        public override void SaveData(TagCompound tag) {
            tag.Set("v2", true, true);
            tag.Set("currentXP", XP, true);
            tag.Set("Stats", Stats, true);

            base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag) {
            Stats = new uint[Enum.GetValues(typeof(Utility.Stat)).Length];
            if (tag.GetBool("v2")) {
                XP = (ulong)tag.GetAsLong("XP");
                Level = Utility.XpToLevel(XP);
                int[] temp = tag.GetIntArray("Stats");
                foreach (int i in Enum.GetValues(typeof(Utility.Stat)))
                    Stats[i] = (uint)temp[i];

                if (XP > Utility.CalculateNeededXp(XP)) LevelUp();
            }
            else if (tag.GetBool("initialized")) {
                XP = (ulong)tag.GetAsLong("currentXP");
                XP += Utility.LevelToXp((ushort)tag.GetAsShort("level"));
                if (XP > Utility.CalculateNeededXp(XP)) LevelUp();
            }
            else {
                StatInitialize();
            }

            base.LoadData(tag);
        }

        public override void OnRespawn(Player player) {
            base.OnRespawn(player);
            //lose a quarter of your current xp on death
            XP -= (ulong)((XP - Utility.LevelToXp(Utility.XpToLevel(XP))) * 0.25f);
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
            //animalia
            Player.fishingSkill += (int)(Player.fishingSkill * (animalia * Utility.FishSkillPerPoint));
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

        public override void PostUpdateEquips() {
            base.PostUpdateEquips();
            //Player.maxMinions += animalia / Utility.MinionPerPoint;
        }

        public override void ModifyManaCost(Item item, ref float reduce, ref float mult) {
            //mult *= Math.Clamp(1.0f - (mysticism * Utility.ManaCostPerPoint), 0.1f, 1.0f);
            base.ModifyManaCost(item, ref reduce, ref mult);
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo) {
            Random rand = new();
            /*
            if (rand.Next(1, 100) <= Utility.AmmoPerPoint * Stats[(int)Utility.Stat.LUCK] * 100) {
                return false;
            }
            */
            base.CanConsumeAmmo(weapon, ammo);
            return true;
        }

        public void AddLevel(int level) {
            SetLevel(Utility.XpToLevel(XP) + level);
        }

        public void SetLevel(int level, bool resetXp = true) {
            if (resetXp) XP = Utility.LevelToXp(level);
            else {

            }
        }

        public void AddXp(ulong amountToAdd, bool addRaw = false) {
            if (Level == ushort.MaxValue) {
                XP = 0;
                return;
            }
            if (addRaw)
                XP += amountToAdd;
            else
                XP += (ulong)(amountToAdd * (luck * Utility.XPPerPoint + 1));
            if (XP >= NeededXP) {
                LevelUp();
            }
        }

        public void SetXp(ulong amountToSetTo) {
            if (Level == ushort.MaxValue) {
                XP = 0;
                return;
            }
            XP = amountToSetTo;
            if (XP >= NeededXP) {
                LevelUp();
            }
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
                    Spend(Utility.Stat.CONSTITUTION, howMuch, statPointsInt);
                    break;
                case 1:
                    Spend(Utility.Stat.MOBILITY, howMuch, statPointsInt);
                    break;
                case 2:
                    Spend(Utility.Stat.DEXTERITY, howMuch, statPointsInt);
                    break;
                case 3:
                    Spend(Utility.Stat.LUCK, howMuch, statPointsInt);
                    break;
                case 4:
                    Spend(Utility.Stat.CHARISMA, howMuch, statPointsInt);
                    break;
                case 5:
                    Spend(Utility.Stat.ANIMALIA, howMuch, statPointsInt);
                    break;
                case 6:
                    Spend(Utility.Stat.INTELLIGENCE, howMuch, statPointsInt);
                    break;
                case 7:
                    Spend(Utility.Stat.MYSTICISM, howMuch, statPointsInt);
                    break;
                case 8:
                    Spend(Utility.Stat.STRENGTH, howMuch, statPointsInt);
                    break;
                case 9:
                    Spend(Utility.Stat.EXCAVATION, howMuch, statPointsInt);
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

        private void LevelUp() {
            if (Level == ushort.MaxValue) {
                XP = 0;
                return;
            }
            XP -= NeededXP;
            ++Level;
            Points += (ushort)Utility.PointsPerLevel;

            NeededXP = CalculateNeededXP(Level);

            Player.statLife = Player.statLifeMax2;
            Player.statMana = Player.statManaMax2;

            //run levelup again if XP is still higher, otherwise, play the level up noise
            if (XP >= NeededXP)
                LevelUp();
            else if (!Main.dedServ)
                SoundEngine.PlaySound(new SoundStyle("levelplus/Sounds/Custom/level"));
        }

        public override void clientClone(ModPlayer clientClone) {
            base.clientClone(clientClone);
            LevelPlusModPlayer clone = clientClone as LevelPlusModPlayer;

            clone.XP = XP;
            clone.Stats = Stats;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {
            base.SyncPlayer(toWho, fromWho, newPlayer);

            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)Utility.PacketType.PlayerSync);
            Utility.AddSyncToPacket(packet, this);
            packet.Send();
        }

        public override void SendClientChanges(ModPlayer clientPlayer) {
            base.SendClientChanges(clientPlayer);
            if (!Utility.PlayerStatsMatch(this, clientPlayer as LevelPlusModPlayer)) {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)Utility.PacketType.StatsChanged);
                Utility.AddSyncToPacket(packet, this);
                packet.Send();
            }
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
