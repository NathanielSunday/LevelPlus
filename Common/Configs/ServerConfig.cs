// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs {
  public class ServerConfig : ModConfig {
    public override ConfigScope Mode => ConfigScope.ServerSide;
    public static ServerConfig Instance => ModContent.GetInstance<ServerConfig>();

    #region Mob
    [Header("Mob Settings")]
    [DefaultValue(true)]
    public bool Mob_ScalingEnabled;
    [DefaultValue(0.025f)]
    public float Mob_LevelScalar;
    #endregion
    #region Level
    [Header("Level/XP Settings")]
    [DefaultValue(true)]
    public bool Level_LossEnabled;
    [Range(0.01f, 1.00f)]
    [DefaultValue(0.10f)]
    public float Level_LossAmount;
    [Range(10, 500)]
    [DefaultValue(100)]
    public int Level_MaxLevel;
    [DefaultValue(2)]
    public int Level_Points;
    [DefaultValue(3)]
    public int Level_StartingPoints;
    [DefaultValue(1)]
    public int Level_Health;
    [DefaultValue(0)]
    public int Level_Mana;
    #endregion
    #region Endurance
    [Header("Endurance Settings")]
    [Label("HP")]
    [Tooltip("The amount of max HP awarded per point")]
    [DefaultValue(5)]
    public int Endurance_Health;
    [DefaultValue(2)]
    public int Endurance_Defense;
    [DefaultValue(1.0f)]
    public float Endurance_Regen; //dim
    #endregion
    #region Strength
    [Header("Strength Settings")]
    [DefaultValue(0.01f)]
    public float Strength_Damage;
    [DefaultValue(5)]
    public int Strength_Crit; //dim
    #endregion
    #region Dexterity
    // Dexterity //
    public float RangedDamagePerPoint = .01f;
    public int RangedCritPerPoint = 5; //dim
    #endregion
    #region Intelligence
    [Header("Intelligence Settings")]
    // Intelligence //
    public float Intelligence_Damage = .01f;
    public int Intelligence_Crit = 5; //dim
    #endregion
    #region Wisdom
    // Mysticism //
    public int ManaPerPoint = 2;
    public int RegenPerPoint = 1; //dim
    public float ManaCostPerPoint = .01f; //dim
    #endregion
    #region Luck
    // Luck //
    public float XPPerPoint = .01f; //dim
    public float AmmoPerPoint = .05f;
    //luck 
    #endregion
    #region Charisma
    // Charisma //
    public float SummonDamagePerPoint = .01f;
    public int SummonCritPerPoint = 5; //dim
    #endregion
    #region Animalia
    // Animalia //
    public float FishSkillPerPoint = .02f;
    //this is obsolete currently
    //public static float MinionKnockBack = .02f; 
    public int MinionPerPoint = 1; //dim
    #endregion
    #region Excavation
    // Excavation //
    public float PickSpeedPerPoint = .01f;
    public float BuildSpeedPerPoint = .02f;
    public int RangePerPoint = 1;
    #endregion
    #region Mobility
    // Mobility //
    public float RunSpeedPerPoint = .01f;
    public float AccelPerPoint = .02f;
    #endregion
    #region Commands
    [SeparatePage]
    [Header("Commands")]
    [Label("Enabled")]
    [Tooltip("Enables commands and disables player validation.")]
    [DefaultValue(false)]
    public bool Commands_Enabled;
    #endregion
  }
}
