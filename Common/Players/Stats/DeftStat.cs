// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class DeftPlayer : StatPlayer
{
  private static DeftConfig Config => ModContent.GetInstance<DeftConfig>();

  protected override string Id => "Deft";
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
    Player.GetDamage(DamageClass.Ranged) *= 1.00f + (Value * Config.Damage);
  }

  public override void PostUpdateRunSpeeds()
  {
    Player.maxRunSpeed *= 1.00f + (Value * Config.MaxSpeed);
    //diminish
    Player.runAcceleration *= 1.00f + (Value * Config.Acceleration);
  }
}

