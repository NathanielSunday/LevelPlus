// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class EndurancePlayer : BaseStat
{
  private EnduranceConfig Config => ModContent.GetInstance<EnduranceConfig>();

  protected override object[] DescriptionArgs => new object[] { };
  public override string Id => "Endurance";

  public override bool IsLoadingEnabled(Mod mod) => true;

  public override void Load(Mod mod)
  {
    ModContent.GetInstance<StatPlayer>().RegisterStat(this);
  }

  public override void SaveData(TagCompound tag)
  {
  }

  public override void LoadData(TagCompound tag)
  {
  }

  public override void ModifyPlayer(ref Player player)
  {
    player.statLifeMax2 += Value * Config.Life;
    player.statDefense += Value * Config.Defense;
  }

  public override void ModifyLifeRegen(ref Player player)
  {
    //diminish
    player.lifeRegen += (int)(player.lifeRegen * (Config.LifeRegen * Value));
  }
}