using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace levelplus.UI
{
	internal class GUI : UIState
	{
		public static bool visible;
		private LevelButton button;
		private ResourceBar xp;

		private levelplusModPlayer modPlayer;
		private Player player;

		private string unspentPoints;

		public override void OnInitialize()
		{
            xp = new ResourceBar(ResourceBarMode.XP, 25, 400);
			xp.Left.Set(Main.screenWidth - 405, 0f);
			xp.Top.Set(65f, 0f);
			xp.SetSnapPoint("xp", 3);
			base.Append(xp);

			button = new LevelButton(ButtonMode.LEVEL, 50, 50);
			button.Left.Set(510, 0f);
			button.Top.Set(20f, 0f);
			button.OnClick += new MouseEvent(OpenLevelClicked);
			base.Append(button);

			visible = true;
	
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			player = Main.player[Main.myPlayer];
			modPlayer = player.GetModPlayer<levelplusModPlayer>();

			unspentPoints = "" + modPlayer.getUnspentPoints() + " unspent points";
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			if (Main.mouseX >= button.Left.Pixels && Main.mouseX <= (button.Left.Pixels + button.Width.Pixels) && Main.mouseY >= button.Top.Pixels && Main.mouseY <= (button.Top.Pixels + button.Height.Pixels))
			{
				Main.instance.MouseText(unspentPoints);
			}
			
			
		}

		private void OpenLevelClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (LevelUI.visible)
			{
				//Main.PlaySound(SoundID.MenuClose, (int)player.Center.X, (int)player.Center.Y, 1, 1f);
			}
			else
			{
				//Main.PlaySound(SoundID.MenuOpen, (int)player.Center.X, (int)player.Center.Y, 1, 1f);
			}

			LevelUI.visible = !LevelUI.visible;
		}
	}
}

