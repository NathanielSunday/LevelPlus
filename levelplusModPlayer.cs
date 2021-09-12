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

        const double RATE = 1.5;
        const double INCREASE = 50;
        const double BASE_XP = 100;
        const ushort BASE_POINTS = 3;
        const ushort LEVEL_POINTS = 3;

        private Weapon weapon = (Weapon)new Random().Next(0, Enum.GetNames(typeof(Weapon)).Length);

        private string talents;

        private double currentXP;
        private double neededXP;
        private ushort level;
        private ushort pointsUnspent;
        private ushort talentUnspent;


        private ushort constitution; //buff to max health, base defense
        private ushort strength; //buff to melee damage
        private ushort intelligence; //buff to max mana and magic damage
        private ushort charisma; //buff to summon damage (maybe shop price)
        private ushort dexterity; //buff to ranged

        private ushort mobility; //movement speed and such
        private ushort excavation; //pick speed
        private ushort animalia; //fishing power and xp gain
        private ushort grace; //jump and flight
        private ushort mysticism; //max mana and regen

        public double getCurrentXP() {
            return currentXP;
        }

        public double getNeededXP() {
            return neededXP;
        }

        public ushort getLevel() {
            return level;
        }

        public ushort getCon() {
            return constitution;
        }

        public ushort getStr() {
            return strength;
        }

        public ushort getInt() {
            return intelligence;
        }

        public ushort getCha() {
            return charisma;
        }

        public ushort getDex() {
            return dexterity;
        }

        public ushort getMys() {
            return mysticism;
        }

        public ushort getMob() {
            return mobility;
        }

        public ushort getExc() {
            return excavation;
        }

        public ushort getAni() {
            return animalia;
        }

        public ushort getGra() {
            return grace;
        }

        public ushort getUnspentPoints() {
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
                    case ButtonMode.GRA:
                        ++grace;
                        break;
                    case ButtonMode.MYS:
                        ++mysticism;
                        break;
                    default:
                        break;
                }
            }
        }

        public override void OnEnterWorld(Player player) {
            base.OnEnterWorld(player);


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
                grace = 0;
                mysticism = 0;
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
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.HornetStaff));
                    break;
                case Weapon.SPEAR:
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.Spear));
                    break;
                case Weapon.YOYO:
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.WoodYoyo));
                    break;
                case Weapon.GUN:
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.FlintlockPistol));
                    break;
                case Weapon.THROWN:
                    itemsByMod["Terraria"].Insert(0, new Item(ItemID.Shuriken));
                    itemsByMod["Terraria"][0].stack = 100 + rand.Next(101);
                    break;
                default:
                    break;
            }
        }

        public override TagCompound Save() {
            TagCompound tag = new TagCompound();


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
                tag.Set("gra", grace);
                tag.Set("mys", mysticism);
            } else {
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
                tag.Add("gra", grace);
                tag.Add("mys", mysticism);
            }

            return tag;
        }

        public override void Load(TagCompound tag) {
            if (tag.GetBool("initialized")) {
                level = (ushort)tag.GetAsShort("level");
                currentXP = tag.GetAsDouble("currentXP");
                neededXP = tag.GetAsDouble("neededXP");
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
                grace = (ushort)tag.GetAsShort("gra");
                mysticism = (ushort)tag.GetAsShort("mys");
            } else {
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
                grace = 0;
                mysticism = 0;
            }
        }

        public override void OnRespawn(Player player) {
            base.OnRespawn(player);
            currentXP = 0;
        }

        public override void PostUpdateEquips() {
            //base.PostUpdateEquips();

            //COMBAT
            //constitution
            Player.statLifeMax2 += (2 * level) + (5 * constitution);
            Player.lifeRegen += (constitution / 25);
            Player.statDefense += constitution / 3;

            //intelligence
            Player.GetDamage(DamageClass.Magic) *= 1f + (intelligence / 200f);
            Player.GetCritChance(DamageClass.Magic) += intelligence / 20;

            //strength
            Player.GetDamage(DamageClass.Melee) *= 1f + (strength / 200f);
            Player.GetCritChance(DamageClass.Melee) += strength / 20;

            //dexterity
            Player.GetDamage(DamageClass.Ranged) *= 1f + (dexterity / 200f);
            Player.GetCritChance(DamageClass.Ranged) += 1 + (dexterity / 20);

            //charisma
            Player.GetDamage(DamageClass.Summon) *= 1f + (charisma / 200f);
            Player.GetCritChance(DamageClass.Summon) += charisma / 20;


            //UTILITY
            //animalia
            Player.fishingSkill += (int)(Player.fishingSkill * (animalia / 100f));
            Player.maxMinions += animalia / 30;


            //excavation
            Player.pickSpeed *= (1f + (excavation / 100f));
            Player.tileSpeed *= (1f + (excavation / 100f));
            Player.blockRange += (int)(Player.blockRange * (excavation / 100f));

            //mobility
            Player.maxRunSpeed *= 1f + (mobility / 100f);
            Player.moveSpeed *= 1f + (mobility / 100f);

            //grace
            Player.wingTimeMax += (int)(Player.wingTimeMax * (grace / 100f));
            Player.jump += (int)(Player.jump * (grace / 100f));

            //mysticism
            Player.statManaMax2 += (1 * level) + (2 * intelligence);
            Player.manaRegen += (intelligence / 25);
            //xpgain +1% per point
        }

        public void gainXP(double amount) {
            currentXP += amount * (1 + (mysticism / 100f));
            //Main.NewText("CurrentXP: " + currentXP);

            if (currentXP >= neededXP) {
                levelUp();
            }
        }

        public void addPoints(int points) {
            pointsUnspent = (ushort)(pointsUnspent + points);
        }

        private void levelUp() {

            if (!Main.dedServ) {
                SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, levelplus.Instance.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/level"));
            }

            Player.statLife = Player.statLifeMax;
            Player.statMana = Player.statManaMax;

            currentXP -= neededXP;
            ++level;
            pointsUnspent += LEVEL_POINTS;

            //Main.NewText("Your new level: " + (level + 1));
            neededXP = Math.Round(INCREASE * Math.Pow(level, RATE)) + BASE_XP;
            //Main.NewText("NeededXP: " + neededXP);
        }

        public void statReset() {
            pointsUnspent += (ushort)(constitution + strength + intelligence + charisma + dexterity + mysticism + mobility + animalia + grace + excavation);

            constitution = 0;
            strength = 0;
            intelligence = 0;
            charisma = 0;
            dexterity = 0;
            mysticism = 0;
            mobility = 0;
            animalia = 0;
            grace = 0;
            excavation = 0;
        }
    }
}
