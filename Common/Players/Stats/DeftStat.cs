// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class DeftStat : BaseStat
{
  //diminish
  private float Acceleration => Value * StatConfig.Instance.Deft_Acceleration;
  private float Damage => Value * StatConfig.Instance.Deft_Damage;
  private float MaxRunSpeed => Value * StatConfig.Instance.Deft_MaxSpeed;

  protected override object[] DescriptionArgs => new object[]
    { Damage * 100, MaxRunSpeed * 100, Acceleration * 100 };

  public override string Id => "Deft";
  

  public override void ModifyPlayer(Player player)
  {
    player.GetDamage(DamageClass.Ranged) *= 1.00f + Damage;
  }

  public override void ModifyRunSpeeds(Player player)
  {
    player.maxRunSpeed *= 1.00f + MaxRunSpeed;
    player.runAcceleration *= 1.00f + Acceleration;
  }
}

