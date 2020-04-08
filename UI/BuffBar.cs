using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace levelplus.UI
{
	class BuffBar : UIElement
	{
		private static readonly MethodInfo DrawBuffIcon = typeof(Main).GetMethod("DrawBuffIcon", BindingFlags.NonPublic | BindingFlags.Static);

		//redraw the buffs
		//DISCLAIMER: I used a lot of kRPGs source for understanding how to do this, because I could not personally figure out what to do when I realized the buffs were gone

		public void DrawBuffs()
		{
			const int iconWidth = 38;
			const int maxSlots = 21;
			const int leftOffset = 35;

			int buffTypeId = -1;
			const int secondRow= 11;

			for (int buffSlot = 0; buffSlot <= maxSlots; buffSlot++)

				if (Main.player[Main.myPlayer].buffType[buffSlot] > 0)
				{
					int buff = Main.player[Main.myPlayer].buffType[buffSlot];
					int xPosition = leftOffset + buffSlot * iconWidth;

					int yPosition = 80;

					if (buffSlot >= secondRow)
					{
						xPosition = 32 + (buffSlot - secondRow) * iconWidth;
						yPosition += 50;
					}	

					buffTypeId = (int)DrawBuffIcon.Invoke(null, new object[] { buffTypeId, buffSlot, buff, xPosition, yPosition });
				}
				else
				{
					Main.buffAlpha[buffSlot] = 0.4f;
				}

			if (buffTypeId < 0)
				return;

			int buffId = Main.player[Main.myPlayer].buffType[buffTypeId];

			if (buffId <= 0)
				return;

			Main.buffString = Lang.GetBuffDescription(buffId);

			int itemRarity = 0;

			switch (buffId)
			{
				case 26 when Main.expertMode:
					Main.buffString = Language.GetTextValue("BuffDescription.WellFed_Expert");
					break;
				case 147:
					Main.bannerMouseOver = true;
					break;
				case 94:
					int percent = (int)(Main.player[Main.myPlayer].manaSickReduction * 100f) + 1;
					Main.buffString = Main.buffString + percent + "%";
					break;	
			}

			if (Main.meleeBuff[buffId])
				itemRarity = -10;

			BuffLoader.ModifyBuffTip(buffId, ref Main.buffString, ref itemRarity);
			Main.instance.MouseTextHackZoom(Lang.GetBuffName(buffId), itemRarity);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			Main.buffString = "";
			Main.bannerMouseOver = false;
			if (!Main.ingameOptionsWindow && !Main.playerInventory)
				DrawBuffs();
		}


		public override void Update(GameTime gameTime)
		{

			base.Update(gameTime);

			
			//dont use items while clicking buffs
			if (ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
			}

		}
	}
}
