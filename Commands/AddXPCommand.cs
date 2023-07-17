// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Config;
using LevelPlus.Core;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Commands
{
    class AddXPCommand : ModCommand {
    public override string Command => "addxp";

    public override string Description => "(Level+) Adds a given amount of experience to the player's current experience.";
    public override CommandType Type => CommandType.Chat;

    public override string Usage => "/addxp <amount>";

    public override void Action(CommandCaller caller, string input, string[] args) {
      if (!ServerConfig.Instance.CommandsEnabled) {
        Main.NewText("Enable commands in the Level+ config to use this command.");
        return;
      }
      LevelPlusModPlayer player = caller.Player.GetModPlayer<LevelPlusModPlayer>();
      player.AddXP(long.Parse(args[0]), true);
    }
  }
}
