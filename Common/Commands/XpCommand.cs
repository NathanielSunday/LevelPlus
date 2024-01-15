// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using LevelPlus.Common.Players.Stats;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Common.Commands;

class XpCommand : ModCommand
{
  public override string Command => "xp";

  public override string Description => "(" + Mod.Name + ")" + Language.GetTextValue("Commands.XpCommand.Description");

  public override CommandType Type => CommandType.Chat;

  public override string Usage => "/xp add|set <amount>";

  public override void Action(CommandCaller caller, string input, string[] args)
  {
    if (!CommandConfig.Instance.CommandsEnabled || !CommandConfig.Instance.XpCommandEnabled)
    {
      Main.NewText(Language.GetTextValue("Commands.CommandNotEnabledError"));
      return;
    }

    if (!long.TryParse(args[1], out long value))
    {
      Main.NewText(Language.GetTextValue("Commands.InvalidArgument", args[1]));
      return;
    }

    LevelStat player = caller.Player.GetModPlayer<LevelStat>();
    switch (args[0].ToLower())
    {
      case "add":
        player.AddXp(value, true);
        break;
      case "set":
        player.SetXp(value);
        break;
      default:
        Main.NewText(Language.GetTextValue("Commands.InvalidArgument", args[0]));
        break;
    }
  }
}

