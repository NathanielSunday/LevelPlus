// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class EndurancePlayer : BaseStat
{
  private EnduranceConfig Config => ModContent.GetInstance<EnduranceConfig>();

  protected override object[] DescriptionArgs => [];
  public override string Id => "Endurance";

  public override bool IsLoadingEnabled(Mod mod) => true;

  public override void Load(Mod mod)
  {
    StatPlayer.RegisterStat(this);
  }

  public override void SaveData(TagCompound tag)
  {
  }

  public override void LoadData(TagCompound tag)
  {
  }

  public override void ModifyPlayer()
  {
    Player.statLifeMax2 += Value * Config.Life;
    Player.statDefense += Value * Config.Defense;
  }

  public override void ModifyLifeRegen()
  {
    //diminish
    Player.lifeRegen += (int)(Player.lifeRegen * (Config.LifeRegen * Value));
  }
}