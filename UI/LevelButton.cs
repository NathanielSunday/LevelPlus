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
                    currentStat.SetText("" + modPlayer.constitution);
                    break;
                case ButtonMode.STR:
                    currentStat.SetText("" + modPlayer.strength);
                    break;
                case ButtonMode.INT:
                    currentStat.SetText("" + modPlayer.intelligence);
                    break;
                case ButtonMode.CHA:
                    currentStat.SetText("" + modPlayer.charisma);
                    break;
                case ButtonMode.DEX:
                    currentStat.SetText("" + modPlayer.dexterity);
                    break;
                case ButtonMode.MOB:
                    currentStat.SetText("" + modPlayer.mobility);
                    break;
                case ButtonMode.EXC:
                    currentStat.SetText("" + modPlayer.excavation);
                    break;
                case ButtonMode.ANI:
                    currentStat.SetText("" + modPlayer.animalia);
                    break;
                case ButtonMode.LUC:
                    currentStat.SetText("" + modPlayer.luck);
                    break;
                case ButtonMode.MYS:
                    currentStat.SetText("" + modPlayer.mysticism);
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
                        + "  +" + (modPlayer.constitution * levelplusConfig.Instance.HealthPerPoint) + " life (+" + (modPlayer.level * levelplusConfig.Instance.HealthPerLevel) + " from level)\n"
                        + "  +" + (modPlayer.constitution / levelplusConfig.Instance.DefensePerPoint) + " defense\n"
                        + "  +" + (modPlayer.constitution / levelplusConfig.Instance.HRegenPerPoint) + " life regen";
                    rarity = 7; //lime
                    break;
                case ButtonMode.STR:
                    text = "Strength:\n\n"
                        + "  +" + ((int)(modPlayer.strength * (levelplusConfig.Instance.MeleeDamagePerPoint * 100))) + "% melee damage\n"
                        + "  +" + (modPlayer.strength / levelplusConfig.Instance.MeleeCritPerPoint) + "% melee crit chance";
                    rarity = 10; //red
                    break;
                case ButtonMode.INT:
                    text = "Intelligence:\n\n"
                        + "  +" + ((int)(modPlayer.intelligence * (levelplusConfig.Instance.MagicDamagePerPoint * 100))) + "% magic damage\n"
                        + "  +" + (modPlayer.intelligence / levelplusConfig.Instance.MagicCritPerPoint) + "% magic crit chance";
                    rarity = 9; //cyan
                    break;
                case ButtonMode.CHA:
                    text = "Charisma:\n\n"
                        + "  +" + ((int)(modPlayer.charisma * (levelplusConfig.Instance.SummonDamagePerPoint * 100))) + "% minion damage\n"
                        + "  +" + (modPlayer.charisma / levelplusConfig.Instance.SummonCritPerPoint) + "% minion crit chance";
                    rarity = 6; //light purple
                    break;
                case ButtonMode.DEX:
                    text = "Dexterity:\n\n"
                        + "  +" + ((int)(modPlayer.dexterity * (levelplusConfig.Instance.RangedDamagePerPoint * 100))) + "% ranged damage\n"
                        + "  +" + (modPlayer.dexterity / levelplusConfig.Instance.RangedCritPerPoint) + "% ranged crit chance";
                    rarity = 8; //yellow
                    break;
                case ButtonMode.MOB:
                    text = "Mobility:\n\n"
                        + "  +" + ((int)(modPlayer.mobility * (levelplusConfig.Instance.AccelPerPoint * 100))) + "% acceleration\n"
                        + "  +" + ((int)(modPlayer.mobility * (levelplusConfig.Instance.WingPerPoint * 100))) + "% wing time\n"
                        + "  +" + ((int)(modPlayer.mobility * (levelplusConfig.Instance.RunSpeedPerPoint * 100))) + "% max run speed";
                    rarity = 0; //white
                    break;
                case ButtonMode.EXC:
                    text = "Excavation:\n\n"
                        + "  +" + ((int)(modPlayer.excavation * (levelplusConfig.Instance.PickSpeedPerPoint * 100))) + "% pick speed\n"
                        + "  +" + ((int)(modPlayer.excavation * (levelplusConfig.Instance.BuildSpeedPerPoint * 100))) + "% place speed\n"
                        + "  +" + (modPlayer.excavation / levelplusConfig.Instance.RangePerPoint) + " block range";
                    rarity = 0; //white
                    break;
                case ButtonMode.ANI:
                    text = "Animalia:\n\n"
                        + "  +" + ((int)(modPlayer.animalia * (levelplusConfig.Instance.FishSkillPerPoint *100))) + "% better fishing\n"
                        + "  +" + (modPlayer.animalia / levelplusConfig.Instance.MinionPerPoint) + " minion slots\n"
                        + "  +" + ((int)(modPlayer.animalia * (levelplusConfig.Instance.MinionKnockBack * 100))) + "% minion knockback";
                    rarity = 0; //white
                    break;
                case ButtonMode.LUC:
                    text = "Luck:\n\n"
                        + "  +" + ((int)(modPlayer.luck * (levelplusConfig.Instance.XPPerPoint * 100))) + "% xp gain\n"
                        + "  +" + ((int)((modPlayer.luck * 100) / levelplusConfig.Instance.AmmoPerPoint)) + "% chance not to consume ammo";
                    rarity = 0; //white
                    break;
                case ButtonMode.MYS:
                    text = "Mysticism:\n\n"
                        + "  +" + (modPlayer.mysticism * levelplusConfig.Instance.ManaPerPoint) + " max mana (+" + (modPlayer.level * levelplusConfig.Instance.ManaPerLevel) + " from level)\n"
                        + "  +" + (modPlayer.mysticism / levelplusConfig.Instance.ManaRegPerPoint) + " mana regen\n"
                        + "  -" + System.Math.Clamp((int)(modPlayer.mysticism * (levelplusConfig.Instance.ManaCostPerPoint * 100)), 0.0f, 100.0f) + "% mana cost";
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
