// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class AdroitPlayer : StatPlayer
{
  private static AdroitConfig Config => ModContent.GetInstance<AdroitConfig>();
  
  protected override string Id => "Adroit";
  public override LocalizedText Name => Language.GetText(NameKey).WithFormatArgs();
  public override LocalizedText Description => Language.GetText(DescriptionKey).WithFormatArgs();

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