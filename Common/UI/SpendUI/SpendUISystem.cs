// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace LevelPlus.Common.UI.SpendUI
{
  [Autoload(Side = ModSide.Client)]
  public class SpendUISystem : ModSystem
  {
    public static SpendUISystem Instance => ModContent.GetInstance<SpendUISystem>();

    internal SpendUIState spendUI;
    private UserInterface spendInterface;

    public void Show()
    {
      spendInterface?.SetState(spendUI);
    }

    public void Hide()
    {
      spendInterface?.SetState(null);
    }

    public void Toggle()
    {
      if (spendInterface?.CurrentState != null)
      {
        Hide();
        return;
      }
      Show();
    }

    public override void Load()
    {
      spendUI = new SpendUIState();
      spendInterface = new UserInterface();
      spendUI.Activate();
      Show();
    }

    public override void UpdateUI(GameTime gameTime)
    {
      if (spendInterface?.CurrentState != null)
      {
        spendInterface?.Update(gameTime);
      }
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
      int resourceBarsIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
      layers.Insert(resourceBarsIndex, new LegacyGameInterfaceLayer(
        "Level+: Spend Interface",
        delegate
        {
          if (spendInterface?.CurrentState != null)
          {
            spendInterface.Draw(Main.spriteBatch, new GameTime());
          }
          return true;
        },
        InterfaceScaleType.UI)
      );
    }
  }
}
