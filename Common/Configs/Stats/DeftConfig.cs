// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs.Stats;

public class DeftConfig : ModConfig
{
  public override ConfigScope Mode => ConfigScope.ServerSide;

  [DefaultValue(0.01f)] public float Damage;
  [DefaultValue(0.01f)] public float MaxSpeed;
  [DefaultValue(0.02f)] public float Acceleration; //dim
}

