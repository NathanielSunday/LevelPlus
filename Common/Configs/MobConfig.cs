// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs;

public class MobConfig : ModConfig
{
  public override ConfigScope Mode => ConfigScope.ServerSide;
  public static MobConfig Instance => ModContent.GetInstance<MobConfig>();
  
  [DefaultValue(true)] public bool ScalingEnabled;
  [DefaultValue(0.025f)] public float LevelScalar;
}
