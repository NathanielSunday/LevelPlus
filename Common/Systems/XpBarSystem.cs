// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.UI.XpBar;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace LevelPlus.Common.Systems;

[Autoload(Side = ModSide.Client)]
public class XpBarSystem : ModSystem
{
  public static XpBarSystem Instance => ModContent.GetInstance<XpBarSystem>();

  internal XpBarUIState xpBarUI;
  private UserInterface xpBarInterface;

  public void Show()
  {
    xpBarInterface?.SetState(xpBarUI);
  }

  public void Hide()
  {
    xpBarInterface?.SetState(null);
  }

  public void Toggle()
  {
    if (xpBarInterface?.CurrentState is null)
    {
      Show();
      return;
    }

    Hide();
  }

  public override void Load()
  {
    xpBarUI = new XpBarUIState();
    xpBarInterface = new UserInterface();
    xpBarUI.Activate();
    Show();
  }

  public override void UpdateUI(GameTime gameTime)
  {
    if (xpBarInterface?.CurrentState is null) return;
    xpBarInterface?.Update(gameTime);
  }

  public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
  {
    int resourceBarsIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
    layers.Insert(resourceBarsIndex, new LegacyGameInterfaceLayer(
      "Level+: XP Bar",
      delegate
      {
        if (xpBarInterface?.CurrentState is not null)
        {
          xpBarInterface.Draw(Main.spriteBatch, new GameTime());
        }

        return true;
      },
      InterfaceScaleType.UI)
    );
  }
}

