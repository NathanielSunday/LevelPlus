// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs {
  internal class ClientConfig : ModConfig {
    public override ConfigScope Mode => ConfigScope.ClientSide;
    public static ClientConfig Instance => ModContent.GetInstance<ClientConfig>();

    #region XPBar
    [DefaultValue(480f)]
    public float XPBar_Left;

    [DefaultValue(35f)]
    public float XPBar_Top;
    #endregion

    #region Spend UI
    [DefaultValue(35f)]
    public float SpendUI_Left;

    [DefaultValue(200f)]
    public float SpendUI_Top;
    #endregion
  }
}
