// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class IntellectStat : BaseStat
{
  private int Mana => Value * StatConfig.Instance.Intellect_Mana;
  private float ManaRegen => Value * StatConfig.Instance.Intellect_ManaRegen; //dim
  private float Damage => Value * StatConfig.Instance.Intellect_Damage;

  protected override object[] DescriptionArgs => new object[]
    { Damage * 100, Mana, ManaRegen * 100 };

  public override string Id => "Intellect";

  public override void ModifyPlayer(Player player)
  {
    player.statManaMax2 += Mana;
    player.manaRegen += (int)(player.manaRegen * ManaRegen);
    player.GetDamage(DamageClass.Magic) *= 1.00f + Damage;
  }
}
