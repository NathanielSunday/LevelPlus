// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria;

namespace LevelPlus.Common.Players.Stats;

public class AdroitStat : BaseStat
{
  private float PlacementSpeed => Value * StatConfig.Instance.Adroit_PlacementSpeed;
  private int BlockRange => Value / StatConfig.Instance.Adroit_RangeCost;

  protected override object[] DescriptionArgs => new object[]
    { PlacementSpeed * 100, BlockRange, Value % StatConfig.Instance.Adroit_RangeCost };

  public override string Id => "Adroit";
  public override Color UIColor => Color.Orange;

  public override void ModifyPlayer(Player player)
  {
    player.tileSpeed *= 1.00f + PlacementSpeed;
    player.wallSpeed *= 1.00f + PlacementSpeed;
    player.blockRange += BlockRange;
  }
}