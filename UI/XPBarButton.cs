using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace levelplus.UI {
    class XPBarButton : UIElement{
        private UIHollowButton button;
        private UIText currentStat;
        private float height;
        private float width;

        public XPBarButton(float height) {
            this.height = height;
            this.width = height * (186/186);
        }

        public override void OnInitialize() {
            base.OnInitialize();

            Height.Set(height, 0f);
            Width.Set(width, 0f);

            button = new UIHollowButton(); //create button
            button.Left.Set(0f, 0f);
            button.Top.Set(0f, 0f);
            button.Width.Set(width, 0f);
            button.Height.Set(height, 0f);
            button.OnClick += new MouseEvent(OpenLevelClicked);

            currentStat = new UIText("0"); //text for showing values
            currentStat.Width.Set(width, 0f);
            currentStat.Height.Set(height, 0f);
            currentStat.Top.Set(height / 2 - currentStat.MinHeight.Pixels / 2, 0f);

            button.Append(currentStat);
            base.Append(button);
        }

        public override void Update(GameTime time) {
            base.Update(time);

            levelplusModPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<levelplusModPlayer>();
            currentStat.SetText("" + (modPlayer.level + 1));
            if (ContainsPoint(new Vector2(Main.mouseX, Main.mouseY))) {
                Main.LocalPlayer.mouseInterface = true;
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);

            levelplusModPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<levelplusModPlayer>();
            if (Main.mouseX >= this.Left.Pixels && Main.mouseX <= this.Left.Pixels + this.Width.Pixels && Main.mouseY >= this.Top.Pixels && Main.mouseY <= this.Top.Pixels + this.Height.Pixels) {
                int numPlayers = 0;
                float averageLevel = 0;

                foreach (Player i in Main.player)
                    if (i.active)
                    {
                        numPlayers++;
                        averageLevel += i.GetModPlayer<levelplusModPlayer>().level + 1;
                    }

                averageLevel /= numPlayers;

                Main.instance.MouseText("Level: " + (modPlayer.level + 1) + "\n" + modPlayer.GetUnspentPoints() + " unspent points\n" + ((Main.netMode == NetmodeID.MultiplayerClient) ? numPlayers + " players online\nAverage Level: " + ((int)averageLevel) : ""));
            }
        }

        private void OpenLevelClicked(UIMouseEvent evt, UIElement listeningElement) {
            SoundEngine.PlaySound(SoundID.MenuTick);
            LevelUI.visible = !LevelUI.visible;
		}
	}
}
