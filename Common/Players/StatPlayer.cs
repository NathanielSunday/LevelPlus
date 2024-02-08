// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using LevelPlus.Common.Configs;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Players;
/*
 * The base for any individual stat.
 * This class also manages every stat child of it, and all the unallocate points, etc
 * Each child should only handle its own points as well as what buffs are given based on those points.
 */
public abstract class StatPlayer : ModPlayer
{
  public static Dictionary<string, StatPlayer> Stats { get; protected set; }
  public static int PointsAvailable { get; protected set; }

  private string NameKey => "Stats." + Id + ".Name";
  private string DescriptionKey => "Stats." + Id + ".Bonuses";
  public LocalizedText Name => Language.GetText(NameKey);
  public LocalizedText Description => Language.GetText(DescriptionKey).WithFormatArgs(DescriptionArgs);
  
  public virtual int Value { get; protected set; }

  protected abstract string Id { get; }
  protected abstract object[] DescriptionArgs { get; }
  

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
  
  // Also want to enforce data being written and *order* of data being written
  protected abstract void OnLoadData(TagCompound tag);
  protected abstract void OnSaveData(TagCompound tag);

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
    OnLoadData(tag);

    // Give the Player their left over points
  }

  public override void SaveData(TagCompound tag)
  {
    // Inject whatever my be needed by other classes here, then pass it on

    OnSaveData(tag);
  }

  public override void OnEnterWorld()
  {
    Validate();
  }
}