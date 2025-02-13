using System;
using LevelPlus.Common.Config;
using LevelPlus.Common.System;
using LevelPlus.Network;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Player;

public class LevelPlayer : ModPlayer
{
    public int Level
    {
        get => ExperienceToLevel(Experience);

        set => Experience = LevelToExperience(value);
    }

    public int Points { get; set; }
    
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
        var priorLevel = Level;
        Experience += experience;

        // Show experience gain popup
        if (Main.dedServ) return;
        CombatText.NewText(Player.getRect(), Color.Aqua,
            Language.GetTextValue(((LevelPlus)Mod).LocalizationPrefix + "Stats.Level.Popup.Experience", experience));

        // Show level up popup, play level up sound, and give runtime points
        if (priorLevel == Level) return;
        SoundEngine.PlaySound(new SoundStyle(((LevelPlus)Mod).AssetPath + "Sounds/LevelUp"));
        CombatText.NewText(Player.getRect(), Color.GreenYellow,
            Language.GetTextValue(((LevelPlus)Mod).LocalizationPrefix + "Stats.Level.Popup.LevelUp"));
        
        Points += PlayConfiguration.Instance.Level.Points * (Level - priorLevel);
    }

    public override void OnEnterWorld()
    {
        if (PlayConfiguration.Instance.CommandsEnabled) return;
        ModContent.GetInstance<StatSystem>().ValidateStats(Player);
    }

    public override void OnRespawn()
    {
        var lossPercentage = PlayConfiguration.Instance.LossPercentage;
        
        if (lossPercentage == 0f) return;
        
        Experience = Math.Max(LevelToExperience(Level), (long)(Experience - (Experience - LevelToExperience(Level)) * lossPercentage));
    }
    
    public override void PostUpdateMiscEffects()
    {
        Player.statLifeMax2 += Life;
        Player.statManaMax2 += Mana;
    }

    public override void LoadData(TagCompound tag)
    {
        Experience = tag.ContainsKey("Experience") ? tag.GetLong("Experience") : 0;
    }

    public override void SaveData(TagCompound tag)
    {
        tag.Set("Experience", Experience);
    }

    public override void CopyClientState(ModPlayer targetCopy)
    {
        ((LevelPlayer)targetCopy).Level = Level;
    }

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
        if (!newPlayer) return;

        var packet = new StatPacket
        {
            Id = "level",
            Value = Level
        };
        
        packet.Send();
    }

    public override void SendClientChanges(ModPlayer clientCopy)
    {
        if (((LevelPlayer)clientCopy).Level == Level) return;
        
        SyncPlayer(0, 0, true);
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (Main.netMode == NetmodeID.Server) return;
        if (ModContent.GetInstance<KeybindSystem>().ToggleStatUI.JustPressed) return;
        ModContent.GetInstance<StatUISystem>().Toggle();
    }
}