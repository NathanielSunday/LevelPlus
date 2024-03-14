// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.Systems;
using LevelPlus.Common.UI.SpendUI;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;

namespace LevelPlus.Common.UI;

public class SpendUIPanel : DraggableUIElement
{
  private const float MainPanelWidth = 200f;
  private const float MainPanelMaxHeight = 500f;

  private UIGrid mainPanel;

  public override void OnInitialize()
  {
    base.OnInitialize();

    mainPanel = new UIGrid();
    mainPanel.Width.Set(MainPanelWidth, 0f);
    mainPanel.MaxHeight.Set(MainPanelMaxHeight, 0f);

    List<string> idList = ModContent.GetInstance<StatProviderSystem>().GetIdList();
    foreach (var stat in idList) mainPanel.Add(new StatBar(stat));

    Append(mainPanel);
  }
}