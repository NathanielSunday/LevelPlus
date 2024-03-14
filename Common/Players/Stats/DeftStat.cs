// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.Configs.Stats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class DeftStat : BaseStat
{
  private DeftConfig Config => ModContent.GetInstance<DeftConfig>();

  protected override List<object> DescriptionArgs => new();
  public override string Id => "Deft";
  public override Color UIColor => Color.Yellow;

  [JITWhenModsEnabled("CalamityMod")]
  private void ModifyCalamityPlayer(Player player)
  {
    var modPlayer = player.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>();

    modPlayer.rogueStealthMax += Value * Config.RogueStealthMax;
    player.GetDamage<CalamityMod.RogueDamageClass>() *= 1.00f + Value * Config.RogueStealthDamage;
  }

  public override void ModifyPlayer(Player player)
  {
    player.GetDamage(DamageClass.Ranged) *= 1.00f + Value * Config.Damage;

    if (!LevelPlus.Instance.IsCalamityModLoaded) return;
    ModifyCalamityPlayer(player);
  }

  public override void ModifyRunSpeeds(Player player)
  {
    player.maxRunSpeed *= 1.00f + Value * Config.MaxSpeed;
    //diminish
    player.runAcceleration *= 1.00f + Value * Config.Acceleration;
  }
}

