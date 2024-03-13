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
  public List<Type> StatTypes { get; } = new();

  public void Register(Player player)
  {
    var statPlayer = player.GetModPlayer<StatPlayer>();

    foreach (var type in StatTypes)
    {
      statPlayer.Register(Activator.CreateInstance(type!) as BaseStat);
    }
  }

  public override void Load()
  {
    StatTypes.Add(typeof(AdroitStat));
    StatTypes.Add(typeof(BrawnStat));
    StatTypes.Add(typeof(CharmStat));
    StatTypes.Add(typeof(DeftStat));
    StatTypes.Add(typeof(EnduranceStat));
    StatTypes.Add(typeof(IntellectStat));
    StatTypes.Add(typeof(LuckStat));
  }
}