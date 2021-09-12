using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace levelplus.UI {
    internal enum ResourceBarMode {
        XP
    }

    class ResourceBar : UIElement {

        private XPBarButton xpButton;
        private ResourceBarMode stat;
        private float width;
        private float height = 24;
        private UIHollowPanel barBackground;
        private UIHollowCap barCap;
        private UIFlatPanel currentBar;

        public ResourceBar(ResourceBarMode stat, float width) {
            this.stat = stat;
            this.width = width;
        }

        public override void OnInitialize() {
            base.OnInitialize();

            Height.Set(height, 0f);
            Width.Set(width, 0f);

            barBackground = new UIHollowPanel(); //create background
            barBackground.Left.Set(0f, 0f);
            barBackground.Top.Set(0f, 0f);
            barBackground.Width.Set(width, 0f);
            barBackground.Height.Set(height, 0f);

            barCap = new UIHollowCap();
            barCap.Left.Set(width, 0f);
            barCap.Top.Set(0f, 0f);
            barCap.Width.Set(42 * height / 186, 0f);
            barCap.Height.Set(height, 0f);


            currentBar = new UIFlatPanel(); //create current value panel
            currentBar.Left.Set(0f, 0f);
            currentBar.Top.Set(0f, 0f);
            currentBar.Width.Set(width, 0f);
            currentBar.Height.Set(height, 0f);

            //assignment of color
            switch (stat) {
                case ResourceBarMode.XP:
                    currentBar.backgroundColor = new Color(50, 205, 50); //green
                    break;
                default:
                    break;
            }

            barBackground.Append(barCap);
            barBackground.Append(currentBar);
            
            //barBackground.Append(text);
            base.Append(barBackground);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);

            Player player = Main.player[Main.myPlayer];
            levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();

            

            if (Main.mouseX >= this.Left.Pixels && Main.mouseX <= this.Left.Pixels + this.Width.Pixels && Main.mouseY >= this.Top.Pixels && Main.mouseY <= this.Top.Pixels + this.Height.Pixels) {
                Main.instance.MouseText("" + (ushort)modPlayer.getCurrentXP() + " | " + modPlayer.getNeededXP());
            }

            //spriteBatch.Begin();
            float quotient = 1f;
            //calculate quotient
            switch (stat) {
                case ResourceBarMode.XP:
                    quotient = (float)modPlayer.getCurrentXP() / (float)modPlayer.getNeededXP();
                    break;
                default:
                    break;
            }

            currentBar.Width.Set(quotient * width, 0f);

            Recalculate();
            //spriteBatch.End();

        }

        public override void Update(GameTime gameTime) {

            base.Update(gameTime);

            if (ContainsPoint(new Vector2(Main.mouseX, Main.mouseY))) {
                Main.LocalPlayer.mouseInterface = true;
            }


            switch (stat) {
                case ResourceBarMode.XP:
                    //text.SetText("" + (ushort)modPlayer.getCurrentXP() + " | " + modPlayer.getNeededXP()); //Set XP
                    break;
                default:
                    break;
            }

            //old drawing code that I keep incase I want to come back to being able to drag them

            /*
            if (dragging) {
                Left.Set(Main.mouseX - offset.X, 0f); // Main.MouseScreen.X and Main.mouseX are the same.
                Top.Set(Main.mouseY - offset.Y, 0f);
                Recalculate();
            }

            // Here we check if the DragableUIPanel is outside the Parent UIElement rectangle. 
            // (In our example, the parent would be ExampleUI, a UIState. This means that we are checking that the DragableUIPanel is outside the whole screen)
            // By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution.
            var parentSpace = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parentSpace)) {
                Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
                // Recalculate forces the UI system to do the positioning math again.
                Recalculate();
            }*/
        }
        /*
        public override void MouseDown(UIMouseEvent evt) {
            base.MouseDown(evt);
            DragStart(evt);
        }

        public override void MouseUp(UIMouseEvent evt) {
            base.MouseUp(evt);
            DragEnd(evt);
        }

        private void DragStart(UIMouseEvent evt) {
            offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            dragging = true;
        }

        private void DragEnd(UIMouseEvent evt) {
            Vector2 end = evt.MousePosition;
            dragging = false;

            Left.Set(end.X - offset.X, 0f);
            Top.Set(end.Y - offset.Y, 0f);

            Recalculate();
        }*/
    }
}

