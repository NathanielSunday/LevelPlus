// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace LevelPlus.Common.UI;

public class DraggableUIPanel : UIPanel
{
  private Vector2 offset;
  private bool dragging;

  public override void LeftMouseDown(UIMouseEvent evt)
  {
    if (IsMouseHovering)
      DragStart(evt);
  }

  public override void LeftMouseUp(UIMouseEvent evt)
  {
    DragEnd(evt);
  }

  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);

    if (IsMouseHovering)
    {
      Main.LocalPlayer.mouseInterface = true;
    }

    if (dragging)
    {
      Left.Set(Main.mouseX - offset.X, 0f);
      Top.Set(Main.mouseY - offset.Y, 0f);
      Recalculate();
    }

    Rectangle parentSpace = Parent.GetDimensions().ToRectangle();
    if (!GetDimensions().ToRectangle().Intersects(parentSpace))
    {
      Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
      Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
      Recalculate();
    }
  }

  private void DragStart(UIMouseEvent evt)
  {
    offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
    dragging = true;
  }

  private void DragEnd(UIMouseEvent evt)
  {
    Vector2 end = evt.MousePosition;
    dragging = false;

    Left.Set(end.X - offset.X, 0f);
    Top.Set(end.Y - offset.Y, 0f);

    Recalculate();
  }
}
