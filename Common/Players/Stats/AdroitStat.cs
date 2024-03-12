// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class AdroitStat : BaseStat
{
  private AdroitConfig Config => ModContent.GetInstance<AdroitConfig>();

  protected override List<object> DescriptionArgs => new();
  public override string Id => "Adroit";

  public override void ModifyPlayer(Player player)
  {
    player.tileSpeed *= 1.00f + Value * Config.PlacementSpeed;
    player.wallSpeed *= 1.00f + Value * Config.PlacementSpeed;
    player.blockRange += Value / Config.RangeCost;
  }
}