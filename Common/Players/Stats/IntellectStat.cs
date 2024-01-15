// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players;

public class IntellectPlayer : StatPlayer
{
  private static IntellectConfig Config => ModContent.GetInstance<IntellectConfig>();

  protected override string Id => "Intellect";
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
    Player.statManaMax2 += Value * Config.Mana;
    Player.manaRegen += (int)(Player.manaRegen * Value * Config.ManaRegen);
    Player.GetDamage(DamageClass.Magic) *= 1.00f + (Value * Config.Damage);
  }
}
