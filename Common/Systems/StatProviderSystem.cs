// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using LevelPlus.Common.Players;
using LevelPlus.Common.Players.Stats;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Systems;

public class StatProviderSystem : ModSystem
{
  private readonly List<Type> statTypes = new();


  private bool CreateStatInstance(Type type, out BaseStat stat)
  {
    if(!type.IsSubclassOf(typeof(BaseStat)))
    {
      LevelPlus.Instance.Logger.ErrorFormat("Stat of type '{0}' is not a derivative of type '{1}'", type, typeof(BaseStat));
      stat = null;
      return false;
    }

    stat = Activator.CreateInstance(type) as BaseStat;
    return true;
  }

  public List<string> GetIdList()
  {
    List<string> output = new();
    foreach (var type in statTypes)
    {
      if(!CreateStatInstance(type, out BaseStat stat)) continue;
      output.Add(stat.Id);
    }
    return output;
  }

  /// <summary>Register a stat to the list of stats to be registered to a player</summary>
  /// <returns>false if the Type provided is not a type of BaseStat</returns>
  public bool Register(Type type)
  {
    if(!type.IsSubclassOf(typeof(BaseStat))) return false;
    statTypes.Add(type);
    return true;
  }

  /// Register all registered stats to the player
  public void Register(Player player)
  {
    var statPlayer = player.GetModPlayer<StatPlayer>();

    foreach (var type in statTypes)
    {
      if(!CreateStatInstance(type, out BaseStat stat)) continue;
      statPlayer.Register(stat);
    }
  }

  public override void Load()
  {
    Register(typeof(AdroitStat));
    Register(typeof(BrawnStat));
    Register(typeof(CharmStat));
    Register(typeof(DeftStat));
    Register(typeof(EnduranceStat));
    Register(typeof(IntellectStat));
    Register(typeof(LuckStat));
  }
}