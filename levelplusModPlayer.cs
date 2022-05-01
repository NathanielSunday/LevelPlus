using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria;
using Terraria.UI;
using Terraria.ID;
using levelplus.UI;
using Microsoft.Xna.Framework.Audio;
using Terraria.Audio;
using Steamworks;

namespace levelplus
{
	internal enum Weapon
	{
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

	class levelplusModPlayer : ModPlayer
	{
 
		const double RATE = 1.5;
		const double INCREASE = 50;
		const double BASE_XP = 100;		
		const ushort BASE_POINTS = 3;
		const ushort LEVEL_POINTS = 3;

		private Weapon weapon;

		private string talents;

		private double currentXP;
		private double neededXP;
		private ushort level;
		private ushort pointsUnspent;
		private ushort talentUnspent;


		private ushort constitution; //buff to max health, base defense
		private ushort strength; //buff to melee damage/throwing damage
		private ushort intelligence; //buff to max mana and magic damage
		private ushort charisma; //buff to summon damage (maybe shop price)
		private ushort dexterity; //buff to ranged

		private ushort mobility; //movement speed and such
		private ushort excavation; //pick speed
		private ushort animalia; //fishing power and xp gain
		private ushort grace; //jump and flight
		private ushort mysticism; //max mana and regen

		private ushort lifeCrystals;
		private ushort manaCrystals;

		public double getCurrentXP()
		{
			return currentXP;
		}

		public double getNeededXP()
		{
			return neededXP;
		}

		public ushort getLevel()
		{
			return level;
		}

		public ushort getCon()
		{
			return constitution;
		}

		public ushort getStr()
		{
			return strength;
		}

		public ushort getInt()
		{
			return intelligence;
		}

		public ushort getCha()
		{
			return charisma;
		}

		public ushort getDex()
		{
			return dexterity;
		}

		public ushort getMys()
		{
			return mysticism;
		}

		public ushort getMob()
		{
			return mobility;
		}

		public ushort getExc()
		{
			return excavation;
		}

		public ushort getAni()
		{
			return animalia;
		}

		public ushort getGra()
		{
			return grace;
		}

		public ushort getUnspentPoints()
		{
			return pointsUnspent;
		}

