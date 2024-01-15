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
  [DefaultValue(480)] public int XpBar_Left;
  [DefaultValue(35)] public int XpBar_Top;
  [DefaultValue(35)] public int SpendUI_Left;
  [DefaultValue(200)] public int SpendUI_Top;
}