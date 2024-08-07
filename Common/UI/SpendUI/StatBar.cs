// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Players;
using LevelPlus.Common.Players.Stats;
using LevelPlus.Common.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace LevelPlus.Common.UI.SpendUI;

public class StatBar : UIElement
{
  /*
  private const float NormalUISize = 20f;
  private const float UIMargin = 2f;
  private const float StatBarBackgroundWidth = 50f;

  private const float NormalUISizeWithMargin = NormalUISize + UIMargin;
  private const float StatBackgroundWithMargin = StatBarBackgroundWidth + UIMargin;
*/
  private UIImage icon;
  private UITextBox amount;
  private UIButton<UIText> button;

  private string id;

  public StatBar(string statId)
  {
    id = statId;
  }

  #region Inits

  private void InitIcon()
  {
    var iconTexture = ModContent.Request<Texture2D>(StatProviderSystem.Instance.GetIconPath(id));

    icon = new UIImage(iconTexture);
    icon.Width.Set(0f, Height.Pixels / Width.Pixels);
    icon.Height.Set(0f, 1f);
    Append(icon);
  }

  private void InitButton()
  {
    button = new UIButton<UIText>(new UIText("+"));
    button.Width.Set(0f, Height.Pixels / Width.Pixels);
    button.Height.Set(0f, 1f);
    button.Left.Set(0f, 1f - button.Width.Percent);
    button.OnLeftClick += delegate
    {
      SoundEngine.PlaySound(SoundID.MenuTick);
      Main.LocalPlayer.GetModPlayer<StatPlayer>().AddStat(id);
    };
    Append(button);
  }
  
  private void InitTextBox()
  {
    amount = new UITextBox("0");
    amount.Width.Set(0, 1f - button.Width.Percent - icon.Width.Percent);
    amount.Height.Set(0f, 1f);
    amount.Left.Set(0f, 0.5f * (1f - amount.Width.Percent));
    Append(amount);
  }

  #endregion

  public override void OnInitialize()
  { 
    InitIcon();
    InitButton();
    InitTextBox();
  }

  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);
    
    if (!Main.LocalPlayer.GetModPlayer<StatPlayer>().Stats.TryGetValue(id, out BaseStat stat)) return;
    amount.SetText(stat.Value.ToString());
    
    if (icon.IsMouseHovering) Main.instance.MouseText(stat.Name.Value + "\n" + stat.Description.Value);
    if (button.IsMouseHovering)  {
      Main.LocalPlayer.mouseInterface = true;
      // Main.instance.MouseText();
    }
  }
}