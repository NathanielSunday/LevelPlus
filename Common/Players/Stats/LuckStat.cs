// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class LuckStat : BaseStat
{
  private System.Random rng;

  private float Crit => Value * StatConfig.Instance.Luck_Crit; //dim
  private float Luck => Value * StatConfig.Instance.Luck_TerrariaLuck; //dim
  private float Mana => Value * StatConfig.Instance.Luck_ManaReductionChance * 100; //dim
  private float Ammo => Value * StatConfig.Instance.Luck_AmmoReductionChance * 100; //dim

  protected override object[] DescriptionArgs => new object[]
    { Crit * 100, Luck * 100, Mana, Ammo };

  public override string Id => "Luck";
  public override Color UIColor => Color.Green;

  public override void Load(TagCompound tag)
  {
    rng = new System.Random(System.DateTime.Now.Millisecond);
  }

  //diminish
  public override void ModifyPlayer(Player player)
  {
    player.GetCritChance(DamageClass.Melee) += Crit;
    player.GetCritChance(DamageClass.Magic) += Crit;
    player.GetCritChance(DamageClass.Ranged) += Crit;
    player.GetCritChance(DamageClass.Summon) += Crit;
    player.luck += Luck;
  }

  public override void ModifyOnConsumeMana(Player player, Item item, int manaConsumed)
  {
    // If the value is less than the 0-99, then do not rebate mana
    // 0 is not less than 0, so at no points invested, this is still correct
    //diminish
    if (Mana < rng.Next(100)) return;
    player.statMana += manaConsumed;
  }

  public override bool CanConsumeAmmo(Item weapon, Item ammo)
  {
    // If the value is less than the 0-99, then ammo can be consumed
    // 0 is not less than 0, so at no points invested, this is still true
    //diminish
    return Ammo < rng.Next(100);
  }
}

