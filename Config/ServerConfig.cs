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
    [Label("Scaling")]
    [Tooltip("Turns on mob scaling.")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool Mob_ScalingEnabled;

    [Label("Average Level Scaling")]
    [Tooltip("The percentage in which average level modifies mob scaling.\nIf you don't know what this does, I don't recommend messing with it.")]
    [DefaultValue(0.025f)]
    [ReloadRequired]
    public float Mob_LevelScalar;
    #endregion

    #region Level
    [Header("Level/XP Settings")]
    [Label("XP Loss")]
    [Tooltip("Lose a portion of your XP progress on death (can't lose level)")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool Level_LossEnabled;

    [Label("XP Loss Percentage")]
    [Tooltip("The amount a player should be punished (can't lose level)" +
      "\nDefault is 10%, or 0.10")]
    [Range(0.01f, 1.00f)]
    [DefaultValue(0.10f)]
    [ReloadRequired]
    public float Level_LossAmount;

    [Label("Max Level")]
    [Tooltip("A long awaited feature. This is the max level a player can level to." +
      "\nNote:" +
      "\nA player will be automatically validated when joining servers, so if your level" +
      "\nis higher than what a server allows, your player will lose stats applied." +
      "\nThis does NOT mean you lose your XP, however. If you go on another server that allows" +
      "\na higher level, you will be able to reallot your rightfully earned points.")]
    [Range(10, 500)]
    [DefaultValue(100)]
    [ReloadRequired]
    public int Level_MaxLevel;

    [Label("Points Per Level")]
    [Tooltip("The amount of points awarded per level")]
    [DefaultValue(3)]
    [ReloadRequired]
    public int Level_Points;

    [Label("Starting Points")]
    [Tooltip("The amount of points you start with")]
    [DefaultValue(3)]
    [ReloadRequired]
    public int Level_StartingPoints;

    [Label("HP")]
    [Tooltip("The amount of max HP awarded per level")]
    [DefaultValue(1)]
    [ReloadRequired]
    public int Level_Health;

    [Label("Mana")]
    [Tooltip("The amount of max mana awarded per level")]
    [DefaultValue(0)]
    [ReloadRequired]
    public int Level_Mana;
    #endregion


    [Header("Constitution Settings")]
    [Label("HP")]
    [Tooltip("The amount of max HP awarded per point")]
    [DefaultValue(5)]
    [ReloadRequired]
    public const int HealthPerPoint = 5;
    public const int DefensePerPoint = 2;
    public const int HRegenPerPoint = 1; //dim

    // Intelligence //
    public const float MagicDamagePerPoint = .01f;
    public const int MagicCritPerPoint = 5; //dim

    // Strength //
    public const float MeleeDamagePerPoint = .01f;
    public const int MeleeCritPerPoint = 5; //dim

    // Dexterity //
    public const float RangedDamagePerPoint = .01f;
    public const int RangedCritPerPoint = 5; //dim

    // Charisma //
    public const float SummonDamagePerPoint = .01f;
    public const int SummonCritPerPoint = 5; //dim

    // Animalia //
    public const float FishSkillPerPoint = .02f;
    //this is obsolete currently
    //public static float MinionKnockBack = .02f; 
    public const int MinionPerPoint = 1; //dim

    // Excavation //
    public const float PickSpeedPerPoint = .01f;
    public const float BuildSpeedPerPoint = .02f;
    public const int RangePerPoint = 1;

    // Mobility //
    public const float RunSpeedPerPoint = .01f;
    public const float AccelPerPoint = .02f;
    public const float WingPerPoint = .02f;

    // Luck //
    public const float XPPerPoint = .01f; //dim
    public const float AmmoPerPoint = .05f;

    // Mysticism //
    public const int ManaPerPoint = 2;
    public const int ManaRegPerPoint = 1; //dim
    public const float ManaCostPerPoint = .01f; //dim

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
