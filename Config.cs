using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace levelplus
{
    public class levelplusConfig : ModConfig
    {
        // You MUST specify a ConfigScope.
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static levelplusConfig Instance => ModContent.GetInstance<levelplusConfig>();
   
        // --- //
        // Constitution //
        [Header("Constitution")]
        [Label("Constitution: Health per Level")]
        [Tooltip("How much HP the player gets per Level")]
        [Range(0, 5)]
        [DefaultValue(2)] 
        public int HealthPerLevel;  
        //Used: levelplusConfig.Instance.HealthPerLevel
        
        [Label("Constitution: Health per Point")]
        [Tooltip("How much HP the player gets per Point")]
        [Range(0, 25)]
        [DefaultValue(5)] 
        public int HealthPerPoint;  
        //Used: levelplusConfig.Instance.HealthPerPoint
        
        [Label("Constitution: LifeRegen")]
        [Tooltip("How many Points needed to be spent for 1 LifeReg")]
        [Range(1, 30)]
        [DefaultValue(20)] 
        public int HRegenPerPoint;  
        //Used: levelplusConfig.Instance.HRegenPerPoint
        
        [Label("Constitution: Defense")]
        [Tooltip("How many Points needed to be spent for 1 Defense")]
        [Range(1, 30)]
        [DefaultValue(3)] 
        public int DefensePerPoint;  
        //Used: levelplusConfig.Instance.DefensePerPoint
          
        // --- //
        // Intelligence //
        [Header("Intelligence")]
        [Label("Intelligence:  Magic Damage")]
        [Tooltip("How much MagicDamage the player gets per Level")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.01f)]
        [DefaultValue(0.01f)]
        public float MagicDamagePerPoint; 
        //Used: levelplusConfig.Instance.MagicDamagePerPoint
        
        [Label("Intelligence: Magic Crit")]
        [Tooltip("How many Points needed to be spent for 1% Magic Crit")]
        [Range(1, 30)]
        [DefaultValue(15)] 
        public int MagicCritPerPoint;  
        //Used: levelplusConfig.Instance.MagicCritPerPoint
        
        // --- //
        // Strength //
        [Header("Strength")]
        [Label("Strength: Melee Damage")]
        [Tooltip("How much MeleeDamage the player gets per Level")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.01f)]
        [DefaultValue(0.01f)]
        public float MeleeDamagePerPoint; 
        //Used: levelplusConfig.Instance.MeleeDamagePerPoint
        
        [Label("Strength: Melee Crit")]
        [Tooltip("How many Points needed to be spent for 1% Melee Crit")]
        [Range(1, 30)]
        [DefaultValue(15)] 
        public int MeleeCritPerPoint;  
        //Used: levelplusConfig.Instance.MeleeCritPerPoint
        
        // --- //
        // Dexterity //
        [Header("Dexterity")]
        [Label("Dexterity: Ranged Damage")]
        [Tooltip("How much RangedDamage the player gets per Level")]
        [Slider]
        [Range(0.00f, 0.25f)]
        [Increment(0.01f)]
        [DefaultValue(0.01f)]
        public float RangedDamagePerPoint; 
        //Used: levelplusConfig.Instance.RangedDamagePerPoint
        
        [Label("Dexterity: Ranged Crit")]
        [Tooltip("How many Points needed to be spent for 1% Ranged Crit")]
        [Range(1, 30)]
        [DefaultValue(15)] 
        public int RangedCritPerPoint;  
        //Used: levelplusConfig.Instance.RangedCritPerPoint
        
        // --- //
        // Charisma //
        [Header("Charisma")]
        [Label("Charisma: Summon Damage")]
        [Tooltip("How much SummonDamage the player gets per Level")]
        [Slider]
        [Range(0.00f, 0.25f)]
        [Increment(0.01f)]
        [DefaultValue(0.01f)]
        public float SummonDamagePerPoint; 
        //Used: levelplusConfig.Instance.SummonDamagePerPoint
        
        [Label("Charisma: Summon Crit")]
        [Tooltip("How many Points needed to be spent for 1% Summon Crit")]
        [Range(1, 30)]
        [DefaultValue(15)] 
        public int SummonCritPerPoint;  
        //Used: levelplusConfig.Instance.SummonCritPerPoint
        
        
        // --- //
        // Animalia //
        [Header("Animalia")]
        [Label("Animalia: Fishing Skill")]
        [Tooltip("How much Fishing Skill the player gets per Level")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.005f)]
        [DefaultValue(0.02f)]
        public float FishSkillPerPoint; 
        //Used: levelplusConfig.Instance.FishSkillPerPoint
        
        [Label("Animalia: Minion Knockback")]
        [Tooltip("How much Minion Knockback the player gets per Level")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.005f)]
        [DefaultValue(0.02f)]
        public float MinionKnockBack; 
        //Used: levelplusConfig.Instance.MinionKnockBack
        
        [Label("Animalia: Max Minions")]
        [Tooltip("How many Points needed to be spent for 1 Minion Capacity")]
        [Range(1, 30)]
        [DefaultValue(20)] 
        public int MinionPerPoint;  
        //Used: levelplusConfig.Instance.MinionPerPoint
        
        
        // --- //
        // Excavation //
        [Header("Excavation")]
        [Label("Excavation: Pick Speed")]
        [Tooltip("How much Pick Speed the player gets per Level")]
        [Slider]
        [Range(0.00f, 0.05f)]
        [Increment(0.001f)]
        [DefaultValue(0.01f)]
        public float PickSpeedPerPoint; 
        //Used: levelplusConfig.Instance.PickSpeedPerPoint
        
        [Label("Excavation: Building Speed")]
        [Tooltip("How much Building Speed the player gets per Level")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.005f)]
        [DefaultValue(0.02f)]
        public float BuildSpeedPerPoint; 
        //Used: levelplusConfig.Instance.BuildSpeedPerPoint
        
        [Label("Excavation: Block Range")]
        [Tooltip("How many Points needed to be spent for 1 Placement Range")]
        [Range(1, 30)]
        [DefaultValue(20)] 
        public int RangePerPoint;  
        //Used: levelplusConfig.Instance.RangePerPoint
        
        // --- //
        // Mobility //
        [Header("Mobility")]
        [Label("Mobility: Run Speed")]
        [Tooltip("How much Run Speed the player gets per Level")]
        [Slider]
        [Range(0.00f, 0.05f)]
        [Increment(0.001f)]
        [DefaultValue(0.01f)]
        public float RunSpeedPerPoint; 
        //Used: levelplusConfig.Instance.RunSpeedPerPoint
        
        [Label("Mobility: Acceleration")]
        [Tooltip("How much Acceleration the player gets per Level")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.005f)]
        [DefaultValue(0.02f)]
        public float AccelPerPoint; 
        //Used: levelplusConfig.Instance.AccelPerPoint
        
        [Label("Mobility: Wing Time")]
        [Tooltip("How much Acceleration the player gets per Level")]
        [Slider]
        [Range(0.00f, 0.10f)]
        [Increment(0.005f)]
        [DefaultValue(0.02f)]
        public float WingPerPoint;  
        //Used: levelplusConfig.Instance.WingPerPoint
        
        // --- //
        // Luck //
        [Header("Luck")]
        [Label("Luck: Bonus Experience")]
        [Tooltip("How much Bonus Experience the player gets per Level")]
        [Slider]
        [Range(0.00f, 0.05f)]
        [Increment(0.001f)]
        [DefaultValue(0.01f)]
        public float XPPerPoint; 
        //Used: levelplusConfig.Instance.XPPerPoint
        
        [Label("Luck: Ammo Consumption")]
        [Tooltip("How much Points you need to Reach 100%")]
        [Range(50, 500)]
        [DefaultValue(100)] 
        [Increment(10)]
        public int AmmoPerPoint; 
        //Used: levelplusConfig.Instance.AmmoPerPoint
        
        // --- //
        // Mysticism //
        [Header("Mysticism")]
        [Label("Mysticism: Mana per Level")]
        [Tooltip("How much HP the player gets per Level")]
        [Range(0, 5)]
        [DefaultValue(1)] 
        public int ManaPerLevel;  
        //Used: levelplusConfig.Instance.ManaPerLevel
        
        [Label("Mysticism: Mana per Point")]
        [Tooltip("How much HP the player gets per Point")]
        [Range(0, 25)]
        [DefaultValue(2)] 
        public int ManaPerPoint;  
        //Used: levelplusConfig.Instance.ManaPerPoint
        
        [Label("Mysticism: Mana Regen")]
        [Tooltip("How many Points needed to be spent for 1 ManaReg")]
        [Range(0, 30)]
        [DefaultValue(15)] 
        public int ManaRegPerPoint;  
        //Used: levelplusConfig.Instance.ManaRegPerPoint
        
        [Label("Mysticism: Mana Cost")]
        [Tooltip("")]
        [Slider]
        [Range(0.00f, 0.025f)]
        [Increment(0.001f)]
        [DefaultValue(0.005f)]
        public float ManaCostPerPoint; 
        //Used: levelplusConfig.Instance.ManaCostPerPoint

        [Header("Character Stats - (Requires Reload)")]
        [Label("Starting Points")]
        [Tooltip("Points at Level 0")]
        [Range(0, 10)]
        [DefaultValue(3)]
        [ReloadRequired]
        public int PointsBase;
        //Used: levelplusConfig.Instance.PointsBase
        
        [Label("Points per Level")]
        [Tooltip("Statpoint Gain per level")]
        [Range(0, 10)]
        [DefaultValue(3)]
        [ReloadRequired]
        public int PointsPerLevel;
        //Used: levelplusConfig.Instance.PointsPerLevel
        
        
        [Label("Base XP")]
        [Tooltip("Level Up Calculation related")]
        [Range(50, 500)]
        [DefaultValue(100)]
        [Increment(25)]
        [ReloadRequired]
        public int XPBase;
        //Used: levelplusConfig.Instance.XPBase
        
        [Label("XP Increase")]
        [Tooltip("Level Up Calculation relatedl")]
        [Range(50, 500)]
        [DefaultValue(100)]
        [Increment(25)]
        [ReloadRequired]
        public int XPIncrease;
        //Used: levelplusConfig.Instance.XPIncrease
        
        [Label("XP Rate")]
        [Tooltip("Level Up Calculation related")]
        [Slider]
        [Range(1.00f, 5.00f)]
        [Increment(0.5f)]
        [DefaultValue(2.0f)]
        [ReloadRequired]
        public float XPRate;
        //Used: levelplusConfig.Instance.XPRate
        
        [Label("Experience Factor (Mob)")]
        [Tooltip("Enemy HP is divided by this to determine Experience Gain")]
        [Range(1, 10)]
        [DefaultValue(3)] 
        [ReloadRequired]
        public int MobXP;
        //Used: levelplusConfig.Instance.MobXP
        
        [Label("Experience Factor (Boss)")]
        [Tooltip("Enemy HP is divided by this to determine Experience Gain")]
        [Range(1, 10)]
        [DefaultValue(4)] 
        [ReloadRequired]
        public int BossXP;
        //Used: levelplusConfig.Instance.BossXP
        
        [Header("Level Scaling")]
        [Label("Enemy Health")]
        [Tooltip("Multiplies the Health of Enemies per Average level")]
        [Slider]
        [Range(0.0050f, 0.050f)]
        [Increment(0.0050f)]
        [DefaultValue(0.0250f)]
        [ReloadRequired]
        public float ScalingHealth;
        //Used: levelplusConfig.Instance.ScalingHealth
        
        [Label("Enemy Damage")]
        [Tooltip("Multiplies the Damage of Enemies per Average level")]
        [Slider]
        [Range(0.0050f, 0.050f)]
        [Increment(0.0050f)]
        [DefaultValue(0.0250f)]
        [ReloadRequired]
        public float ScalingDamage;
        //Used: levelplusConfig.Instance.ScalingDamage
       
    }
}
