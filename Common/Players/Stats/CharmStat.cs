// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class CharmStat : BaseStat
{
  private float Damage => Value * StatConfig.Instance.Charm_Damage;
  private int MaxMinions => Value / StatConfig.Instance.Charm_MinionCost;
  private int MaxSentries => Value / StatConfig.Instance.Charm_SentryCost;
  private int FishingLevel => Value * StatConfig.Instance.Charm_Fishing;

  protected override object[] DescriptionArgs => new object[]
  {
    Damage * 100, MaxMinions, Value % StatConfig.Instance.Charm_MinionCost, MaxSentries,
    Value % StatConfig.Instance.Charm_SentryCost, FishingLevel
  };

  public override string Id => "Charm";
  public override Color UIColor => Color.Purple;

  public override void ModifyPlayer(Player player)
  {
    player.GetDamage(DamageClass.Summon) *= 1.00f + Damage;
    player.maxMinions += MaxMinions;
    player.maxTurrets += MaxSentries;
  }

  public override void ModifyFishingLevel(Item fishingRod, Item bait, ref float fishingLevel)
  {
    fishingLevel += FishingLevel;
  }
}

