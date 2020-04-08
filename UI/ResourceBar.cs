using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace levelplus.UI
{
	internal enum ResourceBarMode
	{
		HEALTH,
		MANA,
		XP, 
		BREATH,
		LAVA
	}

	class ResourceBar : UIElement
	{

		//private LevelButton xpButton;
		private ResourceBarMode stat;
		private float width;
		private float height;
		private UIFlatPanel barBackground;
		private UIFlatPanel currentBar;
		private UIText text;
		private Vector2 offset;
		public bool dragging;

		public ResourceBar(ResourceBarMode stat, int height, int width)
		{
			this.stat = stat;
			this.width = width;
			this.height = height;
		}

		public override void OnInitialize()
		{
			Height.Set(height, 0f);
			Width.Set(width, 0f);

			barBackground = new UIFlatPanel(); //create gray background
			barBackground.Left.Set(0f, 0f);
			barBackground.Top.Set(0f, 0f);
			barBackground.backgroundColor = Color.Gray;
			barBackground.Width.Set(width, 0f);
			barBackground.Height.Set(height, 0f);

			currentBar = new UIFlatPanel(); //create current value panel
			currentBar.SetPadding(0);
			currentBar.Left.Set(0f, 0f);
			currentBar.Top.Set(0f, 0f);
			currentBar.Width.Set(width, 0f);
			currentBar.Height.Set(height, 0f);

			/*xpButton = new LevelButton(ButtonMode.LEVEL, 35, 35);
			xpButton.SetPadding(0);
			xpButton.Left.Set(0f, 0f);
			xpButton.Top.Set(0f, 0f);
			xpButton.Width.Set(35, 0f);
			xpButton.Height.Set(35, 0f);*/

			//assignment of color
			switch (stat)
			{
				case ResourceBarMode.HEALTH:
					currentBar.backgroundColor = new Color(164, 55, 65); //red
					break;
				case ResourceBarMode.MANA:
					currentBar.backgroundColor = new Color(46, 67, 114); //blue
					break;
				case ResourceBarMode.XP:
					currentBar.backgroundColor = new Color(50, 205, 50); //green
					break;
				case ResourceBarMode.BREATH:
					currentBar.backgroundColor = Color.LightBlue;
					break;
				case ResourceBarMode.LAVA:
					currentBar.backgroundColor = Color.Orange;
					break;
				default:
					break;
			}

			text = new UIText("0|0"); //text for showing values
			text.Width.Set(width, 0f);
			text.Height.Set(height, 0f);
			text.Top.Set(height / 2 - text.MinHeight.Pixels / 2, 0f); //center the text, because I'm not a heathen

			//barBackground.Append(xpButton);
			barBackground.Append(currentBar);
			barBackground.Append(text);
			base.Append(barBackground);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			Player player = Main.player[Main.myPlayer];
			levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();
			float quotient = 1f;
			//calculate quotient
			switch (stat)
			{
				case ResourceBarMode.HEALTH:
					quotient = (float)player.statLife / (float)player.statLifeMax2;
					break;
				case ResourceBarMode.MANA:
					quotient = (float)player.statMana / (float)player.statManaMax2;
					break;
				case ResourceBarMode.XP:
					quotient = (float)modPlayer.getCurrentXP() / (float)modPlayer.getNeededXP();
					break;
				case ResourceBarMode.BREATH:
					quotient = (float)player.breath / (float)player.breathMax;
					break;
				case ResourceBarMode.LAVA:
					quotient = (float)player.lavaTime / (float)player.lavaMax;
					break;
				default:
					break;
			}
			
			currentBar.Width.Set(quotient * width, 0f);

			Recalculate();

			
		}

		public override void Update(GameTime gameTime)
		{
			
			base.Update(gameTime);
			Player player = Main.player[Main.myPlayer];
			levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();



			switch (stat)
			{
				case ResourceBarMode.HEALTH:
					text.SetText("" + player.statLife + " | " + player.statLifeMax2); //Set Life
					break;
				case ResourceBarMode.MANA:
					text.SetText("" + player.statMana + " | " + player.statManaMax2); //Set Mana
					break;
				case ResourceBarMode.XP:
					text.SetText("" + modPlayer.getCurrentXP() + " | " + modPlayer.getNeededXP()); //Set XP
					break;
				case ResourceBarMode.BREATH:
					text.SetText("");
					if (player.breath == player.breathMax)
						barBackground.Width.Set(0f, 0f);
					else
						barBackground.Width.Set(width, 0f);
					break;
				case ResourceBarMode.LAVA:
					text.SetText("");
					if (player.lavaTime == player.lavaMax)
						barBackground.Width.Set(0f, 0f);
					else
						barBackground.Width.Set(width, 0f);
					break;
				default:
					break;
			}
			if (stat != ResourceBarMode.BREATH && stat != ResourceBarMode.LAVA)
			{
				// Checking ContainsPoint and then setting mouseInterface to true is very common. This causes clicks on this UIElement to not cause the player to use current items. 
				if (ContainsPoint(Main.MouseScreen) && !(stat == ResourceBarMode.BREATH && stat == ResourceBarMode.LAVA))
				{
					Main.LocalPlayer.mouseInterface = true;
				}
			}

			if (dragging)
			{
				Left.Set(Main.mouseX - offset.X, 0f); // Main.MouseScreen.X and Main.mouseX are the same.
				Top.Set(Main.mouseY - offset.Y, 0f);
				Recalculate();
			}

			// Here we check if the DragableUIPanel is outside the Parent UIElement rectangle. 
			// (In our example, the parent would be ExampleUI, a UIState. This means that we are checking that the DragableUIPanel is outside the whole screen)
			// By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution.
			var parentSpace = Parent.GetDimensions().ToRectangle();
			if (!GetDimensions().ToRectangle().Intersects(parentSpace))
			{
				Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
				Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
				// Recalculate forces the UI system to do the positioning math again.
				Recalculate();
			}
		}

		public override void MouseDown(UIMouseEvent evt)
		{
			base.MouseDown(evt);
			if (stat != ResourceBarMode.BREATH && stat != ResourceBarMode.LAVA)
				DragStart(evt);
		}

		public override void MouseUp(UIMouseEvent evt)
		{
			base.MouseUp(evt);
			if (stat != ResourceBarMode.BREATH && stat != ResourceBarMode.LAVA)
				DragEnd(evt);
		}

		private void DragStart(UIMouseEvent evt)
		{
			offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
			dragging = true;
		}

		private void DragEnd(UIMouseEvent evt)
		{
			Vector2 end = evt.MousePosition;
			dragging = false;

			Left.Set(end.X - offset.X, 0f);
			Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}
	}
}

