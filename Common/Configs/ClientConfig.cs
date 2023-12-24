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
    [Range(0f, 1920f)]
    [DefaultValue(480f)]
    public float XPBar_Left;

    [Range(0f, 1080f)]
    [DefaultValue(35f)]
    public float XPBar_Top;
    #endregion

    #region Spend UI
    [Range(0f, 1920f)]
    [DefaultValue(35f)]
    public float SpendUI_Left;

    [Range(0f, 1080f)]
    [DefaultValue(200f)]
    public float SpendUI_Top;
    #endregion
  }
}
