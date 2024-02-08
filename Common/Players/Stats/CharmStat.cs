// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class CharmPlayer : StatPlayer
{
  private static CharmConfig Config => ModContent.GetInstance<CharmConfig>();

  protected override string Id => "Charm";
  protected override object[] DescriptionArgs => new object[] { };

  protected override void OnLoadData(TagCompound tag)
  {
    throw new System.NotImplementedException();
  }

  protected override void OnSaveData(TagCompound tag)
  {
    throw new System.NotImplementedException();
  }

  public override void PostUpdateMiscEffects()
  {
    Player.GetDamage(DamageClass.Summon) *= 1.00f + Value * Config.Damage;
    Player.maxMinions += Value / Config.MinionCost;
    Player.maxTurrets += Value / Config.SentryCost;
  }

  public override void GetFishingLevel(Item fishingRod, Item bait, ref float fishingLevel)
  {
    fishingLevel += Value * Config.Fishing;
  }
}

