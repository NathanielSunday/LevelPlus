// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.Configs.Stats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class CharmStat : BaseStat
{
  private CharmConfig Config => ModContent.GetInstance<CharmConfig>();

  protected override List<object> DescriptionArgs => new();
  public override string Id => "Charm";
  public override Color UIColor => Color.Purple;

  public override void ModifyPlayer(Player player)
  {
    player.GetDamage(DamageClass.Summon) *= 1.00f + Value * Config.Damage;
    player.maxMinions += Value / Config.MinionCost;
    player.maxTurrets += Value / Config.SentryCost;
  }

  public override void ModifyFishingLevel(Item fishingRod, Item bait, ref float fishingLevel)
  {
    fishingLevel += Value * Config.Fishing;
  }
}

