using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.ModLoader;
using Terraria.UI;


namespace levelplus.UI
{
	class UICirclePanel : UIElement
	{
		public Color backgroundColor = Color.Gray;
		private static Texture2D _backgroundTexture;
		public string HoverText;

		public UICirclePanel()
		{
			if (_backgroundTexture == null)
			{
				_backgroundTexture = ModContent.GetTexture("levelplus/Textures/UI/Circle");
			}
		}

		public override void OnDeactivate()
		{
			base.OnDeactivate();
			_backgroundTexture = null;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			//base.DrawSelf(spriteBatch);


			//spriteBatch.Begin();
			Rectangle rectangle = GetDimensions().ToRectangle();
			spriteBatch.Draw(_backgroundTexture, rectangle, backgroundColor);
		}
	}
}
