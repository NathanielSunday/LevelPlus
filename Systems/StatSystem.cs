using System.Collections.Generic;
using System.Linq;
using LevelPlus.Configs;
using LevelPlus.Players;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Systems;

public class StatSystem : ModSystem
{
    // Base instances of Stat implementations, should not modify these directly
    public List<Stat> Stats { get; private set; }

    public void ValidateStats(Player player)
    {
        var maxPoints = player.GetModPlayer<LevelPlayer>().Level * PlayConfiguration.Instance.Level.Points +
                        PlayConfiguration.Instance.Level.StartingPoints;
        var playerStats = GetStatsOfPlayer(player.whoAmI);

        // Roll each stat down by one point until we are back under maxPoints
        // Don't want to use percentages in case players log out with unspent points
        while (playerStats.Sum(s => s.Value) > maxPoints) playerStats.ForEach(s => s.Value -= s.Value > 0 ? 1 : 0);


        player.GetModPlayer<LevelPlayer>().Points = maxPoints - playerStats.Sum(s => s.Value);
    }

    public Stat GetStatOfPlayer(int player, string id)
    {
        return Main.player[player].GetModPlayer(Stats.Find(s => s.Id == id));
    }

    public List<Stat> GetStatsOfPlayer(int player)
    {
        return Stats
            .Select(p => Main.player[player].GetModPlayer(p))
            .ToList();
    }

    public override void Load()
    {
        Stats = [];

        // Get every instance of Stat loaded and add it
        foreach (var stat in ModContent.GetContent<Stat>()) Stats.Add(stat);

        Mod.Logger.Info("Loading Stats...");
        Mod.Logger.Info(Stats.Select(s => s.Id));
    }

    public override void Unload()
    {
        Stats.Clear();
        Stats = null;
    }
}