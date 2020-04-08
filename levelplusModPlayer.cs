using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria;
using Terraria.UI;
using Terraria.ID;
using levelplus.UI;

namespace levelplus
{
	class levelplusModPlayer : ModPlayer
	{
 
		const double RATE = 1.5;
		const double INCREASE = 50;
		const double BASE_XP = 100;		
		const ushort BASE_POINTS = 3;

		private double currentXP;
		private double neededXP;
		private ushort level;
		private ushort pointsUnspent;
		private ushort constitution; //buff to max health, base defense
		private ushort strength; //buff to melee damage/throwing damage
		private ushort intelligence; //buff to max mana and magic damage
		private ushort charisma; //buff to summon damage (maybe shop price)
		private ushort dexterity; //buff to ranged, movement speed (maybe dodge chance)

		private StyleDimension healthLeft;
		private StyleDimension healthTop;
		private StyleDimension manaLeft;
		private StyleDimension manaTop;
		private StyleDimension xpLeft;
		private StyleDimension xpTop;

		public StyleDimension getHealthLeft()
		{
			return healthLeft;
		}

		public StyleDimension getHealthTop()
		{
			return healthTop;
		}

		public StyleDimension getManaLeft()
		{
			return manaLeft;
		}

		public StyleDimension getManaTop()
		{
			return manaTop;
		}

		public StyleDimension getXpLeft()
		{
			return xpLeft;
		}

		public StyleDimension getXpTop()
		{
			return xpTop;
		}

		public void setHealthLeft(StyleDimension _healthLeft)
		{
			healthLeft = _healthLeft;
		}

		public void setHealthTop(StyleDimension _healthTop)
		{
			healthTop = _healthTop;
		}

		public void setManaLeft(StyleDimension _manaLeft)
		{
			manaLeft = _manaLeft;
		}

		public void setManaTop(StyleDimension _manaTop)
		{
			manaTop = _manaTop;
		}

		public void setXpLeft(StyleDimension _xpLeft)
		{
			xpLeft = _xpLeft;
		}

		public void setXpTop(StyleDimension _xpTop)
		{
			xpTop = _xpTop;
		}
		
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
					default:
						break;
				}
			}
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
				constitution = 0;
				strength = 0;
				intelligence = 0;
				charisma = 0;
				dexterity = 0;
			}
			//items.Clear();
			switch (rand.Next(4))
			{
				default:
					items[0].SetDefaults(ItemID.CopperBroadsword, true);
					break;
				case 1:
					items[0].SetDefaults(ItemID.CopperBow, true);
					Item arrows = new Item();
					int arrowChance = rand.Next(20);
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
				case 2:
					items[0].SetDefaults((rand.Next(2)==0) ? ItemID.AmethystStaff : ItemID.HornetStaff, true);
					Item manaCrystal = new Item();
					manaCrystal.SetDefaults(ItemID.ManaCrystal, true);
					items.Add(manaCrystal);
					break;
				case 3:
					items[0].SetDefaults(ItemID.Shuriken, true);
					items[0].stack = 150;
					break;
			}
			
			items[1].SetDefaults(ItemID.CopperPickaxe, true);
			items[2].SetDefaults(ItemID.CopperAxe, true);
		}

		public override TagCompound Save()
		{
			TagCompound tag = new TagCompound();

			if (tag.GetBool("initialized"))
			{
				//tag.Set("initialized", true);
				tag.Set("level", level);
				tag.Set("currentXP", currentXP);
				tag.Set("neededXP", neededXP);
				tag.Set("points", pointsUnspent);
				tag.Set("con", constitution);
				tag.Set("str", strength);
				tag.Set("int", intelligence);
				tag.Set("cha", charisma);
				tag.Set("dex", dexterity);
			}
			else
			{
				tag.Add("initialized", true);
				tag.Add("level", level);
				tag.Add("currentXP", currentXP);
				tag.Add("neededXP", neededXP);
				tag.Add("points", pointsUnspent);
				tag.Add("con", constitution);
				tag.Add("str", strength);
				tag.Add("int", intelligence);
				tag.Add("cha", charisma);
				tag.Add("dex", dexterity);
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
				constitution = (ushort)tag.GetAsShort("con");
				strength = (ushort)tag.GetAsShort("str");
				intelligence = (ushort)tag.GetAsShort("int");
				charisma = (ushort)tag.GetAsShort("cha");
				dexterity = (ushort)tag.GetAsShort("dex");
			}	
		}
		public override void PostUpdateEquips()
		{
			//base.PostUpdateEquips();

			player.statLifeMax2 += (2 * level) + (2 * constitution);
			player.lifeRegen += (constitution / 20);
			player.statDefense += constitution;
			player.statManaMax2 += (1 * level) + (1 * intelligence);
			player.manaRegen += (intelligence / 20);

			player.meleeDamage *= (1f + (strength/100f));
			player.meleeCrit += (strength / 10);
			player.thrownDamage *= (1f + (strength / 100f));
			player.thrownCrit += (strength / 10);
			player.rangedDamage *= (1f + (dexterity / 100f));
			player.rangedCrit += (1 + (dexterity / 10));
			player.magicDamage *= (1f + (intelligence / 100f));
			player.magicCrit += (intelligence / 10);
			player.minionDamage *= (1f + (charisma / 100f));
			player.maxMinions += (charisma / 10);
			player.wingTimeMax += (int)(player.wingTimeMax * (charisma / 100f));

			player.maxRunSpeed += (1f + (dexterity / 100f));
			player.moveSpeed *= (1f + (dexterity / 100f));
			player.jumpSpeedBoost += (dexterity / 100f);
		}

		public void gainXP(double amount)
		{
			currentXP += amount;
			//Main.NewText("CurrentXP: " + currentXP);

			if (currentXP >= neededXP)
			{
				levelUp();
			}
		}

		private void levelUp()
		{

			Projectile proj = new Projectile();

			Main.PlaySound(SoundID.DD2_OgreGroundPound, (int)player.Center.X, (int)player.Center.Y);
			Main.PlaySound(SoundID.Meowmere, (int)player.Center.X, (int)player.Center.Y);
			Main.PlaySound(SoundID.Coins, (int)player.Center.X, (int)player.Center.Y);
			Main.PlaySound(SoundID.Tink, (int)player.Center.X, (int)player.Center.Y);
			//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Audio/levelup")); //throws index out of bounds exception

			player.statLife = player.statLifeMax2;
			player.statMana = player.statManaMax2;

			currentXP -= neededXP;
			++level;
			++pointsUnspent;
			//Main.NewText("Your new level: " + (level + 1));
			neededXP = Math.Round(INCREASE * Math.Pow(level, RATE)) + BASE_XP;
			//Main.NewText("NeededXP: " + neededXP);
		}

		public void statReset()
		{
			pointsUnspent += (ushort)(constitution + strength + intelligence + charisma + dexterity);

			constitution = 0;
			strength = 0;
			intelligence = 0;
			charisma = 0;
			dexterity = 0;
		}
	}
}
