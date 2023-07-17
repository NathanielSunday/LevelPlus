// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace LevelPlus.UI
{
    class StatCircle : UIElement {
        //C1 = cos(pi * 2/5)
        //C2 = cos(pi * 1/5)
        //S1 = sin(pi * 2/5)
        //S2 = sin(pi * 4/5)
        const float C1 = 0.309016994375f;
        const float C2 = 0.809016994375f;
        const float S1 = 0.951056516295f;
        const float S2 = 0.587785252292f;
        //big radius ratio
        const float R1 = 252f / 534f;
        //little radius ratio
        const float R2 = 190f / 534f;
        //big button size ratio
        const float B1 = 47f / 534f;
        //little button ratio
        const float B2 = 34f / 534f;


        private float diameter;
        private UITexture pentagram;
        private UIText unspentPoints;

        private StatButton Constitution;
        private StatButton Strength;
        private StatButton Intelligence;
        private StatButton Charisma;
        private StatButton Dexterity;

        private StatButton Mobility;
        private StatButton Excavation;
        private StatButton Animalia;
        private StatButton Luck;
        private StatButton Mysticism;

        private bool dragging;
        private Vector2 offset;

        public StatCircle(float diameter) {
            this.diameter = diameter;
            offset = Vector2.Zero;
        }

        public override void OnInitialize() {
            base.OnInitialize();
            Width.Set(diameter, 0f);
            Height.Set(diameter, 0f);

            Vector2 center = new Vector2(diameter / 2f, diameter / 2f);

            pentagram = new UITexture("levelplus/Textures/UI/ComboCircle", true);
            pentagram.Width.Set(diameter, 0f);
            pentagram.Height.Set(diameter, 0f);
            pentagram.Left.Set(0f, 0f);
            pentagram.Top.Set(0f, 0f);

            unspentPoints = new UIText("-1");
            unspentPoints.Width.Set(20f, 0f);
            unspentPoints.Height.Set(20f, 0f);
            unspentPoints.Top.Set(center.Y - unspentPoints.Height.Pixels / 2f, 0f);
            unspentPoints.Left.Set(center.X - unspentPoints.Width.Pixels / 2f, 0f);
            unspentPoints.OnClick += new MouseEvent(CloseMenu);

            pentagram.Append(unspentPoints);
            Append(pentagram);
            //big radius
            float BR = R1 * diameter;
            //little radius
            float LR = R2 * diameter;
            //big button radius
            float BBR = B1 * diameter;
            //little button radius
            float LBR = B2 * diameter;


            //bottom
            Animalia = new StatButton(Utils.Stat.ANIMALIA, LBR * 2);
            Animalia.Left.Set(center.X - LBR, 0f);
            Animalia.Top.Set(center.Y + LR - LBR, 0f);
            Append(Animalia);

            //bottom right
            Luck = new StatButton(Utils.Stat.LUCK, LBR * 2);
            Luck.Left.Set(center.X + S1 * LR - LBR, 0f);
            Luck.Top.Set(center.Y + C1 * LR - LBR, 0f);
            Append(Luck);

            //top right
            Mobility = new StatButton(Utils.Stat.MOBILITY, LBR * 2);
            Mobility.Left.Set(center.X + S2 * LR - LBR, 0f);
            Mobility.Top.Set(center.Y - C2 * LR - LBR, 0f);
            Append(Mobility);

            //top left
            Excavation = new StatButton(Utils.Stat.EXCAVATION, LBR * 2);
            Excavation.Left.Set(center.X - S2 * LR - LBR, 0f);
            Excavation.Top.Set(center.Y - C2 * LR - LBR, 0f);
            Append(Excavation);

            //bottom left
            Mysticism = new StatButton(Utils.Stat.MYSTICISM, LBR * 2);
            Mysticism.Left.Set(center.X - S1 * LR - LBR, 0f);
            Mysticism.Top.Set(center.Y + C1 * LR - LBR, 0f);
            Append(Mysticism);


            //top
            Constitution = new StatButton(Utils.Stat.CONSTITUTION, BBR * 2);
            Constitution.Left.Set(center.X - BBR, 0f);
            Constitution.Top.Set(center.Y - BR - BBR, 0f);
            Append(Constitution);

            //top left
            Strength = new StatButton(Utils.Stat.STRENGTH, BBR * 2);
            Strength.Left.Set(center.X - S1 * BR - BBR, 0f);
            Strength.Top.Set(center.Y - C1 * BR - BBR, 0f);
            Append(Strength);

            //bottom left
            Intelligence = new StatButton(Utils.Stat.INTELLIGENCE, BBR * 2);
            Intelligence.Left.Set(center.X - S2 * BR - BBR, 0f);
            Intelligence.Top.Set(center.Y + C2 * BR - BBR, 0f);
            Append(Intelligence);

            //bottom right
            Charisma = new StatButton(Utils.Stat.CHARISMA, BBR * 2);
            Charisma.Left.Set(center.X + S2 * BR - BBR, 0f);
            Charisma.Top.Set(center.Y + C2 * BR - BBR, 0f);
            Append(Charisma);

            //top right
            Dexterity = new StatButton(Utils.Stat.DEXTERITY, BBR * 2);
            Dexterity.Left.Set(center.X + S1 * BR - BBR, 0f);
            Dexterity.Top.Set(center.Y - C1 * BR - BBR, 0f);
            Append(Dexterity);
        }

        public override void OnDeactivate() {
            base.OnDeactivate();
            pentagram = null;
            unspentPoints = null;
            Constitution = null;
            Strength = null;
            Intelligence = null;
            Charisma = null;
            Dexterity = null;
            Mobility = null;
            Excavation = null;
            Animalia = null;
            Luck = null;
            Mysticism = null;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            LevelPlusModPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<LevelPlusModPlayer>();

            unspentPoints.SetText("" + modPlayer.Points);

            if (dragging) {
                Left.Set(Main.mouseX - offset.X, 0f);
                Top.Set(Main.mouseY - offset.Y, 0f);
                Recalculate();
            }

            Rectangle parentSpace = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parentSpace)) {
                Left.Pixels = Terraria.Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                Top.Pixels = Terraria.Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
                Recalculate();
            }
        }

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
        }

        private void CloseMenu(UIMouseEvent evt, UIElement listeningElement) {
            SoundEngine.PlaySound(SoundID.MenuTick);
            SpendUI.visible = false;
        }
    }
}
