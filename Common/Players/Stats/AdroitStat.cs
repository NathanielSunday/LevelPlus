// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class AdroitPlayer : StatPlayer
{
  private static AdroitConfig Config => ModContent.GetInstance<AdroitConfig>();

  protected override string Id => "Adroit";
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
    Player.tileSpeed *= 1.00f + Value * Config.PlacementSpeed;
    Player.wallSpeed *= 1.00f + Value * Config.PlacementSpeed;
    Player.blockRange += Value / Config.RangeCost;
  }
}