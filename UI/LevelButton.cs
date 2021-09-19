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
        LUC,
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
                case ButtonMode.LUC:
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
                    currentStat.SetText("" + modPlayer.GetCon());
                    break;
                case ButtonMode.STR:
                    currentStat.SetText("" + modPlayer.GetStr());
                    break;
                case ButtonMode.INT:
                    currentStat.SetText("" + modPlayer.GetInt());
                    break;
                case ButtonMode.CHA:
                    currentStat.SetText("" + modPlayer.GetCha());
                    break;
                case ButtonMode.DEX:
                    currentStat.SetText("" + modPlayer.GetDex());
                    break;
                case ButtonMode.MOB:
                    currentStat.SetText("" + modPlayer.GetMob());
                    break;
                case ButtonMode.EXC:
                    currentStat.SetText("" + modPlayer.GetExc());
                    break;
                case ButtonMode.ANI:
                    currentStat.SetText("" + modPlayer.GetAni());
                    break;
                case ButtonMode.LUC:
                    currentStat.SetText("" + modPlayer.GetLuc());
                    break;
                case ButtonMode.MYS:
                    currentStat.SetText("" + modPlayer.GetMys());
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
                        + "  +" + (modPlayer.GetCon() * 5) + " life (+" + (modPlayer.GetLevel() * 2) + " from level)\n"
                        + "  +" + (modPlayer.GetCon() / 3) + " defense\n"
                        + "  +" + (modPlayer.GetCon() / 20) + " life regen";
                    rarity = 7; //lime
                    break;
                case ButtonMode.STR:
                    text = "Strength:\n\n"
                        + "  +" + modPlayer.GetStr() + "% melee damage\n"
                        + "  +" + (modPlayer.GetStr() / 15) + "% melee crit chance";
                    rarity = 10; //red
                    break;
                case ButtonMode.INT:
                    text = "Intelligence:\n\n"
                        + "  +" + modPlayer.GetInt() + "% magic damage\n"
                        + "  +" + (modPlayer.GetInt() / 15) + "% magic crit chance";
                    rarity = 9; //cyan
                    break;
                case ButtonMode.CHA:
                    text = "Charisma:\n\n"
                        + "  +" + modPlayer.GetCha() + "% minion damage\n"
                        + "  +" + (modPlayer.GetCha() / 15) + "% minion crit chance";
                    rarity = 6; //light purple
                    break;
                case ButtonMode.DEX:
                    text = "Dexterity:\n\n"
                        + "  +" + modPlayer.GetDex() + "% ranged damage\n"
                        + "  +" + (modPlayer.GetDex() / 15) + "% ranged crit chance";
                    rarity = 8; //yellow
                    break;
                case ButtonMode.MOB:
                    text = "Mobility:\n\n"
                        + "  +" + (modPlayer.GetMob() * 2) + "% acceleration\n"
                        + "  +" + (modPlayer.GetMob() * 2) + "% wing time\n"
                        + "  +" + (modPlayer.GetMob()) + "% max run speed";
                    rarity = 0; //white
                    break;
                case ButtonMode.EXC:
                    text = "Excavation:\n\n"
                        + "  +" + (modPlayer.GetExc()) + "% pick speed\n"
                        + "  +" + (modPlayer.GetExc() * 2) + "% place speed\n"
                        + "  +" + (modPlayer.GetExc() / 10) + " block range";
                    rarity = 0; //white
                    break;
                case ButtonMode.ANI:
                    text = "Animalia:\n\n"
                        + "  +" + (modPlayer.GetAni() * 2) + "% better fishing\n"
                        + "  +" + (modPlayer.GetAni() / 20) + " minion slots\n"
                        + "  +" + (modPlayer.GetAni() * 2) + "% minion knockback";
                    rarity = 0; //white
                    break;
                case ButtonMode.LUC:
                    text = "Luck:\n\n"
                        + "  +" + modPlayer.GetLuc() + "% xp gain\n"
                        + "  +" + modPlayer.GetLuc() + "% chance not to consume ammo";
                    rarity = 0; //white
                    break;
                case ButtonMode.MYS:
                    text = "Mysticism:\n\n"
                        + "  +" + (modPlayer.GetMys() * 2) + " max mana (+" + modPlayer.GetLevel() + " from level)\n"
                        + "  +" + (modPlayer.GetMys() / 15) + " mana regen\n"
                        + "  -" + System.Math.Clamp(modPlayer.GetMys() / 2.0f, 0.0f, 100.0f) + "% mana cost";
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
