using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs;

// ReSharper disable file InconsistentNaming
[JITWhenModsEnabled("CalamityMod")]
public class CalamityStatConfig : ModConfig
{
  public override ConfigScope Mode => ConfigScope.ServerSide;
  public static CalamityStatConfig Instance => ModContent.GetInstance<CalamityStatConfig>();

  [Header("$Mods.LevelPlus.Stats.Brawn.DisplayName")] [DefaultValue(0.01f)]
  public float Brawn_RogueDamage;

  [DefaultValue(0.01f)] public float Brawn_RogueVelocity;

  [Header("$Mods.LevelPlus.Stats.Deft.DisplayName")] [DefaultValue(0.2f)]
  public float Deft_RogueStealthMax;

  [DefaultValue(0.01f)] public float Deft_RogueStealthDamage;
}