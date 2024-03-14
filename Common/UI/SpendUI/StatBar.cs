// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using LevelPlus.Common.Players;
using LevelPlus.Common.Players.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace LevelPlus.Common.UI.SpendUI;

public class StatBar : UIElement
{
  private const float NormalUISize = 20f;
  private const float UIMargin = 2f;
  private const float StatBarBackgroundWidth = 50f;
  
  private const float NormalUISizeWithMargin = NormalUISize + UIMargin;
  private const float StatBackgroundWithMargin = StatBarBackgroundWidth + UIMargin;
  
  private UIImage icon;
  private UIImage statBarMain;
  private UIText statText;
  private UIImage spendButton;
  private UIImage hint;
  
  private string statId;
  
  private bool displayHintText;

  public StatBar(string statId)
  {
    this.statId = statId;
  }

  #region Inits
  
  private void InitIcon()
  {
    var iconTexture = ModContent.Request<Texture2D>("LevelPlus/Assets/Textures/UI/Icons/" + statId);
    
    icon = new UIImage(iconTexture);
    icon.Width.Set(NormalUISize, 0f);
    icon.Height.Set(NormalUISize, 0f);
    icon.Left.Set(0f, 0f);
    icon.Top.Set(0, 0f);
  }

  private void InitStatBarMain()
  {
    var statBarTexture = ModContent.Request<Texture2D>("LevelPlus/Assets/Textures/UI/StatBar");
    
    statBarMain = new UIImage(statBarTexture);
    statBarMain.Left.Set(NormalUISizeWithMargin, 0f);
    statBarMain.Top.Set(0f, 0f);
    statBarMain.Width.Set(StatBarBackgroundWidth, 0f);
    statBarMain.Height.Set(NormalUISize, 0f);
    
    statText = new UIText("0");
    statText.Width.Set(0, 100f);
    statText.Height.Set(0f, 100f);
    statText.TextOriginY = 0.5f;
    statText.TextOriginX = 0.5f;
    
    statBarMain.Append(statText);
  }

  private void InitSpendButton()
  {
    var buttonSpendTexture = ModContent.Request<Texture2D>("LevelPlus/Assets/Textures/UI/StatSpend");
    
    spendButton = new UIImage(buttonSpendTexture);
    spendButton.Left.Set(NormalUISizeWithMargin + StatBackgroundWithMargin, 0f);
    spendButton.Top.Set(0f, 0f);
    spendButton.Width.Set(NormalUISize, 0f);
    spendButton.Height.Set(NormalUISize, 0f);
    spendButton.OnLeftClick += delegate
    {
      SoundEngine.PlaySound(SoundID.MenuTick);
      Main.LocalPlayer.GetModPlayer<StatPlayer>().AddStat(statId);
    }; 
  }

  private void InitHint()
  {
    var hintTexture = ModContent.Request<Texture2D>("LevelPlus/Assets/Textures/UI/Hint");
    
    hint = new UIImage(hintTexture);
    hint.Width.Set(NormalUISize, 0f);
    hint.Height.Set(NormalUISize, 0f);
    hint.Left.Set(NormalUISizeWithMargin + StatBackgroundWithMargin + NormalUISizeWithMargin, 0f);
    hint.Top.Set(0f, 0f);
    
    hint.OnMouseOver += delegate { displayHintText = true; };
    hint.OnMouseOut += delegate { displayHintText = false; };
  }

  #endregion
  
  public override void OnInitialize()
  {
    Width.Set(0f, 1f);
    Height.Set(NormalUISize, 0f);
    Left.Set(0, 0f);
    Top.Set(0, 0f);
    
    InitIcon();
    InitStatBarMain();
    InitSpendButton();
    InitHint();
    
    Append(icon);
    Append(statBarMain);
    Append(spendButton);
    Append(hint);
  }

  public override void OnDeactivate()
  {
    hint = null;
    statText = null;
    spendButton = null;
    statBarMain = null;
    icon = null;
  }

  public override void Update(GameTime time)
  {
    if(!Main.LocalPlayer.GetModPlayer<StatPlayer>().Stats.TryGetValue(statId, out BaseStat stat)) return;
    if (icon == null) InitIcon();
    if (statBarMain == null) InitStatBarMain();
    if (spendButton == null) InitSpendButton();
    if (hint == null) InitHint();
    
    icon.Color = stat.UIColor;
    statBarMain.Color = stat.UIColor;
    statText.TextColor = stat.UIColor;
    spendButton.Color = stat.UIColor;
    
    statText.SetText(stat.Value.ToString());
    
    if (displayHintText) Main.instance.MouseText(stat.Description.Value);
  }
}