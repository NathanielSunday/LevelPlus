// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus.Config {
  internal class ClientConfig : ModConfig {
    public override ConfigScope Mode => ConfigScope.ClientSide;
    public static ClientConfig Instance => ModContent.GetInstance<ClientConfig>();

    #region XPBar
    [DefaultValue(480f)]
    public float XPBarLeft;

    [DefaultValue(35f)]
    public float XPBarTop;
    #endregion

    #region Spend UI
    [DefaultValue(35f)]
    public float SpendUILeft;

    [DefaultValue(200f)]
    public float SpendUITop;
    #endregion
  }
}
