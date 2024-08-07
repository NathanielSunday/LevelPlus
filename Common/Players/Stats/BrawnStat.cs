// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class BrawnStat : BaseStat
{
  //diminish
  private float WingTimeMax => Value * StatConfig.Instance.Brawn_MaxWingTime;
  private float Damage => Value * StatConfig.Instance.Brawn_Damage;
  private float PickSpeed => Value * StatConfig.Instance.Brawn_PickSpeed;

  protected override object[] DescriptionArgs => new object[]
    { Damage * 100, PickSpeed * 100, WingTimeMax * 100 };

  public override string Id => "Brawn";
  
  public override void ModifyPlayer(Player player)
  {
    player.GetDamage(DamageClass.Melee) *= 1.00f + Damage;
    player.pickSpeed *= 1.00f + PickSpeed;
    player.wingTimeMax += (int)(player.wingTimeMax * WingTimeMax);
  }
}
