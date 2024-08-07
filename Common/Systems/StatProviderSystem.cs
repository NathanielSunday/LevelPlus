// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using LevelPlus.Common.Players;
using LevelPlus.Common.Players.Stats;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Common.Systems;

public class StatProviderSystem : ModSystem
{
  private readonly Dictionary<string, Type> statTypes = new();

  public static readonly StatProviderSystem Instance = ModContent.GetInstance<StatProviderSystem>();

  private bool CreateStatInstance(Type type, out BaseStat stat)
  {
    if (!type.IsSubclassOf(typeof(BaseStat)))
    {
      LevelPlus.Instance.Logger.ErrorFormat(Language.GetTextValue(
        LevelPlus.Instance.LocalizationPrefix + "Provider.UnhandledStatError", type, typeof(BaseStat)));
      stat = null;
      return false;
    }

    stat = Activator.CreateInstance(type) as BaseStat;
    return true;
  }

  private bool CreateStatInstance(string key, out BaseStat stat)
  {
    if (!statTypes.TryGetValue(key, out var statType) || !CreateStatInstance(statType, out stat))
    {
      stat = null;
      return false;
    }

    return true;
  }

  private void LoadVanillaStats()
  {
    Register(typeof(EnduranceStat));
    Register(typeof(BrawnStat));
    Register(typeof(DeftStat));
    Register(typeof(IntellectStat));
    Register(typeof(CharmStat));
    Register(typeof(LuckStat));
    Register(typeof(AdroitStat));
    LevelPlus.Instance.Logger.Info("Registered stats");
  }

  [JITWhenModsEnabled("CalamityMod")]
  private void LoadCalamityStats()
  {
    Register(typeof(StealthStat));
    LevelPlus.Instance.Logger.Info("Registered Calamity stats");
  }

  [JITWhenModsEnabled("ThoriumMod")]
  private void LoadThoriumStats()
  {
    LevelPlus.Instance.Logger.Info("Registered Thorium stats");
  }

  public string GetIconPath(string key)
  {
    if (!CreateStatInstance(key, out var stat)) return "LevelPlus/Assets/Textures/UI/Hint";
    return stat.IconPath;
  }

  public List<string> GetIdList()
  {
    return statTypes.Keys.ToList();
  }

  /// <summary>Register a stat to the list of stats to be registered to a player</summary>
  /// <returns>false if the Type provided is not a type of BaseStat</returns>
  public bool Register(Type type)
  {
    if (!type.IsSubclassOf(typeof(BaseStat))) return false;
    if (!CreateStatInstance(type, out var stat)) return false;
    statTypes.Add(stat.Id, type);
    return true;
  }

  /// Register all registered stats to the player
  public void Register(StatPlayer player)
  {
    foreach (var type in statTypes.Values)
    {
      if (!CreateStatInstance(type, out BaseStat stat)) continue;
      player.Register(stat);
    }
  }

  public override void OnModLoad()
  {
    LoadVanillaStats();
    if (LevelPlus.Instance.IsCalamityModLoaded) LoadCalamityStats();
    if (LevelPlus.Instance.IsThoriumModLoaded) LoadThoriumStats();
  }
}