// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace LevelPlus.Common.UI.XpBar;

internal enum ResourceBarMode
{
  Xp
}

class ResourceBar : UIElement
{
  private ResourceBarMode stat;
  private float width;
  private float height;


  public ResourceBar(ResourceBarMode stat, float width, float height)
  {
    this.stat = stat;
    this.width = width;
    this.height = height;
  }

  public override void OnInitialize()
  {
    base.OnInitialize();

    Height.Set(height, 0f);
    Width.Set(width, 0f);



    //assignment of color
    switch (stat)
    {
      case ResourceBarMode.Xp:
         //green
        break;
    }


  }

  protected override void DrawSelf(SpriteBatch spriteBatch)
  {
    base.DrawSelf(spriteBatch);

    StatPlayer player = Main.LocalPlayer.GetModPlayer<StatPlayer>();


    //spriteBatch.Begin();
    float quotient = 1f;
    //calculate quotient
    switch (stat)
    {
      case ResourceBarMode.Xp:

        break;
    }

    //currentBar.Width.Set(quotient * width, 0f);

    Recalculate();
  }

  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);

    StatPlayer player = Main.LocalPlayer.GetModPlayer<StatPlayer>();
    string HoverText = stat switch
    {
      ResourceBarMode.Xp => "" + player.Xp + " | " + StatPlayer.LevelToXp(player.Level + 1),
      _ => ""
    };

    if (IsMouseHovering)
    {
      Main.instance.MouseText(HoverText);
    }
  }
}


