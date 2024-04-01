// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

public class BrawnStat : BaseStat
{
  //diminish
  private float WingTimeMax => StatConfig.Instance.Brawn_MaxWingTime * Value;
  private float Damage => Value * StatConfig.Instance.Brawn_Damage;
  private float PickSpeed => Value * StatConfig.Instance.Brawn_PickSpeed;
  private float RogueDamage => Value * CalamityStatConfig.Instance.Brawn_RogueDamage;
  private float RogueVelocity => Value * CalamityStatConfig.Instance.Brawn_RogueVelocity;

  protected override object[] DescriptionArgs => new object[]
    { Damage * 100, PickSpeed * 100, WingTimeMax * 100, RogueDamage * 100, RogueVelocity * 100 };

  protected override string DescriptionKey =>
    base.DescriptionKey + (LevelPlus.Instance.IsCalamityModLoaded ? "Calamity" : "");

  public override string Id => "Brawn";
  public override Color UIColor => Color.Red;

  [JITWhenModsEnabled("CalamityMod")]
  private void ModifyCalamityPlayer(Player player)
  {
    var modPlayer = player.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>();

    player.GetDamage<CalamityMod.RogueDamageClass>() *= 1.00f + RogueDamage;
    modPlayer.rogueVelocity *= 1.00f + RogueVelocity;
  }

  public override void ModifyPlayer(Player player)
  {
    player.GetDamage(DamageClass.Melee) *= 1.00f + Damage;
    player.pickSpeed *= 1.00f + PickSpeed;
    player.wingTimeMax += (int)(player.wingTimeMax * WingTimeMax);

    if (!LevelPlus.Instance.IsCalamityModLoaded) return;
    ModifyCalamityPlayer(player);
  }
}
