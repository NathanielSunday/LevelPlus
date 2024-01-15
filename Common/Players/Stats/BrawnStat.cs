// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class BrawnPlayer : StatPlayer
{
  private static BrawnConfig Config => ModContent.GetInstance<BrawnConfig>();
  
  protected override string Id => "Brawn";
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
    Player.GetDamage(DamageClass.Melee) *= 1.00f + (Value * Config.Damage);
    Player.pickSpeed *= 1.00f + (Value * Config.PickSpeed);
    //diminish
    Player.wingTimeMax += (int)(Player.wingTimeMax * Config.MaxWingTime * Value);
  }
}
