using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.ModLoader;
using Terraria.UI;

namespace levelplus.UI
{
	class UIFlatPanel : UIElement
	{
		public Color backgroundColor = Color.Gray;
		private static Texture2D _backgroundTexture;

		public UIFlatPanel()
		{
			if (_backgroundTexture == null)
			{
				_backgroundTexture = ModContent.GetTexture("levelplus/Textures/UI/Blank");
			}
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
			int width = (int)Math.Ceiling(dimensions.Width);
			int height = (int)Math.Ceiling(dimensions.Height);
			spriteBatch.Draw(_backgroundTexture, new Rectangle(point1.X, point1.Y, width, height), backgroundColor);
		}
	}
}
