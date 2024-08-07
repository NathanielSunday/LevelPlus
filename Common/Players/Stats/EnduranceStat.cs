// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria;

namespace LevelPlus.Common.Players.Stats;

public class EnduranceStat : BaseStat
{
  //diminish
  private float LifeRegen => StatConfig.Instance.Endurance_LifeRegen * Value;
  private int Life => StatConfig.Instance.Endurance_Life * Value;
  private int Defense => StatConfig.Instance.Endurance_Defense * Value;

  protected override object[] DescriptionArgs => new object[]
    { Life, Defense, LifeRegen * 100 };

  public override string Id => "Endurance";

  public override void ModifyPlayer(Player player)
  {
    player.statLifeMax2 += Life;
    player.statDefense += Defense;
  }

  public override void ModifyLifeRegen(Player player)
  {
    player.lifeRegen += (int)(player.lifeRegen * LifeRegen);
  }
}