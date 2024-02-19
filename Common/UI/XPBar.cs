// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Players;
using LevelPlus.Common.Players.Stats;
using LevelPlus.Common.UI.SpendUI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace LevelPlus.Common.UI {
  class XPBar : DraggableUIPanel {
    private ResourceBar bar;
    private XPBarButton button;

    public override void OnInitialize() {
      base.OnInitialize();

      button = new XPBarButton(Height.Pixels);
      bar = new ResourceBar(ResourceBarMode.XP, Width.Pixels - Height.Pixels * (186f / 186f), Height.Pixels);

      button.Left.Set(0f, 0f);
      button.Top.Set(0f, 0f);

      bar.Left.Set(Height.Pixels * (186f / 186f), 0f);
      bar.Top.Set(0f, 0f);

      Append(bar);
      Append(button);

    }

    public override void OnDeactivate() {
      base.OnDeactivate();
      bar = null;
      button = null;
    }

    public override void Update(GameTime gameTime) {
      base.Update(gameTime);
    }

    public override void LeftMouseDown(UIMouseEvent evt) {
      base.LeftMouseDown(evt);
    }

    public override void LeftMouseUp(UIMouseEvent evt) {
      base.LeftMouseUp(evt);
    }
  }

  internal class XPBarButton : UIElement {
    private UITexture button;
    private UIText level;
    private float height;
    private float width;

    public XPBarButton(float height) {
      this.height = height;
      width = height * (186f / 186f);
    }

    public override void OnInitialize() {
      base.OnInitialize();

      Height.Set(height, 0f);
      Width.Set(width, 0f);

      button = new UITexture("LevelPlus/Assets/Textures/UI/Hollow_Start", true); //create button
      button.Left.Set(0f, 0f);
      button.Top.Set(0f, 0f);
      button.Width.Set(width, 0f);
      button.Height.Set(height, 0f);
      button.OnLeftClick += new MouseEvent(OpenLevelClicked);

      level = new UIText("0"); //text for showing values
      level.Width.Set(width, 0f);
      level.Height.Set(height, 0f);
      level.Top.Set(height / 2 - level.MinHeight.Pixels / 2, 0f);

      button.Append(level);
      Append(button);
    }

    public override void OnDeactivate() {
      base.OnDeactivate();
      level = null;
      button = null;
    }

    public override void Update(GameTime time) {
      base.Update(time);

      StatPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<StatPlayer>();
      level.SetText("" + (StatPlayer.XpToLevel(modPlayer.Xp) + 1));

      if (IsMouseHovering) {
        int numPlayers = 0;
        float averageLevel = 0;

        foreach (Player i in Main.player)
          if (i.active) {
            numPlayers++;
            averageLevel += StatPlayer.XpToLevel(i.GetModPlayer<StatPlayer>().Xp) + 1;

          }

        averageLevel /= numPlayers;

        Main.instance.MouseText("Level: " + (StatPlayer.XpToLevel(modPlayer.Xp) + 1) + "\n" + modPlayer.Points + " unspent points\n" + (Main.netMode == NetmodeID.MultiplayerClient ? numPlayers + " players online\nAverage Level: " + (int)averageLevel : ""));
      }
    }

    private void OpenLevelClicked(UIMouseEvent evt, UIElement listeningElement) {
      SoundEngine.PlaySound(SoundID.MenuTick);
      SpendUISystem.Instance.Toggle();
    }
  }
}
