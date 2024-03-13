// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Players;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;

namespace LevelPlus.Common.UI;

public class SpendUIPanel : DraggableUIPanel
{
  private const float MainPanelWidth = 200f;
  private const float MainPanelMaxHeight = 500f;

  private UIGrid mainPanel;
  private bool initialized;

  public void InitializeStats()
  {
    if(initialized) return;
    foreach (var stat in ModContent.GetInstance<StatPlayer>().Stats)
    {
      var statBar = new StatBar(stat.Value);
      mainPanel.Add(statBar);
    }
    initialized = true;
  }

  public override void OnInitialize()
  {
    base.OnInitialize();

    mainPanel = new UIGrid();
    mainPanel.Width.Set(MainPanelWidth, 0f);
    mainPanel.MaxHeight.Set(MainPanelMaxHeight, 0f);

    Append(mainPanel);
  }
}