// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs.Stats;

public class AdroitConfig : ModConfig
{
  public override ConfigScope Mode => ConfigScope.ServerSide;

  [DefaultValue(0.02f)] public float PlacementSpeed;
  [DefaultValue(25)] public int RangeCost;
}
