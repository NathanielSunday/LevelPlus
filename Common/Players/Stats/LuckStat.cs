// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class LuckPlayer : BaseStat
{
  private static LuckConfig Config => ModContent.GetInstance<LuckConfig>();

  private System.Random rand;

  protected override object[] DescriptionArgs => [];
  public override string Id => "Luck";

  public override bool IsLoadingEnabled(Mod mod) => true;

  public override void Load(Mod mod)
  {
    StatPlayer.RegisterStat(this);
    rand = new System.Random(System.DateTime.Now.Millisecond);
  }

  public override void SaveData(TagCompound tag)
  {
  }

  public override void LoadData(TagCompound tag)
  {
  }

  //diminish
  public override void ModifyPlayer()
  {
    Player.GetCritChance(DamageClass.Melee) += Value * Config.Crit;
    Player.GetCritChance(DamageClass.Ranged) += Value * Config.Crit;
    Player.GetCritChance(DamageClass.Magic) += Value * Config.Crit;
    Player.GetCritChance(DamageClass.Summon) += Value * Config.Crit;
    Player.luck += Value * Config.TerrariaLuck;
  }

  public override void ModifyOnConsumeMana(Item item, int manaConsumed)
  {
    //diminish
    if (Value * Config.ManaReductionChance * 100 > rand.Next(1, 101))
    {
      Player.statMana += manaConsumed;
    }
  }

  public override bool CanConsumeAmmo(Item weapon, Item ammo)
  {
    // If the value is less than the 0-99, then ammo can be consumed
    // 0 is not less than 0, so at no points invested, this is still true
    //diminish
    return Value * Config.AmmoReductionChance * 100 < rand.Next(0, 100);
  }
}

