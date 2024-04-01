// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class DeftStat : BaseStat
{
  //diminish
  private float Acceleration => Value * StatConfig.Instance.Deft_Acceleration;
  private float Damage => Value * StatConfig.Instance.Deft_Damage;
  private float MaxRunSpeed => Value * StatConfig.Instance.Deft_MaxSpeed;
  private float RogueStealthMax => Value * CalamityStatConfig.Instance.Deft_RogueStealthMax;
  private float RogueStealthDamage => Value * CalamityStatConfig.Instance.Deft_RogueStealthDamage;

  protected override object[] DescriptionArgs => new object[]
    { Damage * 100, MaxRunSpeed * 100, Acceleration * 100, RogueStealthDamage * 100, RogueStealthMax * 100 };

  protected override string DescriptionKey =>
    base.DescriptionKey + (LevelPlus.Instance.IsCalamityModLoaded ? "Calamity" : "");

  public override string Id => "Deft";
  public override Color UIColor => Color.Yellow;

  [JITWhenModsEnabled("CalamityMod")]
  private void ModifyCalamityPlayer(Player player)
  {
    var modPlayer = player.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>();

    modPlayer.rogueStealthMax *= 1.00f + RogueStealthMax;
    player.GetDamage<CalamityMod.RogueDamageClass>() *= 1.00f + RogueStealthDamage;
  }

  public override void ModifyPlayer(Player player)
  {
    player.GetDamage(DamageClass.Ranged) *= 1.00f + Damage;

    if (!LevelPlus.Instance.IsCalamityModLoaded) return;
    ModifyCalamityPlayer(player);
  }

  public override void ModifyRunSpeeds(Player player)
  {
    player.maxRunSpeed *= 1.00f + MaxRunSpeed;
    player.runAcceleration *= 1.00f + Acceleration;
  }
}

