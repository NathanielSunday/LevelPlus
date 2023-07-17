// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.UI;
using LevelPlus.Config;

namespace LevelPlus.UI {
  class SpendUI : UIState {
    public static bool Visible { get; private set; }
    private StatCircle circle;

    public static void ToggleVisible() {
      Visible = !Visible;
    }

    public override void OnInitialize() {
      base.OnInitialize();

      float circleDiameter = 300f;
      circle = new StatCircle(circleDiameter);
      circle.Left.Set(ClientConfig.Instance.SpendUILeft, 0f);
      circle.Top.Set(ClientConfig.Instance.SpendUITop, 0f);

      Append(circle);
    }

    public override void OnDeactivate() {
      base.OnDeactivate();
      circle = null;
    }
  }
}
