using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus {
    public class LevelPlusConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static LevelPlusConfig Instance => ModContent.GetInstance<LevelPlusConfig>();

        // --- //
        //Level Bonuses
        [Header("Stats")]
        [Label("Level: Health per Level")]
        [Tooltip("How much HP the player gets per Level")]
        [Range(0, 5)]
        [DefaultValue(2)]
        public int HealthPerLevel;

        [Label("Level: Mana per Level")]
        [Tooltip("How much HP the player gets per Level")]
        [Range(0, 5)]
        [DefaultValue(1)]
        public int ManaPerLevel;

        // --- //
        // Constitution //
        [Label("Constitution: Health per Point")]
        [Tooltip("How much HP the player gets per point")]
        [Range(0, 25)]
        [DefaultValue(5)]
        public int HealthPerPoint;

        [Label("Constitution: LifeRegen")]
        [Tooltip("How many Points needed to be spent for 1 LifeReg")]
        [Range(1, 30)]
        [DefaultValue(20)]
        public int HRegenPerPoint;

        [Label("Constitution: Defense")]
        [Tooltip("How many Points needed to be spent for 1 Defense")]
        [Range(1, 30)]
        [DefaultValue(3)]
        public int DefensePerPoint;

        // --- //
        // Intelligence //
        [Label("Intelligence:  Magic Damage")]
        [Tooltip("How much MagicDamage the player gets per point")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.01f)]
        [DefaultValue(0.01f)]
        public float MagicDamagePerPoint;

        [Label("Intelligence: Magic Crit")]
        [Tooltip("How many Points needed to be spent for 1% Magic Crit")]
        [Range(1, 30)]
        [DefaultValue(15)]
        public int MagicCritPerPoint;

        // --- //
        // Strength //
        [Label("Strength: Melee Damage")]
        [Tooltip("How much MeleeDamage the player gets per point")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.01f)]
        [DefaultValue(0.01f)]
        public float MeleeDamagePerPoint;

        [Label("Strength: Melee Crit")]
        [Tooltip("How many Points needed to be spent for 1% Melee Crit")]
        [Range(1, 30)]
        [DefaultValue(15)]
        public int MeleeCritPerPoint;

        // --- //
        // Dexterity //
        [Label("Dexterity: Ranged Damage")]
        [Tooltip("How much RangedDamage the player gets per point")]
        [Slider]
        [Range(0.00f, 0.25f)]
        [Increment(0.01f)]
        [DefaultValue(0.01f)]
        public float RangedDamagePerPoint;

        [Label("Dexterity: Ranged Crit")]
        [Tooltip("How many Points needed to be spent for 1% Ranged Crit")]
        [Range(1, 30)]
        [DefaultValue(15)]
        public int RangedCritPerPoint;

        // --- //
        // Charisma //
        [Label("Charisma: Summon Damage")]
        [Tooltip("How much SummonDamage the player gets per point")]
        [Slider]
        [Range(0.00f, 0.25f)]
        [Increment(0.01f)]
        [DefaultValue(0.01f)]
        public float SummonDamagePerPoint;

        [Label("Charisma: Summon Crit")]
        [Tooltip("How many Points needed to be spent for 1% Summon Crit")]
        [Range(1, 30)]
        [DefaultValue(15)]
        public int SummonCritPerPoint;


        // --- //
        // Animalia //
        [Label("Animalia: Fishing Skill")]
        [Tooltip("How much Fishing Skill the player gets per point")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.005f)]
        [DefaultValue(0.02f)]
        public float FishSkillPerPoint;


        //an update removed minionKB as a stat I can modify, come back to this later
        /*
        [Label("Animalia: Minion Knockback")]
        [Tooltip("How much Minion Knockback the player gets per point")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.005f)]
        [DefaultValue(0.02f)]
        public float MinionKnockBack; 
        */

        [Label("Animalia: Max Minions")]
        [Tooltip("How many Points needed to be spent for 1 Minion Capacity")]
        [Range(1, 30)]
        [DefaultValue(20)]
        public int MinionPerPoint;


        // --- //
        // Excavation //
        [Label("Excavation: Pick Speed")]
        [Tooltip("How much Pick Speed the player gets per point")]
        [Slider]
        [Range(0.00f, 0.05f)]
        [Increment(0.001f)]
        [DefaultValue(0.01f)]
        public float PickSpeedPerPoint;

        [Label("Excavation: Building Speed")]
        [Tooltip("How much Building Speed the player gets per point")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.005f)]
        [DefaultValue(0.02f)]
        public float BuildSpeedPerPoint;

        [Label("Excavation: Block Range")]
        [Tooltip("How many Points needed to be spent for 1 Placement Range")]
        [Range(1, 30)]
        [DefaultValue(20)]
        public int RangePerPoint;

        // --- //
        // Mobility //
        [Label("Mobility: Run Speed")]
        [Tooltip("How much Run Speed the player gets per point")]
        [Slider]
        [Range(0.00f, 0.05f)]
        [Increment(0.001f)]
        [DefaultValue(0.01f)]
        public float RunSpeedPerPoint;

        [Label("Mobility: Acceleration")]
        [Tooltip("How much Acceleration the player gets per point")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.005f)]
        [DefaultValue(0.02f)]
        public float AccelPerPoint;

        [Label("Mobility: Wing Time")]
        [Tooltip("How much Acceleration the player gets per point")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.005f)]
        [DefaultValue(0.02f)]
        public float WingPerPoint;

        // --- //
        // Luck //
        [Label("Luck: Bonus Experience")]
        [Tooltip("How much Bonus Experience the player gets per point")]
        [Slider]
        [Range(0.00f, 0.05f)]
        [Increment(0.001f)]
        [DefaultValue(0.01f)]
        public float XPPerPoint;

        [Label("Luck: Ammo Consumption")]
        [Tooltip("How much Points you need to Reach 100%")]
        [Range(50, 500)]
        [DefaultValue(100)]
        [Increment(10)]
        public int AmmoPerPoint;

        // --- //
        // Mysticism //
        [Label("Mysticism: Mana per Point")]
        [Tooltip("How much Mana the player gets per point")]
        [Range(0, 25)]
        [DefaultValue(2)]
        public int ManaPerPoint;

        [Label("Mysticism: Mana Regen")]
        [Tooltip("How many Points needed to be spent for 1 ManaReg")]
        [Range(0, 30)]
        [DefaultValue(15)]
        public int ManaRegPerPoint;

        [Label("Mysticism: Mana Cost")]
        [Tooltip("The percent of mana reduced per point invested")]
        [Slider]
        [Range(0.00f, 0.025f)]
        [Increment(0.001f)]
        [DefaultValue(0.001f)]
        public float ManaCostPerPoint;

        [SeparatePage]
        [Header("Requires Reload")]
        [Label("Starting Points")]
        [Tooltip("Points at Level 0")]
        [Range(0, 10)]
        [DefaultValue(3)]
        [ReloadRequired]
        public int PointsBase;

        [Label("Points per Level")]
        [Tooltip("Statpoint Gain per level")]
        [Range(0, 10)]
        [DefaultValue(3)]
        [ReloadRequired]
        public int PointsPerLevel;


        [Label("Base XP")]
        [Tooltip("Level Up Calculation related")]
        [Range(50, 500)]
        [DefaultValue(100)]
        [Increment(25)]
        [ReloadRequired]
        public int XPBase;

        [Label("XP Increase")]
        [Tooltip("Level Up Calculation related")]
        [Range(50, 500)]
        [DefaultValue(100)]
        [Increment(25)]
        [ReloadRequired]
        public int XPIncrease;

        [Label("XP Rate")]
        [Tooltip("Level Up Calculation related")]
        [Slider]
        [Range(1.00f, 5.00f)]
        [Increment(0.5f)]
        [DefaultValue(2.0f)]
        [ReloadRequired]
        public float XPRate;

        [Label("Mob Experience")]
        [Tooltip("This is the percentage of mob HP you get in XP")]
        [Slider]
        [Range(0.0f, 1.0f)]
        [Increment(0.05f)]
        [DefaultValue(0.35f)]
        [ReloadRequired]
        public float MobXP;

        [Label("Boss Experience")]
        [Tooltip("This is the percentage of boss HP you get in XP")]
        [Slider]
        [Range(0.0f, 1.0f)]
        [Increment(0.05f)]
        [DefaultValue(0.25f)]
        [ReloadRequired]
        public float BossXP;

        [Label("Scaling")]
        [Tooltip("Turns on mob scaling")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool ScalingEnabled;

        [Label("Enemy Health")]
        [Tooltip("Multiplies the Health of Enemies per Average level")]
        [Slider]
        [Range(0.0050f, 0.050f)]
        [Increment(0.0050f)]
        [DefaultValue(0.0250f)]
        [ReloadRequired]
        public float ScalingHealth;

        [Label("Enemy Damage")]
        [Tooltip("Multiplies the Damage of Enemies per Average level")]
        [Slider]
        [Range(0.0050f, 0.050f)]
        [Increment(0.0050f)]
        [DefaultValue(0.0250f)]
        [ReloadRequired]
        public float ScalingDamage;

        [Label("Commands")]
        [Tooltip("Enables Commands")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool CommandsEnabled;
    }
}
