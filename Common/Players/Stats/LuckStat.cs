// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class LuckPlayer : StatPlayer
{
  private static LuckConfig Config => ModContent.GetInstance<LuckConfig>();

  protected override string Id => "Luck";
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

  //diminish
  public override void PostUpdateMiscEffects()
  {
    Player.GetCritChance(DamageClass.Melee) += Value * Config.Crit;
    Player.GetCritChance(DamageClass.Ranged) += Value * Config.Crit;
    Player.GetCritChance(DamageClass.Magic) += Value * Config.Crit;
    Player.GetCritChance(DamageClass.Summon) += Value * Config.Crit;
    Player.luck += Value * Config.TerrariaLuck;
  }

  public override void OnConsumeMana(Item item, int manaConsumed)
  {
    //diminish
    if (Value * Config.ManaReductionChance * 100 > new System.Random(System.DateTime.Now.Millisecond).Next(1, 101))
    {
      Player.statMana += manaConsumed;
    }
  }

  public override bool CanConsumeAmmo(Item weapon, Item ammo)
  {
    //diminish
    return Value * Config.AmmoReductionChance * 100 > new System.Random(System.DateTime.Now.Millisecond).Next(1, 101);
  }
}

