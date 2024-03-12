// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class EnduranceStat : BaseStat
{
  private EnduranceConfig Config => ModContent.GetInstance<EnduranceConfig>();

  protected override List<object> DescriptionArgs => new();
  public override string Id => "Endurance";

  public override void ModifyPlayer(Player player)
  {
    player.statLifeMax2 += Value * Config.Life;
    player.statDefense += Value * Config.Defense;
  }

  public override void ModifyLifeRegen(Player player)
  {
    //diminish
    player.lifeRegen += (int)(player.lifeRegen * (Config.LifeRegen * Value));
  }
}