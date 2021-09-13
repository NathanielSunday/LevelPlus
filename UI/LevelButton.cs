using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace levelplus.UI {
    internal enum ButtonMode {
        CON,
        STR,
        INT,
        CHA,
        DEX,
        MOB,
        EXC,
        ANI,
        GRA,
        MYS,
        LEVEL
    }

    internal class LevelButton : UIElement {

        private ButtonMode button;
        private float height;
        private float width;
        private UICirclePanel currentButton;
        private UIText currentStat;

        public LevelButton(ButtonMode button, int height, int width) {
            this.button = button;
            this.height = height;
            this.width = width;
        }

        public override void OnInitialize() {
            base.OnInitialize();

            Height.Set(height, 0f);
            Width.Set(width, 0f);

            currentButton = new UICirclePanel(); //create current value panel
            if (button != ButtonMode.LEVEL) {
                currentButton.OnClick += new MouseEvent(pointSpend);
            }

            currentButton.SetPadding(0);
            currentButton.Left.Set(0f, 0f);
            currentButton.Top.Set(0f, 0f);
            currentButton.Width.Set(width, 0f);
            currentButton.Height.Set(height, 0f);

            switch (button) {
                case ButtonMode.CON:
                    currentButton.backgroundColor = Color.LimeGreen; //green
                    break;
                case ButtonMode.STR:
                    currentButton.backgroundColor = Color.Red; //red
                    break;
                case ButtonMode.INT:
                    currentButton.backgroundColor = Color.Blue; //blue	
                    break;
                case ButtonMode.CHA:
                    currentButton.backgroundColor = Color.Purple; //purple
                    break;
                case ButtonMode.DEX:
                    currentButton.backgroundColor = Color.Yellow; //yellow
                    break;
                case ButtonMode.MOB:
                    break;
                case ButtonMode.EXC:
                    break;
                case ButtonMode.ANI:
                    break;
                case ButtonMode.GRA:
                    break;
                case ButtonMode.MYS:
                    break;
                case ButtonMode.LEVEL:
                    currentButton.backgroundColor = Color.Green;
                    break;
                default:
                    break;
            }

            currentStat = new UIText("0"); //text for showing values
            currentStat.Width.Set(width, 0f);
            currentStat.Height.Set(height, 0f);
            currentStat.Top.Set(height / 2 - currentStat.MinHeight.Pixels / 2, 0f); //center the text, because I'm not a heathen



            currentButton.Append(currentStat);
            base.Append(currentButton);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            //if (ContainsPoint(Main.MouseScreen)) {
            //    Main.LocalPlayer.mouseInterface = true;
            //}
            levelplusModPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<levelplusModPlayer>();
            switch (button) {
                case ButtonMode.CON:
                    currentStat.SetText("" + modPlayer.getCon());
                    break;
                case ButtonMode.STR:
                    currentStat.SetText("" + modPlayer.getStr());
                    break;
                case ButtonMode.INT:
                    currentStat.SetText("" + modPlayer.getInt());
                    break;
                case ButtonMode.CHA:
                    currentStat.SetText("" + modPlayer.getCha());
                    break;
                case ButtonMode.DEX:
                    currentStat.SetText("" + modPlayer.getDex());
                    break;
                case ButtonMode.MOB:
                    currentStat.SetText("" + modPlayer.getMob());
                    break;
                case ButtonMode.EXC:
                    currentStat.SetText("" + modPlayer.getExc());
                    break;
                case ButtonMode.ANI:
                    currentStat.SetText("" + modPlayer.getAni());
                    break;
                case ButtonMode.GRA:
                    currentStat.SetText("" + modPlayer.getGra());
                    break;
                case ButtonMode.MYS:
                    currentStat.SetText("" + modPlayer.getMys());
                    break;
                default:
                    break;
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            string text;
            int rarity;

            levelplusModPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<levelplusModPlayer>();

            switch (button) {
                case ButtonMode.CON:
                    text = "Constitution:\n\n"
                        + "  +" + (modPlayer.getCon() * 5) + " life (+" + (modPlayer.getLevel() * 2) + " from level)\n"
                        + "  +" + (modPlayer.getCon() / 3) + " defense\n"
                        + "  +" + (modPlayer.getCon() / 25) + " life regen";
                    rarity = 7; //lime
                    break;
                case ButtonMode.STR:
                    text = "Strength:\n\n"
                        + "  +" + (modPlayer.getStr() / 2.0f) + "% melee damage\n"
                        + "  +" + (modPlayer.getStr() / 20) + "% melee crit chance";
                    rarity = 10; //red
                    break;
                case ButtonMode.INT:
                    text = "Intelligence:\n\n"
                        + "  +" + (modPlayer.getInt() / 2.0f) + "% magic damage\n"
                        + "  +" + (modPlayer.getInt() / 20) + "% magic crit chance";
                    rarity = 9; //cyan
                    break;
                case ButtonMode.CHA:
                    text = "Charisma:\n\n"
                        + "  +" + (modPlayer.getCha() / 2.0f) + "% minion damage\n"
                        + "  +" + (modPlayer.getCha() / 20) + "% minion crit chance";
                    rarity = 6; //light purple
                    break;
                case ButtonMode.DEX:
                    text = "Dexterity:\n\n"
                        + "  +" + (modPlayer.getDex() / 2.0f) + "% ranged damage\n"
                        + "  +" + (modPlayer.getDex() / 20) + "% ranged crit chance";
                    rarity = 8; //yellow
                    break;
                case ButtonMode.MOB:
                    text = "Mobility:\n\n"
                        + "  +" + modPlayer.getMob() + "% move speed\n"
                        + "  +" + modPlayer.getMob() + "% max run speed";
                    rarity = 0; //white
                    break;
                case ButtonMode.EXC:
                    text = "Excavation:\n\n"
                        + "  +" + modPlayer.getExc() + "% pick speed\n"
                        + "  +" + modPlayer.getExc() + "% place speed\n"
                        + "  +" + modPlayer.getExc() + "% block range";
                    rarity = 0; //white
                    break;
                case ButtonMode.ANI:
                    text = "Animalia:\n\n"
                        + "  +" + (modPlayer.getAni()) + "% better fishing\n"
                        + "  +" + (modPlayer.getAni() / 20) + " minion slots\n"
                        + "  +" + (modPlayer.getAni()) + "% xp gain";
                    rarity = 0; //white
                    break;
                case ButtonMode.GRA:
                    text = "Grace:\n\n"
                        + "  +" + modPlayer.getGra() + "% wing time\n"
                        + "  +" + modPlayer.getGra() + "% jump height";
                    rarity = 0; //white
                    break;
                case ButtonMode.MYS:
                    text = "Mysticism:\n\n"
                        + "  +" + (modPlayer.getMys() * 2) + " max mana (+" + modPlayer.getLevel() + " from level)\n"
                        + "  +" + (modPlayer.getMys() / 25) + " mana regen";
                    rarity = 0; //white
                    break;
                default:
                    text = "";
                    rarity = 0;
                    break;
            }
            if (Main.mouseX >= this.Left.Pixels && Main.mouseX <= this.Left.Pixels + this.Width.Pixels && Main.mouseY >= this.Top.Pixels && Main.mouseY <= this.Top.Pixels + this.Height.Pixels) {
                Main.instance.MouseText(text, rarity);
            }
            

            Recalculate();
        }

        public ButtonMode GetButtonType() {
            return button;
        }

        private void pointSpend(UIMouseEvent evt, UIElement listeningElement) {
            SoundEngine.PlaySound(SoundID.MenuTick);
            levelplusModPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<levelplusModPlayer>();
            modPlayer.spend(button);
        }
    }
}
