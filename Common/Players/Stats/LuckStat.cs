// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs.Stats;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players.Stats;

public class LuckPlayer : BaseStat
{
  private static LuckConfig Config => ModContent.GetInstance<LuckConfig>();

  private System.Random rand;

  protected override object[] DescriptionArgs => new object[] { };
  public override string Id => "Luck";

  public override bool IsLoadingEnabled(Mod mod) => true;

  public override void Load(Mod mod)
  {
    ModContent.GetInstance<StatPlayer>().RegisterStat(this);
    rand = new System.Random(System.DateTime.Now.Millisecond);
  }

  public override void SaveData(TagCompound tag)
  {
  }

  public override void LoadData(TagCompound tag)
  {
  }

  //diminish
  public override void ModifyPlayer(ref Player player)
  {
    player.GetCritChance(DamageClass.Melee) += Value * Config.Crit;
    player.GetCritChance(DamageClass.Ranged) += Value * Config.Crit;
    player.GetCritChance(DamageClass.Magic) += Value * Config.Crit;
    player.GetCritChance(DamageClass.Summon) += Value * Config.Crit;
    player.luck += Value * Config.TerrariaLuck;
  }

  public override void ModifyOnConsumeMana(ref Player player, Item item, int manaConsumed)
  {
    //diminish
    if (Value * Config.ManaReductionChance * 100 > rand.Next(1, 101))
    {
      player.statMana += manaConsumed;
    }
  }

  public override bool CanConsumeAmmo(Item weapon, Item ammo)
  {
    //diminish
    return Value * Config.AmmoReductionChance * 100 > rand.Next(1, 101);
  }
}

