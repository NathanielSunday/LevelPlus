// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class IntellectPlayer : BaseStat
{
  private IntellectConfig Config => ModContent.GetInstance<IntellectConfig>();

  protected override object[] DescriptionArgs => [];
  public override string Id => "Intellect";

  public override bool IsLoadingEnabled(Mod mod) => true;

  public override void Load(Mod mod)
  {
    StatPlayer.RegisterStat(this);
  }
  
  public override void SaveData(TagCompound tag)
  {
  }

  public override void LoadData(TagCompound tag)
  {
  }

  public override void ModifyPlayer()
  {
    Player.statManaMax2 += Value * Config.Mana;
    Player.manaRegen += (int)(Player.manaRegen * Value * Config.ManaRegen);
    Player.GetDamage(DamageClass.Magic) *= 1.00f + (Value * Config.Damage);
  }
}
