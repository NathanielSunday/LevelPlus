// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class EndurancePlayer : StatPlayer
{
  private static EnduranceConfig Config => ModContent.GetInstance<EnduranceConfig>();

  protected override string Id => "Endurance";
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
    Player.statLifeMax2 += Value * Config.Life;
    Player.statDefense += Value * Config.Defense;
  }

  public override void UpdateLifeRegen()
  {
    //diminish
    Player.lifeRegen += (int)(Player.lifeRegen * (Config.LifeRegen * Value));
  }
}