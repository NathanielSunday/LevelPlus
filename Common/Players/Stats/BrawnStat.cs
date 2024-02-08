// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class BrawnPlayer : StatPlayer
{
  private static BrawnConfig Config => ModContent.GetInstance<BrawnConfig>();
  
  protected override string Id => "Brawn";
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
    Player.GetDamage(DamageClass.Melee) *= 1.00f + (Value * Config.Damage);
    Player.pickSpeed *= 1.00f + (Value * Config.PickSpeed);
    //diminish
    Player.wingTimeMax += (int)(Player.wingTimeMax * Config.MaxWingTime * Value);
  }
}
