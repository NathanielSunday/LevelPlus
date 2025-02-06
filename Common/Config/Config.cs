using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Config;

[BackgroundColor(55, 15, 85, 190)]
public partial class PlayConfiguration
{
    public override ConfigScope Mode => ConfigScope.ServerSide;    
    public override void OnLoaded() => (Mod as LevelPlus)!.PlayConfig = this;
    
    [BackgroundColor(0, 0, 0)]
    [Range(0f, 0.10f)]
    public float ScalingPercentage { get; set; } = 0.02f;
    
    [BackgroundColor(0, 0, 0)]
    [Increment(0.1f)]
    [Range(0f, 1f)]
    public float LossPercentage { get; set; } = 0.1f;
    
    [BackgroundColor(0, 0, 0)]
    public bool RandomStartingWeapon { get; set; } = true;
    
    public int StartingPoints { get; set; } = 3;
    
}