// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus.Common.Configs;

public class CommandConfig : ModConfig
{
  public override ConfigScope Mode => ConfigScope.ServerSide;
  public static CommandConfig Instance => ModContent.GetInstance<CommandConfig>();
  
  [DefaultValue(false)] public bool CommandsEnabled;
  [DefaultValue(true)] public bool LevelCommandEnabled;
  [DefaultValue(true)] public bool PointCommandEnabled;
  [DefaultValue(true)] public bool XpCommandEnabled;
  
}
