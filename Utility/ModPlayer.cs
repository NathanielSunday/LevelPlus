using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using levelplus.UI;
using Terraria.Audio;

namespace levelplus {
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

    class levelplusModPlayer : ModPlayer {

        float RATE = levelplusConfig.Instance.XPRate;
        ushort INCREASE = (ushort)levelplusConfig.Instance.XPIncrease;
        ushort BASE_XP = (ushort)levelplusConfig.Instance.XPBase;
        ushort BASE_POINTS = (ushort)levelplusConfig.Instance.PointsBase;
        ushort LEVEL_POINTS = (ushort)levelplusConfig.Instance.PointsPerLevel;

        private Weapon weapon = (Weapon)new Random().Next(0, Enum.GetNames(typeof(Weapon)).Length);

        private string talents;

        private ulong currentXP;
        private ulong neededXP;
        public ushort level { get; set; }
        private ushort pointsUnspent;
        private ushort talentUnspent;


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

        public ulong GetCurrentXP() {
            return currentXP;
        }

        public ulong GetNeededXP() {
            return neededXP;
        }

        public ushort GetUnspentPoints() {
            return pointsUnspent;
        }

        public void spend(ButtonMode stat) {
            if (pointsUnspent > 0) {
                //SoundID.MenuTick
                //ModSound.PlaySound(SoundEffectInstance, (int)Player.Center.X, (int)Player.Center.Y, Terraria.ModLoader.SoundType.Custom);
                --pointsUnspent;

                switch (stat) {
                    case ButtonMode.CON:
                        ++constitution;
                        break;
                    case ButtonMode.STR:
                        ++strength;
                        break;
                    case ButtonMode.INT:
                        ++intelligence;
                        break;
                    case ButtonMode.CHA:
                        ++charisma;
                        break;
                    case ButtonMode.DEX:
                        ++dexterity;
                        break;
                    case ButtonMode.MOB:
                        ++mobility;
                        break;
                    case ButtonMode.EXC:
                        ++excavation;
                        break;
                    case ButtonMode.ANI:
                        ++animalia;
                        break;
                    case ButtonMode.LUC:
                        ++luck;
                        break;
                    case ButtonMode.MYS:
                        ++mysticism;
                        break;
                    default:
                        break;
                }
            }
        }

