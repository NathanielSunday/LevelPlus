// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class AdroitPlayer : BaseStat
{
  private AdroitConfig Config => ModContent.GetInstance<AdroitConfig>();

  protected override object[] DescriptionArgs => [];
  public override string Id => "Adroit";

  public override bool IsLoadingEnabled(Mod mod) => true;

  public override void Load(Mod mod)
  {
    StatPlayer.RegisterStat(this);
  }

  public override void LoadData(TagCompound tag)
  {
  }

  public override void SaveData(TagCompound tag)
  {
  }

  public override void ModifyPlayer()
  {
    Player.tileSpeed *= 1.00f + Value * Config.PlacementSpeed;
    Player.wallSpeed *= 1.00f + Value * Config.PlacementSpeed;
    Player.blockRange += Value / Config.RangeCost;
  }
}