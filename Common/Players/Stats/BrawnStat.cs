// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class BrawnStat : BaseStat
{
  private BrawnConfig Config => ModContent.GetInstance<BrawnConfig>();

  protected override List<object> DescriptionArgs => new();
  public override string Id => "Brawn";

  [JITWhenModsEnabled("CalamityMod")]
  private void ModifyCalamityPlayer(Player player)
  {
    var modPlayer = player.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>();

    player.GetDamage<CalamityMod.RogueDamageClass>() *= 1.00f + Value * Config.RogueDamage;
    modPlayer.rogueVelocity *= 1.00f + Value * Config.RogueVelocity;
  }

  public override void ModifyPlayer(Player player)
  {
    player.GetDamage(DamageClass.Melee) *= 1.00f + Value * Config.Damage;
    player.pickSpeed *= 1.00f + Value * Config.PickSpeed;
    //diminish
    player.wingTimeMax += (int)(player.wingTimeMax * Config.MaxWingTime * Value);

    if (!LevelPlus.Instance.IsCalamityModLoaded) return;
    ModifyCalamityPlayer(player);
  }
}
