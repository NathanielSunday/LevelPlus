// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using LevelPlus.Common.Players;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Common.Commands;

class PointCommand : ModCommand
{
  public override string Command => "point";

  public override string Description =>
    "(" + Mod.Name + ")" + Language.GetTextValue("Commands.PointCommand.Description");

  public override CommandType Type => CommandType.Chat;

  public override string Usage => "/point add|set <amount> [statKey]";

  public override void Action(CommandCaller caller, string input, string[] args)
  {
    if (!CommandConfig.Instance.CommandsEnabled || !CommandConfig.Instance.PointCommandEnabled)
    {
      Main.NewText(Language.GetTextValue("Commands.CommandNotEnabledError"));
      return;
    }

    if (!int.TryParse(args[1], out int value))
    {
      Main.NewText(Language.GetTextValue("Commands.InvalidArgument", args[1]));
      return;
    }
    if (args[2] != null)
    {
      // Find stat by key here
    }
    StatPlayer player = caller.Player.GetModPlayer<StatPlayer>();
    switch (args[0].ToLower())
    {
      case "add":
        player.Add(value);
        break;
      case "set":
        player.Set(value);
        break;
      default:
        Main.NewText(Language.GetTextValue("Commands.InvalidArgument", args[0]));
        break;
    }
  }
}

