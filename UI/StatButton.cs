using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace LevelPlus.UI {

    class StatButton : UIElement {

        public Utility.Stat type { get; private set; }
        private float height;
        private float width;
        private UITexture button;
        private UIText points;

        public StatButton(Utility.Stat type, float diameter) : this(type, diameter, diameter) { }

        public StatButton(Utility.Stat type, float height, float width) {
            this.type = type;
            this.height = height;
            this.width = width;
        }

        public override void OnInitialize() {
            base.OnInitialize();

            Height.Set(height, 0f);
            Width.Set(width, 0f);

            button = new UITexture("levelplus/Textures/UI/Circle", true);

            button.SetPadding(0);
            button.Left.Set(0f, 0f);
            button.Top.Set(0f, 0f);
            button.Width.Set(width, 0f);
            button.Height.Set(height, 0f);

            switch (type) {
                case Utility.Stat.CONSTITUTION:
                    button.backgroundColor = Color.LimeGreen; //green
                    break;
                case Utility.Stat.STRENGTH:
                    button.backgroundColor = Color.Red; //red
                    break;
                case Utility.Stat.INTELLIGENCE:
                    button.backgroundColor = Color.Blue; //blue	
                    break;
                case Utility.Stat.CHARISMA:
                    button.backgroundColor = Color.Purple; //purple
                    break;
                case Utility.Stat.DEXTERITY:
                    button.backgroundColor = Color.Yellow; //yellow
                    break;
                case Utility.Stat.MOBILITY:
                    break;
                case Utility.Stat.EXCAVATION:
                    break;
                case Utility.Stat.ANIMALIA:
                    break;
                case Utility.Stat.LUCK:
                    break;
                case Utility.Stat.MYSTICISM:
                    break;
                default:
                    break;
            }

            points = new UIText("0"); //text for showing values
            points.Width.Set(width, 0f);
            points.Height.Set(height, 0f);
            points.Top.Set(height / 2 - points.MinHeight.Pixels / 2, 0f); //center the text, because I'm not a heathen
            this.OnClick += new MouseEvent(pointSpend);


            button.Append(points);
            base.Append(button);
        }

        public override void OnDeactivate() {
            base.OnDeactivate();

            button = null;
            points = null;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            string text;
            int rarity;

            LevelPlusModPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<LevelPlusModPlayer>();

            switch (type) {
                case Utility.Stat.CONSTITUTION:
                    points.SetText("" + modPlayer.constitution);
                    text = "Constitution:\n\n"
                        + "  +" + (modPlayer.constitution * Utility.HealthPerPoint) + " life (+" + (modPlayer.Level * Utility.HealthPerLevel) + " from level)\n"
                        + "  +" + (modPlayer.constitution / Utility.DefensePerPoint) + " defense\n"
                        + "  +" + (modPlayer.constitution / Utility.HRegenPerPoint) + " life regen";
                    rarity = 7; //lime
                    break;
                case Utility.Stat.STRENGTH:
                    points.SetText("" + modPlayer.strength);
                    text = "Strength:\n\n"
                        + "  +" + ((int) (modPlayer.strength * (Utility.MeleeDamagePerPoint * 100))) + "% melee damage\n"
                        + "  +" + (modPlayer.strength / Utility.MeleeCritPerPoint) + "% melee crit chance";
                    rarity = 10; //red
                    break;
                case Utility.Stat.INTELLIGENCE:
                    points.SetText("" + modPlayer.intelligence);
                    text = "Intelligence:\n\n"
                        + "  +" + ((int) (modPlayer.intelligence * (Utility.MagicDamagePerPoint * 100))) + "% magic damage\n"
                        + "  +" + (modPlayer.intelligence / Utility.MagicCritPerPoint) + "% magic crit chance";
                    rarity = 9; //cyan
                    break;
                case Utility.Stat.CHARISMA:
                    points.SetText("" + modPlayer.charisma);
                    text = "Charisma:\n\n"
                        + "  +" + ((int) (modPlayer.charisma * (Utility.SummonDamagePerPoint * 100))) + "% minion damage\n"
                        + "  +" + (modPlayer.charisma / Utility.SummonCritPerPoint) + "% minion crit chance";
                    rarity = 6; //light purple
                    break;
                case Utility.Stat.DEXTERITY:
                    points.SetText("" + modPlayer.dexterity);
                    text = "Dexterity:\n\n"
                        + "  +" + ((int) (modPlayer.dexterity * (Utility.RangedDamagePerPoint * 100))) + "% ranged damage\n"
                        + "  +" + (modPlayer.dexterity / Utility.RangedCritPerPoint) + "% ranged crit chance";
                    rarity = 8; //yellow
                    break;
                case Utility.Stat.MOBILITY:
                    points.SetText("" + modPlayer.mobility);
                    text = "Mobility:\n\n"
                        + "  +" + ((int) (modPlayer.mobility * (Utility.AccelPerPoint * 100))) + "% acceleration\n"
                        + "  +" + ((int) (modPlayer.mobility * (Utility.WingPerPoint * 100))) + "% wing time\n"
                        + "  +" + ((int) (modPlayer.mobility * (Utility.RunSpeedPerPoint * 100))) + "% max run speed";
                    rarity = 0; //white
                    break;
                case Utility.Stat.EXCAVATION:
                    points.SetText("" + modPlayer.excavation);
                    text = "Excavation:\n\n"
                        + "  +" + ((int) (modPlayer.excavation * (Utility.PickSpeedPerPoint * 100))) + "% pick speed\n"
                        + "  +" + ((int) (modPlayer.excavation * (Utility.BuildSpeedPerPoint * 100))) + "% place speed\n"
                        + "  +" + (modPlayer.excavation / Utility.RangePerPoint) + " block range";
                    rarity = 0; //white
                    break;
                case Utility.Stat.ANIMALIA:
                    points.SetText("" + modPlayer.animalia);
                    text = "Animalia:\n\n"
                        + "  +" + ((int) (modPlayer.animalia * (Utility.FishSkillPerPoint * 100))) + "% better fishing\n"
                        + "  +" + (modPlayer.animalia / Utility.MinionPerPoint) + " minion slots\n";
                    //+ "  +" + ((int)(modPlayer.animalia * (levelplusConfig.Instance.MinionKnockBack * 100))) + "% minion knockback";
                    rarity = 0; //white
                    break;
                case Utility.Stat.LUCK:
                    points.SetText("" + modPlayer.luck);
                    text = "Luck:\n\n"
                        + "  +" + ((int) (modPlayer.luck * (Utility.XPPerPoint * 100))) + "% xp gain\n"
                        + "  +" + ((int) ((modPlayer.luck * 100) / Utility.AmmoPerPoint)) + "% chance not to consume ammo";
                    rarity = 0; //white
                    break;
                case Utility.Stat.MYSTICISM:
                    points.SetText("" + modPlayer.mysticism);
                    text = "Mysticism:\n\n"
                        + "  +" + (modPlayer.mysticism * Utility.ManaPerPoint) + " max mana (+" + (modPlayer.Level * Utility.ManaPerLevel) + " from level)\n"
                        + "  +" + (modPlayer.mysticism / Utility.ManaRegPerPoint) + " mana regen\n"
                        + "  -" + System.Math.Clamp((int) (modPlayer.mysticism * (Utility.ManaCostPerPoint * 100)), 0f, 99.0f) + "% mana cost (can't be reduced below 1%)";
                    rarity = 0; //white
                    break;
                default:
                    text = "";
                    rarity = 0;
                    break;
            }

            if (this.IsMouseHovering) {
                Main.instance.MouseText(text, rarity);
            }
        }

        private void pointSpend(UIMouseEvent evt, UIElement listeningElement) {
            SoundEngine.PlaySound(SoundID.MenuTick);
            LevelPlusModPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<LevelPlusModPlayer>();
            modPlayer.Spend(type, (ushort) (levelplus.SpendModFive.Current ? 5 : levelplus.SpendModTen.Current ? 10 : levelplus.SpendModTwentyFive.Current ? 25 : 1));
        }
    }
}
