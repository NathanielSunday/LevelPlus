// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class LuckStat : BaseStat
{
  private static LuckConfig Config => ModContent.GetInstance<LuckConfig>();

  private System.Random rng;

  protected override List<object> DescriptionArgs => new();
  public override string Id => "Luck";

  public override void Load(TagCompound tag)
  {
    rng = new System.Random(System.DateTime.Now.Millisecond);
  }

  //diminish
  public override void ModifyPlayer(Player player)
  {
    player.GetCritChance(DamageClass.Melee) += Value * Config.Crit;
    player.GetCritChance(DamageClass.Ranged) += Value * Config.Crit;
    player.GetCritChance(DamageClass.Magic) += Value * Config.Crit;
    player.GetCritChance(DamageClass.Summon) += Value * Config.Crit;
    player.luck += Value * Config.TerrariaLuck;
  }

  public override void ModifyOnConsumeMana(Player player, Item item, int manaConsumed)
  {
    // If the value is less than the 0-99, then do not rebate mana
    // 0 is not less than 0, so at no points invested, this is still correct
    //diminish
    if (Value * Config.ManaReductionChance * 100 < rng.Next(100)) return;
    player.statMana += manaConsumed;
  }

  public override bool CanConsumeAmmo(Item weapon, Item ammo)
  {
    // If the value is less than the 0-99, then ammo can be consumed
    // 0 is not less than 0, so at no points invested, this is still true
    //diminish
    return Value * Config.AmmoReductionChance * 100 < rng.Next(100);
  }
}

