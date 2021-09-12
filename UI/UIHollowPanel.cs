using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.UI;

namespace levelplus.UI
{
	class UIHollowPanel : UIElement
	{
		public Color backgroundColor = Color.White;
		private static Texture2D _backgroundTexture;

		public UIHollowPanel()
		{
			if (_backgroundTexture == null)
			{
				_backgroundTexture = ModContent.Request<Texture2D>("levelplus/Textures/UI/Hollow", AssetRequestMode.ImmediateLoad).Value;
			}
		}

		public override void OnDeactivate()
		{
			base.OnDeactivate();
			_backgroundTexture = null;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			Rectangle rectangle = GetDimensions().ToRectangle();
			spriteBatch.Draw(_backgroundTexture, rectangle, backgroundColor);
		}
	}
}
