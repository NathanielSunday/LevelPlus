using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace LevelPlus.Common.Player;

public interface Stat
{
    // The LocalizedText for the name of the Stat
    LocalizedText Name { get; }
    
    // The LocalizedText for the description. Should be pre-formatted with args.
    LocalizedText Description { get; }
    
    // The LocalizedText for the description for next point(s) spent. Should be pre-formatted with args.
    LocalizedText SpendTooltip { get; }
    
    // The access key for stat, usually the name.
    string Id { get; }
    
    // The path of the icon to be used in the UI.
    string IconPath { get; }
    
    // The color to modify the UI element by.
    Color Color { get; }
    
    // The value of, or amount of points invested in, the stat.
    int Value { get; set; }
}