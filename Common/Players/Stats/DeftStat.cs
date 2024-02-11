// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class DeftPlayer : BaseStat
{
  private DeftConfig Config => ModContent.GetInstance<DeftConfig>();

  protected override object[] DescriptionArgs => new object[] { };
  public override string Id => "Deft";

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
    player.GetDamage(DamageClass.Ranged) *= 1.00f + (Value * Config.Damage);
  }

  public override void ModifyRunSpeeds(ref Player player)
  {
    player.maxRunSpeed *= 1.00f + (Value * Config.MaxSpeed);
    //diminish
    player.runAcceleration *= 1.00f + (Value * Config.Acceleration);
  }
}

