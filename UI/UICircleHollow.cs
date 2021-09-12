using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
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
				_backgroundTexture = ModContent.Request<Texture2D>("levelplus/Textures/UI/ComboCircle", AssetRequestMode.ImmediateLoad).Value;
		}

		public override void OnDeactivate()
		{
			base.OnDeactivate();
			_backgroundTexture = null;
		}

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

			if (ContainsPoint(new Vector2(Main.mouseX, Main.mouseY))) {
				Main.LocalPlayer.mouseInterface = true;
			}
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
