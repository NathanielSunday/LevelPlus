// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class AdroitPlayer : BaseStat
{
  private AdroitConfig Config => ModContent.GetInstance<AdroitConfig>();

  protected override object[] DescriptionArgs => new object[] { };
  public override string Id => "Adroit";

  public override bool IsLoadingEnabled(Mod mod) => true;

  public override void Load(Mod mod)
  {
    ModContent.GetInstance<StatPlayer>().RegisterStat(this);
  }

  public override void LoadData(TagCompound tag)
  {
  }

  public override void SaveData(TagCompound tag)
  {
  }

  public override void ModifyPlayer(ref Player player)
  {
    player.tileSpeed *= 1.00f + Value * Config.PlacementSpeed;
    player.wallSpeed *= 1.00f + Value * Config.PlacementSpeed;
    player.blockRange += Value / Config.RangeCost;
  }
}