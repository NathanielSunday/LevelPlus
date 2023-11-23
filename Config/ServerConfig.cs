// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus.Config {
  public class ServerConfig : ModConfig {
    public override ConfigScope Mode => ConfigScope.ServerSide;
    public static ServerConfig Instance => ModContent.GetInstance<ServerConfig>();

    #region Mob
    [Header("Mob Settings")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool Mob_ScalingEnabled;

    [DefaultValue(0.025f)]
    [ReloadRequired]
    public float Mob_LevelScalar;
    #endregion

    #region Level
    [Header("Level/XP Settings")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool Level_LossEnabled;

    [Range(0.01f, 1.00f)]
    [DefaultValue(0.10f)]
    [ReloadRequired]
    public float Level_LossAmount;

    [Range(10, 500)]
    [DefaultValue(100)]
    [ReloadRequired]
    public int Level_MaxLevel;

    [DefaultValue(3)]
    [ReloadRequired]
    public int Level_Points;

    [DefaultValue(3)]
    [ReloadRequired]
    public int Level_StartingPoints;

    [DefaultValue(1)]
    [ReloadRequired]
    public int Level_Health;

    [DefaultValue(0)]
    [ReloadRequired]
    public int Level_Mana;
    #endregion

    #region Endurance
    [Header("Endurance Settings")]
    [Label("HP")]
    [Tooltip("The amount of max HP awarded per point")]
    [DefaultValue(5)]
    [ReloadRequired]
    public const int Endurance_Health = 5;
    public const int Endurance_Defense = 2;
    public const int Endurance_Regen = 1; //dim
    #endregion

    #region Strength
    // Strength //
    public const float MeleeDamagePerPoint = .01f;
    public const int MeleeCritPerPoint = 5; //dim
    #endregion

    #region Dexterity
    // Dexterity //
    public const float RangedDamagePerPoint = .01f;
    public const int RangedCritPerPoint = 5; //dim
    #endregion

    #region Intelligence
    [Header("Intelligence Settings")]
    // Intelligence //
    public const float Intelligence_Damage = .01f;
    public const int Intelligence_Crit = 5; //dim
    #endregion

    #region Wisdom
    // Mysticism //
    public const int ManaPerPoint = 2;
    public const int RegenPerPoint = 1; //dim
    public const float ManaCostPerPoint = .01f; //dim
    #endregion

    #region Luck
    // Luck //
    public const float XPPerPoint = .01f; //dim
    public const float AmmoPerPoint = .05f;
    //luck 
    #endregion

    #region Charisma
    // Charisma //
    public const float SummonDamagePerPoint = .01f;
    public const int SummonCritPerPoint = 5; //dim
    #endregion

    #region Animalia
    // Animalia //
    public const float FishSkillPerPoint = .02f;
    //this is obsolete currently
    //public static float MinionKnockBack = .02f; 
    public const int MinionPerPoint = 1; //dim
    #endregion

    #region Excavation
    // Excavation //
    public const float PickSpeedPerPoint = .01f;
    public const float BuildSpeedPerPoint = .02f;
    public const int RangePerPoint = 1;
    #endregion

    #region Mobility
    // Mobility //
    public const float RunSpeedPerPoint = .01f;
    public const float AccelPerPoint = .02f;
    
    #endregion

    #region Commands
    [SeparatePage]
    [Header("Commands")]
    [Label("Enabled")]
    [Tooltip("Enables commands and disables player validation.")]
    [DefaultValue(false)]
    [ReloadRequired]
    public bool Commands_Enabled;
    #endregion
  }
}
