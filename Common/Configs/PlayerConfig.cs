// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs;

public class PlayerConfig : ModConfig
{
  public override ConfigScope Mode => ConfigScope.ServerSide;
  
  [DefaultValue(true)] public bool RandomStartingWeapon;
  
  [DefaultValue(true)] public bool LossEnabled;
  [Range(0.01f, 1.00f)] [DefaultValue(0.10f)] public float LossAmount;
  
  [Range(10, 500)] [DefaultValue(100)] public int MaxLevel;
  [DefaultValue(3)] public int StartingPoints;
}