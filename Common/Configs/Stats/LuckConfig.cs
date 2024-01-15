// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs.Stats;

public class LuckConfig : ModConfig
{
  public override ConfigScope Mode => ConfigScope.ServerSide;

  [DefaultValue(0.05f)] public float Crit; //dim
  [DefaultValue(0.03f)] public float AmmoReductionChance; //dim
  [DefaultValue(0.03f)] public float ManaReductionChance; //dim
  [DefaultValue(0.05f)] public float TerrariaLuck; //dim
}
