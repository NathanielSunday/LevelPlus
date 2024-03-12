// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class IntellectStat : BaseStat
{
  private IntellectConfig Config => ModContent.GetInstance<IntellectConfig>();

  protected override List<object> DescriptionArgs => new();
  public override string Id => "Intellect";

  public override void ModifyPlayer(Player player)
  {
    player.statManaMax2 += Value * Config.Mana;
    player.manaRegen += (int)(player.manaRegen * Value * Config.ManaRegen);
    player.GetDamage(DamageClass.Magic) *= 1.00f + (Value * Config.Damage);
  }
}