		public void spend(ButtonMode stat)
		{
			if (pointsUnspent > 0)
			{
				Main.PlaySound(SoundID.MenuTick, (int)player.Center.X, (int)player.Center.Y, 1, 1f);
				--pointsUnspent;
				switch (stat)
				{
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

		public override void OnEnterWorld(Player player)
		{
			base.OnEnterWorld(player);


		}

		public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
		{
			Random rand = new Random();

			if (!mediumcoreDeath)
			{
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
				lifeCrystals = 0;
				manaCrystals = 0;
			}
			
			switch (weapon)
			{
				case Weapon.SWORD:
					items[0].SetDefaults(ItemID.CopperBroadsword, true);
					break;
				case Weapon.BOOMERANG:
					items[0].SetDefaults(ItemID.WoodenBoomerang, true);
					break;
				case Weapon.BOW:
					items[0].SetDefaults(ItemID.CopperBow, true);
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
					items.Add(arrows);
					break;
				case Weapon.MAGIC:
					items[0].SetDefaults(ItemID.WandofSparking, true);
					if (!mediumcoreDeath)
					{
						Item manaCrystal = new Item();
						manaCrystal.SetDefaults(ItemID.ManaCrystal, true);
						items.Add(manaCrystal);
					}
					break;
				case Weapon.SUMMON:
					items[0].SetDefaults(ItemID.HornetStaff, true);
					break;
				case Weapon.SPEAR:
					items[0].SetDefaults(ItemID.Spear, true);
					break;
				case Weapon.YOYO:
					items[0].SetDefaults(ItemID.WoodYoyo, true);
					break;
				case Weapon.GUN:
					items[0].SetDefaults(ItemID.FlintlockPistol, true);
					break;
				case Weapon.THROWN:
					items[0].SetDefaults(ItemID.Shuriken, true);
					items[0].stack = 100 + rand.Next(101);
					break;
				default:
					break;
			}
			
			items[1].SetDefaults(ItemID.CopperPickaxe, true);
			items[2].SetDefaults(ItemID.CopperAxe, true);
		}

		public override TagCompound Save()
		{
			TagCompound tag = new TagCompound();


			//check if this character has a save tag
			if (tag.GetBool("initialized"))
			{
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
			}
			else
			{
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

		public override void Load(TagCompound tag)
		{
			if (tag.GetBool("initialized"))
			{
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
			}	
		}

		public override void OnRespawn(Player player)
		{
			base.OnRespawn(player);
			currentXP = 0;
		}

		public override void PostUpdateEquips()
		{
			//base.PostUpdateEquips();

			//COMBAT
				//constitution
				player.statLifeMax2 += (2 * level) + (5 * constitution);
				player.lifeRegen += (constitution / 25);
				player.statDefense += constitution / 3;
				
				//intelligence
				player.magicDamage *= (1f + (intelligence / 200f));
				player.magicCrit += (intelligence / 20);

				//strength
				player.meleeDamage *= (1f + (strength / 200f));
				player.meleeCrit += (strength / 15);
				player.thrownVelocity *= (1f + (dexterity / 200f));
				

				//dexterity
				player.rangedDamage *= (1f + (dexterity / 200f));
				player.rangedCrit += (1 + (dexterity / 20));
				player.thrownDamage *= (1f + (dexterity / 200f));
				player.thrownCrit += (dexterity / 20);
				
			
				//charisma
				player.minionDamage *= (1f + (charisma / 200f));
				player.maxMinions += (charisma / 30);

			//UTILITY
				//animalia
				player.fishingSkill += (int)(player.fishingSkill * (animalia / 100f));
				//xpgain 1% per point	

				//excavation
				player.pickSpeed *= (1f + (excavation / 100f));
				player.tileSpeed *= (1f + (excavation / 100f));
				player.blockRange += (int)(player.blockRange * (excavation / 100f));

				//mobility
				player.maxRunSpeed += (1f + (mobility / 100f));
				player.moveSpeed *= (1f + (mobility / 100f));

				//grace
				player.wingTimeMax += (int)(player.wingTimeMax * (grace / 100f));
				player.jump += (int)(player.jump * (grace / 100f));

				//mysticism
				player.statManaMax2 += (1 * level) + (2 * intelligence);
				player.manaRegen += (intelligence / 25);
		}

		public void gainXP(double amount)
		{
			currentXP += amount * (1 + (animalia / 100f));
			//Main.NewText("CurrentXP: " + currentXP);

			if (currentXP >= neededXP)
			{
				levelUp();
			}
		}

		private void levelUp()
		{

			if(!Main.dedServ)
				Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Audio/level"));

			//Main.PlaySound(SoundID.DD2_OgreGroundPound, (int)player.Center.X, (int)player.Center.Y);
			//Main.PlaySound(SoundID.Meowmere, (int)player.Center.X, (int)player.Center.Y);
			//Main.PlaySound(SoundID.Coins, (int)player.Center.X, (int)player.Center.Y);
			//Main.PlaySound(SoundID.Tink, (int)player.Center.X, (int)player.Center.Y);
			//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Audio/levelup")); //throws index out of bounds exception

			player.statLife = player.statLifeMax;
			player.statMana = player.statManaMax;

			currentXP -= neededXP;
			++level;
			pointsUnspent += LEVEL_POINTS;
			//Main.NewText("Your new level: " + (level + 1));
			neededXP = Math.Round(INCREASE * Math.Pow(level, RATE)) + BASE_XP;
			//Main.NewText("NeededXP: " + neededXP);
		}

		public void statReset()
		{
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
