using System;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Config;

[BackgroundColor(55, 15, 85, 190)]
public partial class PlayConfiguration : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [Header("StatsHeader")]
    [Expand(false)]
    [BackgroundColor(255, 255, 0)]
    [LabelKey("$Mods.LevelPlus.Stats.Endurance.DisplayName")]
    public EnduranceConfig Endurance { get; set; } = new();

    public class EnduranceConfig
    {
        [Slider]
        [Range(0, 20)]
        [BackgroundColor(255, 255, 0)]
        public int Life { get; set; } = 5;

        [Slider]
        [Range(0, 20)]
        [BackgroundColor(255, 255, 0)]
        public int Defense { get; set; } = 2;

        [Range(0, 0.25f)]
        [BackgroundColor(255, 255, 0)]
        public float LifeRegen { get; set; } = 0.05f;

        public override int GetHashCode() => HashCode.Combine(Life, Defense, LifeRegen);
    }

    
    [Expand(false)]
    [BackgroundColor(255, 0, 0)]
    [LabelKey("$Mods.LevelPlus.Stats.Brawn.DisplayName")]
    public BrawnConfig Brawn { get; set; } = new();

    public class BrawnConfig
    {
        [Range(0, 0.25f)]
        [BackgroundColor(255, 0, 0)]
        public float Damage { get; set; } = 0.01f;

        [Range(0, 0.25f)]
        [BackgroundColor(255, 0, 0)]
        public float MaxWingTime { get; set; } = 0.02f;

        [Range(0, 0.25f)]
        [BackgroundColor(255, 0, 0)]
        public float PickSpeed { get; set; } = 0.01f;

        public override int GetHashCode() => HashCode.Combine(Damage, MaxWingTime, PickSpeed);
    }

    
    [Expand(false)]
    [BackgroundColor(0, 255, 255)]
    [LabelKey("$Mods.LevelPlus.Stats.Deft.DisplayName")]
    public DeftConfig Deft { get; set; } = new();

    public class DeftConfig
    {
        [Range(0, 0.25f)]
        [BackgroundColor(0, 255, 255)]
        public float Damage { get; set; } = 0.01f;

        [Range(0, 0.25f)]
        [BackgroundColor(0, 255, 255)]
        public float MaxSpeed { get; set; } = 0.01f;

        [Range(0, 0.25f)]
        [BackgroundColor(0, 255, 255)]
        public float PlacementSpeed { get; set; } = 0.02f;

        [Range(0, 0.25f)]
        [BackgroundColor(0, 255, 255)]
        public float Acceleration { get; set; } = 0.02f;

        public override int GetHashCode() => HashCode.Combine(Damage, MaxSpeed, PlacementSpeed, Acceleration);
    }

    
    [Expand(false)]
    [BackgroundColor(0, 0, 255)]
    [LabelKey("$Mods.LevelPlus.Stats.Intellect.DisplayName")]
    public IntellectConfig Intellect { get; set; } = new();

    public class IntellectConfig
    {
        [Range(0, 0.25f)]
        [BackgroundColor(0, 0, 255)]
        public float Damage { get; set; } = 0.01f;

        [Slider]
        [Range(0, 20)]
        [BackgroundColor(0, 0, 255)]
        public int Mana { get; set; } = 2;

        [Range(0, 0.25f)]
        [BackgroundColor(0, 0, 255)]
        public float ManaRegen { get; set; } = 0.02f;

        [Slider]
        [Range(10, 60)]
        [BackgroundColor(0, 0, 255)]
        public int BlockRangeCost { get; set; } = 25;

        public override int GetHashCode() => HashCode.Combine(Damage, Mana, ManaRegen, BlockRangeCost);
    }

    
    [Expand(false)]
    [BackgroundColor(255, 0, 255)]
    [LabelKey("$Mods.LevelPlus.Stats.Charm.DisplayName")]
    public CharmConfig Charm { get; set; } = new();

    public class CharmConfig
    {
        [Range(0, 0.25f)]
        [BackgroundColor(255, 0, 255)]
        public float Damage { get; set; } = 0.01f;

        [Slider]
        [Range(10, 60)]
        [BackgroundColor(255, 0, 255)]
        public int MinionCost { get; set; } = 20;

        [Slider]
        [Range(10, 60)]
        [BackgroundColor(255, 0, 255)]
        public int SentryCost { get; set; } = 30;

        [Range(0, 0.25f)]
        [BackgroundColor(255, 0, 255)]
        public float Fishing { get; set; } = 0.01f;

        public override int GetHashCode() => HashCode.Combine(Damage, MinionCost, SentryCost, Fishing);
    }

    
    [Expand(false)]
    [BackgroundColor(0, 255, 0)]
    [LabelKey("$Mods.LevelPlus.Stats.Luck.DisplayName")]
    public LuckConfig Luck { get; set; } = new();

    public class LuckConfig
    {
        [Range(0, 0.25f)]
        [BackgroundColor(0, 255, 0)]
        public float Crit { get; set; } = 0.05f;

        [Range(0, 0.25f)]
        [BackgroundColor(0, 255, 0)]
        public float TerrariaLuck { get; set; } = 0.05f;

        [Range(0, 0.25f)]
        [BackgroundColor(0, 255, 0)]
        public float Ammo { get; set; } = 0.03f;

        public override int GetHashCode() => HashCode.Combine(Crit, TerrariaLuck, Ammo);
    }
}