using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace levelplus.UI
{
	class LevelCircle : UIElement
	{
		public Color backgroundColor = Color.Gray;
		private float height;
		private float width;
		private static Texture2D _backgroundTexture;


		public LevelCircle(int height, int width)
		{
			if (_backgroundTexture == null)
			{
				_backgroundTexture = ModContent.Request<Texture2D>("levelplus/Textures/UI/Circle", AssetRequestMode.ImmediateLoad).Value;
			}

			this.height = height;
			this.width = width;
		}

		public override void OnInitialize()
		{
			//base.OnInitialize();

			this.Height.Set(height, 0f);
			this.Width.Set(width, 0f);
			this.Left.Set(Main.screenWidth + (Main.screenWidth / 2), 0f);
			this.Top.Set(Main.screenHeight + (Main.screenHeight / 2), 0f); //center the circle
		}
	}
}
