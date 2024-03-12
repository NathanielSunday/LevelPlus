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
  /// Santa's little helper
  internal record Arguments(string Action, int Value, string Stat);

  public override string Command => "point";

  public override string Description =>
    "(" + Mod.Name + ") " + Language.GetTextValue("Commands.PointCommand.Description");

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

    var arguments = new Arguments(args[0], value, args[2]);

    StatPlayer player = caller.Player.GetModPlayer<StatPlayer>();

    switch (arguments)
    {
      case ("add", var v, var s) when !string.IsNullOrEmpty(s):
        if (!player.AddStat(s, v, true)) Main.NewText(Language.GetTextValue("Commands.InvalidArgument", s));
        break;
      case ("set", var v, var s) when !string.IsNullOrEmpty(s):
        if (!player.SetStat(s, v)) Main.NewText(Language.GetTextValue("Commands.InvalidArgument", s));
        break;
      case ("add", var v, _):
        player.Points += v;
        break;
      case ("set", var v, _):
        player.Points = v;
        break;
      default:
        Main.NewText(Language.GetTextValue("Commands.InvalidArgument", args[0]));
        break;
    }
  }
}

