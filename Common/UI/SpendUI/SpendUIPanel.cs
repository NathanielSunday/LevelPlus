// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ModLoader.UI.Elements;

namespace LevelPlus.Common.UI.SpendUI;

public class SpendUIPanel : DraggableUIPanel
{
  private const float HorizontalPadding = 10f;
  private const float MainPanelHeight = 150f;
  private const float ScrollbarWidth = 20f;

  // 3 elements are 20 wide
  // Each element has a 2 wide margin
  // The whole element has padding on left and right
  private const float MainPanelWidth = (3 * (20f + 2f)) + (50f + 2f) + ScrollbarWidth + (2 * HorizontalPadding);

  private UIGrid statGrid;
  private UIScrollbar scrollbar;

  public override void OnInitialize()
  {
    Width.Set(MainPanelWidth, 0f);
    Height.Set(MainPanelHeight, 0f);

    scrollbar = new UIScrollbar();
    scrollbar.Left.Set(MainPanelWidth - ScrollbarWidth - (2 * HorizontalPadding), 0f);
    scrollbar.Width.Set(ScrollbarWidth, 0f);
    scrollbar.Height.Set(MainPanelHeight, 0f);

    statGrid = new UIGrid();
    statGrid.SetScrollbar(scrollbar);
    statGrid.Width.Set(0, 1f);
    statGrid.Height.Set(0, 1f);
    statGrid.Left.Set(0f, 0f);
    statGrid.Top.Set(0f, 0f);
    PaddingLeft = HorizontalPadding;
    PaddingRight = HorizontalPadding;

    List<string> idList = StatProviderSystem.Instance.GetIdList();
    foreach (var stat in idList) statGrid.Add(new StatBar(stat));

    statGrid.Recalculate();

    Append(statGrid);
    Append(scrollbar);
  }

  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);
    statGrid.Update(gameTime);

    if (IsMouseHovering) PlayerInput.LockVanillaMouseScroll("LevelPlus/SpendStat");
  }
}