// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;

namespace LevelPlus.UI {
    abstract class DragableUIElement : UIElement {
        private bool dragging;
        private float width;
        private float height;
        private Vector2 offset;


        protected DragableUIElement(float width, float height) { 
            this.width = width;
            this.height = height;
        }

        public override void OnInitialize() {
            base.OnInitialize();

            Height.Set(height, 0f);
            Width.Set(width, 0f);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

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

            OnDragStart();
        }

        private void DragEnd(UIMouseEvent evt) {
            Vector2 end = evt.MousePosition;
            dragging = false;

            Left.Set(end.X - offset.X, 0f);
            Top.Set(end.Y - offset.Y, 0f);

            OnDragEnd();
            Recalculate();
        }

        protected virtual void OnDragStart() { }

        protected virtual void OnDragEnd() { }
    }
}
