// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.Configs;
using LevelPlus.Common.Players.Stats;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players;

public class StatPlayer : ModPlayer
{
  public Dictionary<string, BaseStat> Stats { get; private set; }
  public int PointsAvailable { get; protected set; }

  public void RegisterStat(BaseStat stat) => Stats.Add(stat.Id, stat);

  private void Validate()
  {
    if (CommandConfig.Instance.CommandsEnabled) return;
    return;
    /*
    int spent = 0;
    for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i)
    {
      spent += Stats[i];
    }

    int possiblePoints = Math.Min(Level, ServerConfig.Instance.Level_MaxLevel) * ServerConfig.Instance.Level_Points +
      ServerConfig.Instance.Level_StartingPoints;
    if (spent > possiblePoints)
    {
      for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i)
      {
      }
    }

    while (spent > possiblePoints)
    {
      for (int i = 0; i < Enum.GetNames(typeof(Stat)).Length; ++i)
      {
        if (Stats[i] > 0)
        {
          --Stats[i];
          --spent;
        }
      }
    }

    PointsAvailable = possiblePoints - spent;
    */
  }

  public virtual void Add(int amount = 1)
  {
    if (PointsAvailable == 0) return;
    if (PointsAvailable < amount) amount = PointsAvailable;
    if (Value + amount < 0)
      amount = int.MaxValue - Value;
    PointsAvailable -= amount;
  }
  
  public virtual void Set(int value)
  {
    if(value < 0) return;
    Value = value;
  }
  
  public virtual bool Match(StatPlayer compare)
  {
    return Value == compare.Value;
  }

  public override void LoadData(TagCompound tag)
  {
    foreach(BaseStat stat in Stats.Values)
    {
      stat.LoadData(tag);
    }

    // Give the Player their left over points
  }

  public override void SaveData(TagCompound tag)
  {
    // Inject whatever my be needed by other classes here, then pass it on

    foreach(BaseStat stat in Stats.Values)
    {
      stat.SaveData(tag);
    }
  }

  /// Validate has to be called "OnEnterWorld" to get server-side configs
  public override void OnEnterWorld() => Validate();


}