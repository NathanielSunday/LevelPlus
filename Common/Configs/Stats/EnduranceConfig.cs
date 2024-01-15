// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs.Stats;

public class EnduranceConfig : ModConfig
{
  public override ConfigScope Mode => ConfigScope.ServerSide;

  [DefaultValue(5)] public int Life;
  [DefaultValue(2)] public int Defense;
  [DefaultValue(0.05f)] public float LifeRegen; //dim
}