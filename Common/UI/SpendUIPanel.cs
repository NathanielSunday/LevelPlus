// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Players;
using Terraria;
using Terraria.ModLoader.UI.Elements;

namespace LevelPlus.Common.UI;

public class SpendUIPanel : DraggableUIPanel
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

    foreach (var stat in Main.LocalPlayer.GetModPlayer<StatPlayer>().Stats)
    {
      var statBar = new StatBar(stat.Value);
      mainPanel.Add(statBar);
    }

    Append(mainPanel);
  }
}