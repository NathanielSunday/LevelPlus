// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace LevelPlus.Common.UI.XpBar;

internal class XpBarUIState : UIState
{
  private Vector2 placement;
  private XpBar xpBar;

  public override void OnInitialize()
  {
    placement = new Vector2(UIConfig.Instance.XpBarLeft, UIConfig.Instance.XpBarTop);

    xpBar = new XpBar();
    // 120 26
    xpBar.Width.Set(140f, 0f);
    xpBar.Height.Set(30f, 0f);
    xpBar.Left.Set(placement.X, 0f);
    xpBar.Top.Set(placement.Y, 0f);

    Append(xpBar);
  }

  public override void OnDeactivate()
  {
    base.OnDeactivate();
    UIConfig.Instance.XpBarLeft = (int)xpBar.Left.Pixels;
    UIConfig.Instance.XpBarTop = (int)xpBar.Top.Pixels;
    xpBar = null;
  }
}
