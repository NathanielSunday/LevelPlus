// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Players.Stats;

[JITWhenModsEnabled("CalamityMod")]
public class StealthStat : BaseStat
{
  private float Damage => Value * StatConfig.Instance.Stealth_Damage;
  private float Velocity => Value * StatConfig.Instance.Stealth_Velocity;
  private float StealthMax => Value * StatConfig.Instance.Stealth_StealthMax;
  private float StealthDamage => Value * StatConfig.Instance.Stealth_StealthDamage;

  protected override object[] DescriptionArgs => new object[]
    { Damage * 100, Velocity * 100, StealthDamage * 100, StealthMax * 100 };

  public override string Id => "Stealth";
  
  public override void ModifyPlayer(Player player)
  {
    var modPlayer = player.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>();
    
    modPlayer.rogueStealthMax *= 1.00f + StealthMax;
    modPlayer.rogueVelocity *= 1.00f + Velocity;
    player.GetDamage<CalamityMod.StealthDamageClass>() *= 1.00f + StealthDamage;
    player.GetDamage<CalamityMod.RogueDamageClass>() *= 1.00f + Damage;
  }
}
