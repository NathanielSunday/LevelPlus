using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;


namespace levelplus.UI
{
	class UICircleHollow : UIElement
	{
		public Color backgroundColor = Color.Black;
		private static Texture2D _backgroundTexture;

		public UICircleHollow()
		{
			if (_backgroundTexture == null)
				_backgroundTexture = ModContent.GetTexture("levelplus/Textures/UI/ComboCircle");
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
