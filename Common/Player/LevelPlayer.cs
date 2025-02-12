using System;
using LevelPlus.Common.Config;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Common.Player;

public class LevelPlayer : ModPlayer
{
    public int Level
    {
        get => ExperienceToLevel(Experience);

        set => Experience = LevelToExperience(value);
    }

    public long Experience { get; set; }

    public LocalizedText Description => Mod.GetLocalization("Stats.Level.Tooltip" +
                                                            (Main.netMode == NetmodeID.MultiplayerClient
                                                                ? ".Multiplayer"
                                                                : ""))
        .WithFormatArgs(Level, Life, Mana, GetAverageLevel());

    private int Life => Level * PlayConfiguration.Instance.Level.Life;
    private int Mana => Level * PlayConfiguration.Instance.Level.Mana;

    public static int GetAverageLevel()
    {
        int level = 0;
        int players = 0;

        foreach (var player in Main.ActivePlayers)
        {
            players++;
            level += player.GetModPlayer<LevelPlayer>().Level;
        }

        return players != 0 ? level / players : 0;
    }

    public static int ExperienceToLevel(long experience)
    {
        return Math.Min((int)Math.Floor(Math.Pow(experience / 100f, 5 / 11f)), PlayConfiguration.Instance.Level.Max);
    }
    
    public static long LevelToExperience(int level)
    {
        return (long)Math.Ceiling(100f * Math.Pow(level, 11 / 5f));
    }

    // Used explicitly for gaining experience legitimately 
    public void GainExperience(long experience)
    {
        int priorLevel = Level;
        Experience += experience;

        // Show experience gain popup
        if (Main.dedServ) return;
        CombatText.NewText(Player.getRect(), Color.Aqua,
            Language.GetTextValue(Mod.Name + "Stats.Level.Popup.Experience", experience));

        // Show level up popup and play level up sound
        if (priorLevel == Level) return;
        SoundEngine.PlaySound(new SoundStyle(((LevelPlus)Mod).AssetPath + "Sounds/LevelUp"));
        CombatText.NewText(Player.getRect(), Color.GreenYellow,
            Language.GetTextValue(((LevelPlus)Mod).LocalizationPrefix + "Stats.Level.Popup.LevelUp"));
    }

    public override void OnEnterWorld()
    {
        // TODO Verify the player
    }

    public override void OnRespawn()
    {
        Experience = Math.Max(LevelToExperience(Level), (long)(Experience - (Experience - LevelToExperience(Level)) * PlayConfiguration.Instance.LossPercentage));
    }
}