// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class LevelStat : BaseStat
{
  private LevelConfig Config => ModContent.GetInstance<LevelConfig>();

  protected override object[] DescriptionArgs => [ Value / 2, Value / 2 ];

  public override int Value => StatPlayer.XpToLevel(StatPlayer.Xp);
  public override string Id => "Level";
  public override bool Displayable => false;

  public override bool IsLoadingEnabled(Mod mod) => true;

  public override void Load(Mod mod)
  {
    StatPlayer.RegisterStat(this);
  }

  public override void ModifyPlayer()
  {
    Player.statLifeMax2 += Value * Config.Life;
    Player.statManaMax2 += Value * Config.Mana;
  }
}

