using Terraria.UI;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Audio;
using Terraria.ID;

namespace levelplus.UI {
    internal class LevelUI : UIState {
        public static bool visible;

        const float CIRCLE_RATIO = 310f / 256f;

        private LevelButton Con;
        private LevelButton Str;
        private LevelButton Int;
        private LevelButton Cha;
        private LevelButton Dex;

        private LevelButton Mob;
        private LevelButton Exc;
        private LevelButton Ani;
        private LevelButton Gra;
        private LevelButton Mys;

        private UICircleHollow combat;
        private UIText unspentPoints;

        public override void OnInitialize() {
            base.OnInitialize();

            visible = false;

            combat = new UICircleHollow();
            combat.Width.Set(200 * CIRCLE_RATIO, 0f);
            combat.Height.Set(200 * CIRCLE_RATIO, 0f);
            combat.SetPadding(0);
            float widthCenter = (Main.screenWidth / 2f);
            float heightCenter = (Main.screenHeight / 2f);
            combat.Left.Set(widthCenter - combat.Width.Pixels / 2f, 0f);
            combat.Top.Set(heightCenter - combat.Height.Pixels / 2f, 0f);

            float m1 = 100f * CIRCLE_RATIO - 25f;
            float m2 = 75f * CIRCLE_RATIO - 19f;
            float c1 = (m1 * (float)Math.Cos(2 * Math.PI / 5));
            float c2 = (m1 * (float)Math.Cos(Math.PI / 5));
            float s1 = (m1 * (float)Math.Sin(2 * Math.PI / 5));
            float s2 = (m1 * (float)Math.Sin(4 * Math.PI / 5));


            unspentPoints = new UIText("-1");
            unspentPoints.SetPadding(0);
            unspentPoints.Width.Set(50f, 0f);
            unspentPoints.Height.Set(50f, 0f);
            unspentPoints.Top.Set(m1 + 13f, 0f);
            unspentPoints.Left.Set(m1 - 1f, 0f);
            unspentPoints.OnClick += new MouseEvent(CloseMenu);
            combat.Append(unspentPoints);

            base.Append(combat);

            //bottom
            Ani = new LevelButton(ButtonMode.ANI, 38, 38);
            Ani.Left.Set(m1 + 6f + combat.Left.Pixels, 0f);
            Ani.Top.Set(m1 + m2 + 4f + combat.Top.Pixels, 0f);
            base.Append(Ani);

            Gra = new LevelButton(ButtonMode.GRA, 38, 38);
            Gra.Left.Set(m1 + s1 - 19f + combat.Left.Pixels, 0f);
            Gra.Top.Set(m1 + c1 + combat.Top.Pixels, 0f);
            base.Append(Gra);

            Mob = new LevelButton(ButtonMode.MOB, 38, 38);
            Mob.Left.Set(m1 + s2 - 9f + combat.Left.Pixels, 0f);
            Mob.Top.Set(m1 - c2 + 25f + combat.Top.Pixels, 0f);
            base.Append(Mob);

            Exc = new LevelButton(ButtonMode.EXC, 38, 38);
            Exc.Left.Set(m1 - s2 + 20f + combat.Left.Pixels, 0f);
            Exc.Top.Set(m1 - c2 + 26f + combat.Top.Pixels, 0f);
            base.Append(Exc);

            Mys = new LevelButton(ButtonMode.MYS, 38, 38);
            Mys.Left.Set(m1 - s1 + 29f + combat.Left.Pixels, 0f);
            Mys.Top.Set(m1 + c1 - 4f + combat.Top.Pixels, 0f);
            base.Append(Mys);


            //top
            Con = new LevelButton(ButtonMode.CON, 50, 50);
            Con.Left.Set(m1 + combat.Left.Pixels, 0f);
            Con.Top.Set(0 + combat.Top.Pixels, 0f);
            base.Append(Con);

            //top left
            Str = new LevelButton(ButtonMode.STR, 50, 50);
            Str.Left.Set(m1 - s1 + combat.Left.Pixels, 0f);
            Str.Top.Set(m1 - c1 + combat.Top.Pixels, 0f);
            base.Append(Str);

            //bottom left
            Int = new LevelButton(ButtonMode.INT, 50, 50);
            Int.Left.Set(m1 - s2 + combat.Left.Pixels, 0f);
            Int.Top.Set(m1 + c2 + combat.Top.Pixels, 0f);
            base.Append(Int);

            //bottom right
            Cha = new LevelButton(ButtonMode.CHA, 50, 50);
            Cha.Left.Set(m1 + s2 + combat.Left.Pixels, 0f);
            Cha.Top.Set(m1 + c2 + combat.Top.Pixels, 0f);
            base.Append(Cha);

            //top right
            Dex = new LevelButton(ButtonMode.DEX, 50, 50);
            Dex.Left.Set(m1 + s1 + combat.Left.Pixels, 0f);
            Dex.Top.Set(m1 - c1 + combat.Top.Pixels, 0f);
            base.Append(Dex);

        }

        public override void Update(GameTime gameTime) {
            
            base.Update(gameTime);

            Player player = Main.player[Main.myPlayer];
            levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();

            unspentPoints.SetText("" + modPlayer.getUnspentPoints());            
        }

        //close the menu when center is clicked
        private void CloseMenu(UIMouseEvent evt, UIElement listeningElement) {
            SoundEngine.PlaySound(SoundID.MenuTick);
            visible = false;
        }
    }
}
