// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs.Stats;

public class CharmConfig : ModConfig
{
  public override ConfigScope Mode => ConfigScope.ServerSide;

  [DefaultValue(0.01f)] public float Damage;
  [DefaultValue(20)] public int MinionCost;
  [DefaultValue(30)] public int SentryCost;
  [DefaultValue(1)] public int Fishing;
}
