// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class EndurancePlayer : StatPlayer
{
  private static EnduranceConfig Config => ModContent.GetInstance<EnduranceConfig>();

  protected override string Id => "Endurance";
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
    Player.statLifeMax2 += Value * Config.Life;
    Player.statDefense += Value * Config.Defense;
  }

  public override void UpdateLifeRegen()
  {
    //diminish
    Player.lifeRegen += (int)(Player.lifeRegen * (Config.LifeRegen * Value));
  }
}