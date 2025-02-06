using System.ComponentModel;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Config;

public class UIConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [BackgroundColor(0, 0, 0)]
    [DefaultValue(typeof(Vector2), "0.25, 0.03")]
    public Vector2 ExperienceBar { get; set; }
    
    [BackgroundColor(0, 0, 0)]
    [DefaultValue(typeof(Vector2), "0.10, 0.03")]
    public Vector2 SpendPanel { get; set; }
}