// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Config;
using LevelPlus.Core;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Commands
{
    class SetXpCommand : ModCommand {
    public override string Command => "setxp";

    public override string Description => "(Level+) Sets player experience to a given amount.";
    public override CommandType Type => CommandType.Chat;

    public override string Usage => "/setxp <amount>";

    public override void Action(CommandCaller caller, string input, string[] args) {
      if (!ServerConfig.Instance.CommandsEnabled) {
        Main.NewText("Enable commands in the Level+ config to use this command.");
        return;
      }
      LevelPlusModPlayer player = caller.Player.GetModPlayer<LevelPlusModPlayer>();
      player.SetXP(long.Parse(args[0]));
    }
  }
}