// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs.Stats;

public class BrawnConfig : ModConfig
{
  public override ConfigScope Mode => ConfigScope.ServerSide;

  [DefaultValue(0.01f)] public float Damage;
  [DefaultValue(0.02f)] public float MaxWingTime; //dim
  [DefaultValue(0.01f)] public float PickSpeed;
  
  [Header("Calamity.Header")]
  [DefaultValue(0.01f)] public float RogueDamage;
  [DefaultValue(0.01f)] public float RogueVelocity;
}