        public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath) {
            Random rand = new Random();

            if (!mediumCoreDeath) {
                currentXP = 0;
                neededXP = BASE_XP;
                level = 0;
                pointsUnspent = BASE_POINTS;
                talents = "--------";
                talentUnspent = 0;
                constitution = 0;
                strength = 0;
                intelligence = 0;
                charisma = 0;
                dexterity = 0;
                mobility = 0;
                excavation = 0;
                animalia = 0;
                luck = 0;
                mysticism = 0;

                Item respec = new Item();
                respec.SetDefaults(ModContent.ItemType<Items.Respec>());
                itemsByMod["Terraria"].Add(respec);
            }

            switch (weapon) {
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

            //check if this character has a save tag
            if (tag.GetBool("initialized")) {
                tag.Set("level", level);
                tag.Set("currentXP", currentXP);
                tag.Set("neededXP", neededXP);
                tag.Set("points", pointsUnspent);
                tag.Set("talents", talents);
                tag.Set("talentPoints", talentUnspent);
                tag.Set("con", constitution);
                tag.Set("str", strength);
                tag.Set("int", intelligence);
                tag.Set("cha", charisma);
                tag.Set("dex", dexterity);
                tag.Set("mob", mobility);
                tag.Set("exc", excavation);
                tag.Set("ani", animalia);
                tag.Set("luc", luck);
                tag.Set("mys", mysticism);
            }
            else {
                tag.Add("initialized", true);
                tag.Add("level", level);
                tag.Add("currentXP", currentXP);
                tag.Add("neededXP", neededXP);
                tag.Add("points", pointsUnspent);
                tag.Add("talents", talents);
                tag.Add("talentPoints", talentUnspent);
                tag.Add("con", constitution);
                tag.Add("str", strength);
                tag.Add("int", intelligence);
                tag.Add("cha", charisma);
                tag.Add("dex", dexterity);
                tag.Add("mob", mobility);
                tag.Add("exc", excavation);
                tag.Add("ani", animalia);
                tag.Add("luc", luck);
                tag.Add("mys", mysticism);
            }

            base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag) {
            if (tag.GetBool("initialized")) {
                level = (ushort)tag.GetAsShort("level");
                currentXP = (ulong)tag.GetAsLong("currentXP");
                neededXP = (ulong)tag.GetAsLong("neededXP");
                pointsUnspent = (ushort)tag.GetAsShort("points");
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
            }
            else {
                currentXP = 0;
                neededXP = BASE_XP;
                level = 0;
                pointsUnspent = BASE_POINTS;
                talents = "--------";
                talentUnspent = 0;
                constitution = 0;
                strength = 0;
                intelligence = 0;
                charisma = 0;
                dexterity = 0;
                mobility = 0;
                excavation = 0;
                animalia = 0;
                luck = 0;
                mysticism = 0;
            }

            if (currentXP > neededXP) {
                LevelUp();
            }

            base.LoadData(tag);
        }

        public override void OnRespawn(Player player) {
            base.OnRespawn(player);
            //lose a quarter of your xp on death
            currentXP = (ulong)(currentXP * .75);
        }

        public override void ResetEffects() {
            base.ResetEffects();

            //COMBAT

            //constitution
            //+2 life per level
            //+5 life per point
            //+1 defense per 3 points
            Player.statLifeMax2 += (levelplusConfig.Instance.HealthPerLevel * level) + (levelplusConfig.Instance.HealthPerPoint * constitution);
            Player.lifeRegen += constitution / levelplusConfig.Instance.HRegenPerPoint;
            Player.statDefense += constitution / levelplusConfig.Instance.DefensePerPoint;

            //intelligence
            //+1% damage per point
            //+1% crit chance per 15 points
            Player.GetDamage(DamageClass.Magic) *= 1.00f + (intelligence * levelplusConfig.Instance.MagicDamagePerPoint);
            Player.GetCritChance(DamageClass.Magic) += intelligence / levelplusConfig.Instance.MagicCritPerPoint;

            //strength
            //+1% damage per point
            //+1% crit chance per 15 points
            Player.GetDamage(DamageClass.Melee) *= 1.00f + (strength * levelplusConfig.Instance.MeleeDamagePerPoint);
            Player.GetCritChance(DamageClass.Melee) += strength / levelplusConfig.Instance.MeleeCritPerPoint;

            //dexterity
            //+1% damage per point
            //+1% crit chance per 15 points
            Player.GetDamage(DamageClass.Ranged) *= 1.00f + (dexterity * levelplusConfig.Instance.RangedDamagePerPoint);
            Player.GetCritChance(DamageClass.Ranged) += dexterity / levelplusConfig.Instance.RangedCritPerPoint;

            //charisma
            //+1% damage per point
            //+1% crit chance per 15 points
            Player.GetDamage(DamageClass.Summon) *= 1.00f + (charisma * levelplusConfig.Instance.SummonDamagePerPoint);
            Player.GetCritChance(DamageClass.Summon) += charisma / levelplusConfig.Instance.SummonCritPerPoint;


            //UTILITY

            //animalia
            //+2% fishing skill per point
            //+1 minion per 20 points
            //+2% minion kb per point
            Player.fishingSkill += (int)(Player.fishingSkill * (animalia * levelplusConfig.Instance.FishSkillPerPoint));
            Player.maxMinions += animalia / levelplusConfig.Instance.MinionPerPoint;
            Player.minionKB *= 1.00f * (animalia * levelplusConfig.Instance.MinionKnockBack);


            //excavation
            //+1% pick speed per point
            //+2% place speed per point
            //+1 place reach per 10 points
            Player.pickSpeed *= 1.00f - (excavation * levelplusConfig.Instance.PickSpeedPerPoint);
            Player.tileSpeed *= 1.00f + (excavation * levelplusConfig.Instance.BuildSpeedPerPoint);
            Player.wallSpeed *= 1.00f + (excavation * levelplusConfig.Instance.BuildSpeedPerPoint);
            Player.blockRange += excavation / levelplusConfig.Instance.RangePerPoint;

            //mobility
            //+1% max run speed per point
            //+2% move speed per point
            //+2% max flight time per point
            Player.maxRunSpeed *= 1.00f + (mobility * levelplusConfig.Instance.RunSpeedPerPoint);
            Player.runAcceleration *= 1.00f + (mobility * levelplusConfig.Instance.AccelPerPoint);
            Player.wingTimeMax += (int)(Player.wingTimeMax * (mobility * levelplusConfig.Instance.WingPerPoint));

            //luck
            //+1% xp per point
            //1% chance not to consume ammo


            //mysticism
            //+1 max mana per level
            //+2 max mana per point 
            //+1 mana regen per 15 points
            //-0.5% mana cost per point
            Player.statManaMax2 += (levelplusConfig.Instance.ManaPerLevel * level) + (levelplusConfig.Instance.ManaPerPoint * mysticism);
            Player.manaRegen += mysticism / levelplusConfig.Instance.ManaRegPerPoint;

        }

        public override void ModifyManaCost(Item item, ref float reduce, ref float mult) {
            mult *= Math.Clamp(1.00f - (mysticism * levelplusConfig.Instance.ManaCostPerPoint), 0.00f, 1.00f);
            base.ModifyManaCost(item, ref reduce, ref mult);
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo) {
            Random rand = new();

            if (rand.Next(1, levelplusConfig.Instance.AmmoPerPoint) <= luck) {
                return false;
            }

            base.CanConsumeAmmo(weapon, ammo);
            return true;
        }


        public void AddLevel(ushort level) {
            pointsUnspent += (ushort)(LEVEL_POINTS * (level - this.level));
            this.level += level;
            currentXP = 0;
            neededXP = (ulong)(INCREASE * Math.Pow(level, RATE)) + BASE_XP;
        }

        public void AddXp(ulong amount) {
            currentXP += (ulong)(amount * (1 + (luck * levelplusConfig.Instance.XPPerPoint)));
            if (currentXP >= neededXP) {
                LevelUp();
            }
        }

        public void AddPoints(int points) {
            pointsUnspent = (ushort)(pointsUnspent + points);
        }

        private void LevelUp() {

            Player.statLife = Player.statLifeMax2;
            Player.statMana = Player.statManaMax2;

            currentXP -= neededXP;
            ++level;
            pointsUnspent += LEVEL_POINTS;

            neededXP = (ulong)(INCREASE * Math.Pow(level, RATE)) + BASE_XP;


            //run levelup again if XP is still higher, otherwise, play the level up noise
            if (currentXP >= neededXP)
                LevelUp();
            else if (!Main.dedServ) {
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/level"));
            }
        }

        public void StatReset() {
            pointsUnspent = (ushort)(level * LEVEL_POINTS + BASE_POINTS);

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

        public override void clientClone(ModPlayer clientClone) {
            base.clientClone(clientClone);
            levelplusModPlayer clone = clientClone as levelplusModPlayer;

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
            if (!StatsMatch(clientPlayer as levelplusModPlayer)) {
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

        public bool StatsMatch(levelplusModPlayer compare) { //returns true if stats match
            if(compare.level != level ||
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
    }
}
