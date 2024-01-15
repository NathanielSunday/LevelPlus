// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Players;
using LevelPlus.Common.Players.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace LevelPlus.Common.UI
{
  internal enum ResourceBarMode
  {
    XP
  }

  class ResourceBar : UIElement
  {

    private ResourceBarMode stat;
    private float width;
    private float height;
    private UITexture barBackground;
    private UITexture barCap;
    private UITexture currentBar;

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

      float capWidthCalc = 42 * height / 186;

      barCap = new UITexture("LevelPlus/Assets/Textures/UI/Hollow_End", true); //create end cap
      barCap.Width.Set(capWidthCalc, 0f);
      barCap.Height.Set(height, 0f);
      barCap.Left.Set(width - capWidthCalc, 0f);
      barCap.Top.Set(0f, 0f);

      barBackground = new UITexture("LevelPlus/Assets/Textures/UI/Hollow", true); //create background
      barBackground.Left.Set(0f, 0f);
      barBackground.Top.Set(0f, 0f);
      barBackground.Width.Set(width - capWidthCalc, 0f);
      barBackground.Height.Set(height, 0f);

      currentBar = new UITexture("LevelPlus/Assets/Textures/UI/Blank", true); //create current value panel
      currentBar.Left.Set(0f, 0f);
      currentBar.Top.Set(0f, 0f);
      currentBar.Width.Set(width - capWidthCalc, 0f);
      currentBar.Height.Set(height, 0f);

      //assignment of color
      switch (stat)
      {
        case ResourceBarMode.XP:
          currentBar.backgroundColor = new Color(50, 205, 50); //green
          break;
        default:
          break;
      }

      barBackground.Append(barCap);
      barBackground.Append(currentBar);

      //barBackground.Append(text);
      Append(barBackground);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);

      LevelStat modPlayer = Main.player[Main.myPlayer].GetModPlayer<LevelStat>();


      //spriteBatch.Begin();
      float quotient = 1f;
      //calculate quotient
      switch (stat)
      {
        case ResourceBarMode.XP:
          //quotient = modPlayer.XP / (float)modPlayer.NeededXP;
          break;
        default:
          break;
      }

      currentBar.Width.Set(quotient * width, 0f);

      Recalculate();
      //spriteBatch.End();

    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);

      LevelStat modPlayer = Main.player[Main.myPlayer].GetModPlayer<LevelStat>();
      string HoverText = "";
      switch (stat)
      {
        case ResourceBarMode.XP:
          //HoverText = "" + modPlayer.XP + " | " + modPlayer.NeededXP;
          break;
        default:
          HoverText = "";
          break;
      }
      if (IsMouseHovering)
      {
        Main.instance.MouseText(HoverText);
      }
    }
  }
}

