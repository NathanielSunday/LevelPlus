// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Players;
using LevelPlus.Common.Players.Stats;
using Terraria.ModLoader;

namespace LevelPlus.Common.Systems;

public class StatProviderSystem : ModSystem
{  
  public override void Load()
  {
    StatPlayer player = ModContent.GetInstance<StatPlayer>();
    
    player.Register(new AdroitStat());
    player.Register(new BrawnStat());
    player.Register(new CharmStat());
    player.Register(new DeftStat());
    player.Register(new EnduranceStat());
    player.Register(new IntellectStat());
    player.Register(new LuckStat());
  }
}