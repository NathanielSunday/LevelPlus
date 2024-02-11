// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class IntellectPlayer : BaseStat
{
  private IntellectConfig Config => ModContent.GetInstance<IntellectConfig>();

  protected override object[] DescriptionArgs => new object[] { };
  public override string Id => "Intellect";

  public override bool IsLoadingEnabled(Mod mod) => true;

  public override void Load(Mod mod)
  {
    ModContent.GetInstance<StatPlayer>().RegisterStat(this);
  }
  
  public override void SaveData(TagCompound tag)
  {
  }

  public override void LoadData(TagCompound tag)
  {
  }

  public override void ModifyPlayer(ref Player player)
  {
    player.statManaMax2 += Value * Config.Mana;
    player.manaRegen += (int)(player.manaRegen * Value * Config.ManaRegen);
    player.GetDamage(DamageClass.Magic) *= 1.00f + (Value * Config.Damage);
  }
}
