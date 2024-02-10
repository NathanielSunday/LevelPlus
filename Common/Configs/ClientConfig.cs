// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs;

public class ClientConfig : ModConfig
{
  public override ConfigScope Mode => ConfigScope.ClientSide;
  public static ClientConfig Instance => ModContent.GetInstance<ClientConfig>();

  // ReSharper disable InconsistentNaming
  [DefaultValue(480)] public int XpBarLeft;
  [DefaultValue(35)] public int XpBarTop;
  [DefaultValue(35)] public int SpendUILeft;
  [DefaultValue(200)] public int SpendUITop;
}