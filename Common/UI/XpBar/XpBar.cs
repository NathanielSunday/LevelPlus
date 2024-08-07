// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Players;
using LevelPlus.Common.UI.SpendUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Common.UI.XpBar;

public class XpBar : DraggableUIElement
{
  private const float BarCapRatio = 3f / 13f;

  private UIImage bar;
  private UIImage barCap;
  private UIImage barQuotient;
  private UIImage button;
  private UIText text;

  #region Init

  private void InitBarQuotient()
  {
    barQuotient = new UIImage(ModContent.Request<Texture2D>(LevelPlus.Instance.AssetPath + "Textures/UI/Blank"));
    barQuotient.ScaleToFit = true;
    barQuotient.Width.Set(0f, 1f);
    barQuotient.Height.Set(0f, 1f);
    barQuotient.Left.Set(0f, 0f);
    barQuotient.Top.Set(0f, 0f);
    barQuotient.Color = new Color(50, 205, 50);
  }

  private void InitBarCap()
  {
    barCap = new UIImage(ModContent.Request<Texture2D>(LevelPlus.Instance.AssetPath + "Textures/UI/Hollow_End"));
    barCap.ScaleToFit = true;
    barCap.Width.Set(0f, BarCapRatio * Height.Pixels / Width.Pixels / (1f - button.Width.Percent));
    barCap.Height.Set(0f, 1f);
    barCap.Left.Set(0f, 1f);
    barCap.Top.Set(0f, 0f);
  }

  private void InitBar()
  {
    bar = new UIImage(ModContent.Request<Texture2D>(LevelPlus.Instance.AssetPath + "Textures/UI/Hollow"));
    bar.ScaleToFit = true;
    bar.Width.Set(0f, 1f - button.Width.Percent);
    bar.Height.Set(0f, 1f);
    bar.Left.Set(0f, button.Width.Percent);
    bar.Top.Set(0f, 0f);
    bar.Append(barQuotient);
    bar.Append(barCap);
  }

  private void InitText()
  {
    text = new UIText("0"); //text for showing level
    text.Width.Set(0f, 1f);
    text.Height.Set(0f, 1f);
    text.Left.Set(0f, 0f);
    text.Top.Set(0f, 0f);
    text.TextOriginX = 0.5f;
    text.TextOriginY = 0.5f; 
  }

  private void InitButton()
  {
    button = new UIImage(ModContent.Request<Texture2D>(LevelPlus.Instance.AssetPath + "Textures/UI/Hollow_Start"));
    button.ScaleToFit = true;
    button.Height.Set(0f, 1f);
    button.Width.Set(0f, Height.Pixels / Width.Pixels);
    button.Left.Set(0f, 0f);
    button.Top.Set(0f, 0f);
    button.OnLeftClick += delegate
    {
      SoundEngine.PlaySound(SoundID.MenuTick);
      SpendUISystem.Instance.Toggle();
    };
    button.Append(text);
  }

  #endregion

  public override void OnInitialize()
  {
    InitText();
    InitButton();
    InitBarCap();
    InitBarQuotient();
    InitBar();

    Append(bar);
    Append(button);
  }

  public override void OnDeactivate()
  {
    barCap = null;
    barQuotient = null;
    bar = null;
    text = null;
    button = null;
  }

  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);

    var player = Main.LocalPlayer.GetModPlayer<StatPlayer>();
    text.SetText(player.Level.ToString());

    if (bar.IsMouseHovering)
      Main.instance.MouseText(player.Xp + " | " + StatPlayer.LevelToXp(player.Level + 1));
    if (button.IsMouseHovering) Main.instance.MouseText(player.LevelDescription.Value);
  }

  protected override void DrawSelf(SpriteBatch spriteBatch)
  {
    var player = Main.LocalPlayer.GetModPlayer<StatPlayer>();
    float currentXp = player.Xp - StatPlayer.LevelToXp(player.Level);
    float neededXp = StatPlayer.LevelToXp(player.Level + 1) - StatPlayer.LevelToXp(player.Level);
    var quotient = currentXp / neededXp;

    barQuotient.Width.Set(0f, quotient);
    Recalculate();
  }
}