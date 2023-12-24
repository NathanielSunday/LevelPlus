// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace LevelPlus.Common.UI.XpBar
{
  internal class XpBarUIState : UIState
  {
    public static bool Visible { get; private set; }
    private XPBar XpBar;
    private Vector2 placement;

    public override void OnInitialize()
    {
      base.OnInitialize();
      placement = new Vector2(ClientConfig.Instance.XPBar_Left, ClientConfig.Instance.XPBar_Top);

      XpBar = new XPBar();

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
}

