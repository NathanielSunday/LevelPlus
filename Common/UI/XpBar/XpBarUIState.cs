// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace LevelPlus.Common.UI.XpBar;

internal class XpBarUIState : UIState
{
  private Vector2 placement;
  private XpBar XpBar;

  public override void OnInitialize()
  {
    base.OnInitialize();

    placement = new Vector2(ClientConfig.Instance.XpBarLeft, ClientConfig.Instance.XpBarTop);

    XpBar = new XpBar();

    XpBar.Left.Set(placement.X, 0f);
    XpBar.Top.Set(placement.Y, 0f);
    XpBar.SetSnapPoint("Origin", 0, placement);

    Append(XpBar);
  }

  public override void OnDeactivate()
  {
    base.OnDeactivate();
    XpBar = null;
  }
}
