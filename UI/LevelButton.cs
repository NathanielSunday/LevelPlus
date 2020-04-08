using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace levelplus.UI
{
	internal enum ButtonMode
	{	
		CON,
		STR,
		INT,
		CHA,
		DEX,
		LEVEL
	}

	internal class LevelButton : UIElement
	{

		private ButtonMode button;
		private float height;
		private float width;
		private UICirclePanel currentButton;
		private UIText currentStat;

		public LevelButton(ButtonMode button, int height, int width)
		{
			this.button = button;
			this.height = height;
			this.width = width;
		}

		public override void OnInitialize()
		{
			//base.OnInitialize();

			Height.Set(height, 0f);
			Width.Set(width, 0f);

			/*switch (button)
			{
				case ButtonMode.CON:
					hover = "Constitution";					
					break;
				case ButtonMode.STR:
					hover = "Strength";					
					break;
				case ButtonMode.INT:
					hover = "Intelligence";					
					break;
				case ButtonMode.CHA:
					hover = "Charisma";				
					break;
				case ButtonMode.DEX:
					hover = "Dexterity";					
					break;
				case ButtonMode.LEVEL:
					hover = "Spend Points";
					break;
				default:
					break;
			}*/

			currentButton = new UICirclePanel(); //create current value panel
			currentButton.SetPadding(0);
			currentButton.Left.Set(0f, 0f);
			currentButton.Top.Set(0f, 0f);
			currentButton.Width.Set(width, 0f);
			currentButton.Height.Set(height, 0f);

			switch (button)
			{
				case ButtonMode.CON:
					currentButton.backgroundColor = Color.LimeGreen; //green
					currentButton.HoverText = "CON";
					break;
				case ButtonMode.STR:
					currentButton.backgroundColor = Color.Red; //red
					currentButton.HoverText = "STR";
					break;
				case ButtonMode.INT:
					currentButton.backgroundColor = Color.Blue; //blue	
					currentButton.HoverText = "INT";
					break;
				case ButtonMode.CHA:
					currentButton.backgroundColor = Color.Purple; //purple
					currentButton.HoverText = "CHA";
					break;
				case ButtonMode.DEX:
					currentButton.backgroundColor = Color.Yellow; //yellow
					currentButton.HoverText = "DEX";
					break;
				case ButtonMode.LEVEL:
					currentButton.backgroundColor = Color.Green;
					currentButton.HoverText = "LEVEL";
					break;
				default:
					break;
			}

			currentStat = new UIText("0"); //text for showing values
			currentStat.Width.Set(width, 0f);
			currentStat.Height.Set(height, 0f);
			currentStat.Top.Set(height / 2 - currentStat.MinHeight.Pixels / 2, 0f); //center the text, because I'm not a heathen



			currentButton.Append(currentStat);
			base.Append(currentButton);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			/*
			if (Main.mouseX >= this.Left.Pixels && Main.mouseX <= (this.Left.Pixels + this.Width.Pixels) && Main.mouseY >= this.Top.Pixels && Main.mouseY <= (this.Left.Pixels + this.Width.Pixels))
			{
				Main.instance.MouseText(currentButton.HoverText);
			}
			*/
			base.Draw(spriteBatch);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			Player player = Main.player[Main.myPlayer];
			levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();
			switch (button)
			{
				case ButtonMode.CON:
					currentStat.SetText("" + modPlayer.getCon());
					break;
				case ButtonMode.STR:
					currentStat.SetText("" + modPlayer.getStr());
					break;
				case ButtonMode.INT:
					currentStat.SetText("" + modPlayer.getInt());
					break;
				case ButtonMode.CHA:
					currentStat.SetText("" + modPlayer.getCha());
					break;
				case ButtonMode.DEX:
					currentStat.SetText("" + modPlayer.getDex());
					break;
				case ButtonMode.LEVEL:
					currentStat.SetText("" + (modPlayer.getLevel() + 1));
					break;
				default:
					break;
			}

			
		}
	}
}
