using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs;

// ReSharper disable file InconsistentNaming
public class StatConfig : ModConfig
{
  public override ConfigScope Mode => ConfigScope.ServerSide;
  public static StatConfig Instance => ModContent.GetInstance<StatConfig>();

  [Header("$Mods.LevelPlus.Stats.Endurance.DisplayName")] [DefaultValue(5)]
  public int Endurance_Life;

  [DefaultValue(2)] public int Endurance_Defense;
  [DefaultValue(0.05f)] public float Endurance_LifeRegen; //dim

  [Header("$Mods.LevelPlus.Stats.Brawn.DisplayName")] [DefaultValue(0.01f)]
  public float Brawn_Damage;

  [DefaultValue(0.02f)] public float Brawn_MaxWingTime; //dim
  [DefaultValue(0.01f)] public float Brawn_PickSpeed;

  [Header("$Mods.LevelPlus.Stats.Deft.DisplayName")] [DefaultValue(0.01f)]
  public float Deft_Damage;

  [DefaultValue(0.01f)] public float Deft_MaxSpeed;
  [DefaultValue(0.02f)] public float Deft_Acceleration; //dim

  [Header("$Mods.LevelPlus.Stats.Intellect.DisplayName")] [DefaultValue(0.01f)]
  public float Intellect_Damage;

  [DefaultValue(2)] public int Intellect_Mana;
  [DefaultValue(0.02f)] public float Intellect_ManaRegen;

  [Header("$Mods.LevelPlus.Stats.Charm.DisplayName")] [DefaultValue(0.01f)]
  public float Charm_Damage;

  [DefaultValue(20)] public int Charm_MinionCost;
  [DefaultValue(30)] public int Charm_SentryCost;
  [DefaultValue(1)] public int Charm_Fishing;

  [Header("$Mods.LevelPlus.Stats.Luck.DisplayName")] [DefaultValue(0.05f)]
  public float Luck_Crit; //dim

  [DefaultValue(0.03f)] public float Luck_AmmoReductionChance; //dim
  [DefaultValue(0.03f)] public float Luck_ManaReductionChance; //dim
  [DefaultValue(0.05f)] public float Luck_TerrariaLuck; //dim

  [Header("$Mods.LevelPlus.Stats.Adroit.DisplayName")] [DefaultValue(0.02f)]
  public float Adroit_PlacementSpeed;

  [DefaultValue(25)] public int Adroit_RangeCost;
}