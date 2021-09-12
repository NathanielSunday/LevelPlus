using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.UI;

namespace levelplus.UI {
    class UIHollowButton : UIElement {
        public Color backgroundColor = Color.White;
        private static Texture2D _backgroundTexture;

        public UIHollowButton() {
            if (_backgroundTexture == null) {
                _backgroundTexture = ModContent.Request<Texture2D>("levelplus/Textures/UI/Hollow_Start", AssetRequestMode.ImmediateLoad).Value;
            }
        }

        public override void OnDeactivate() {
            base.OnDeactivate();
            _backgroundTexture = null;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);


            //spriteBatch.Begin();
            Rectangle rectangle = GetDimensions().ToRectangle();
            spriteBatch.Draw(_backgroundTexture, rectangle, backgroundColor);
        }
    }
}

