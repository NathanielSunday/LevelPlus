// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus.Config {
  internal class ClientConfig : ModConfig {
    public override ConfigScope Mode => ConfigScope.ClientSide;
    public static ClientConfig Instance => ModContent.GetInstance<ClientConfig>();

    [Label("XP Bar Left")]
    [DefaultValue(480f)]
    public float XPBarLeft;

    [Label("XP Bar Top")]
    [DefaultValue(35f)]
    public float XPBarTop;

    [Label("SpendUI Left")]
    [DefaultValue(35f)]
    public float SpendUILeft;

    [Label("Spend UI Top")]
    [DefaultValue(200f)]
    public float SpendUITop;
  }
}
