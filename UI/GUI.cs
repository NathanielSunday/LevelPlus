using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace levelplus.UI
{
	internal class GUI : UIState
	{
		public static bool visible;
		//private static readonly MethodInfo DrawBuffIcon = typeof(Main).GetMethod("DrawBuffIcon", BindingFlags.NonPublic | BindingFlags.Static);
		//public static bool level;
		//public float oldScale;

		//private static readonly MethodInfo DrawBuffIcon = typeof(Main).GetMethod("DrawBuffIcon", BindingFlags.NonPublic | BindingFlags.Static);
		//private static readonly MethodInfo DrawBuffs = typeof(Main).GetMethod("DrawBuffs", BindingFlags.NonPublic | BindingFlags.Static);
		private LevelButton button;
		private ResourceBar health;
		private ResourceBar mana;
		private ResourceBar xp;
		private ResourceBar breath;
		private ResourceBar lava;
		private BuffBar buff;

		private levelplusModPlayer modPlayer;
		private Player player;

		private string unspentPoints;

		/*public override void OnDeactivate()
        {
            Player player = Main.player[Main.myPlayer];
            levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();

            /*StyleDimension healthLeft = health.Left;
            StyleDimension healthTop = health.Top;
            StyleDimension manaLeft = mana.Left;
            StyleDimension manaTop = mana.Top;
            StyleDimension xpLeft = xp.Left;
            StyleDimension xpTop = xp.Top;
            StyleDimension buffLeft = buff.Left;
            StyleDimension buffTop = buff.Top;
        }*/

		public override void OnInitialize()
		{

			visible = true;

			buff = new BuffBar();
			//buff.Left.Set(20f, 0f);
			//buff.Top.Set(80f, 0f);
			base.Append(buff);

			breath = new ResourceBar(ResourceBarMode.BREATH, 15, 200);
			breath.Left.Set(Main.screenWidth / 2 - 100, 0f);
			breath.Top.Set(Main.screenHeight / 2 - 40, 0f);
			base.Append(breath);

			lava = new ResourceBar(ResourceBarMode.LAVA, 15, 200);
			lava.Left.Set(Main.screenWidth / 2 - 100, 0f);
			lava.Top.Set(Main.screenHeight / 2 - 40, 0f);
			base.Append(lava);

			health = new ResourceBar(ResourceBarMode.HEALTH, 25, 400);
			health.Left.Set(Main.screenWidth - 405, 0f);
			health.Top.Set(5f, 0f);
			health.SetSnapPoint("health", 1);
			base.Append(health);

            mana = new ResourceBar(ResourceBarMode.MANA, 25, 400);
			mana.Left.Set(Main.screenWidth - 405, 0f);
			mana.Top.Set(35f, 0f);
			mana.SetSnapPoint("mana", 2);
			base.Append(mana);

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

			//base.Append(xpCanvas);
			//DrawBuffs();	
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
				Main.PlaySound(SoundID.MenuClose, (int)player.Center.X, (int)player.Center.Y, 1, 1f);
			}
			else
			{
				Main.PlaySound(SoundID.MenuOpen, (int)player.Center.X, (int)player.Center.Y, 1, 1f);
			}

			LevelUI.visible = !LevelUI.visible;
		}
	}
}

