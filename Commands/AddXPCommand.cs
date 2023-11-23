// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Config;
using LevelPlus.Core;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Commands
{
    class AddXPCommand : ModCommand {
    public override string Command => "addxp";

    public override string Description => "(" + Mod.Name + ")" + Language.GetTextValue("Commands.AddXPCommand.Description");

    public override CommandType Type => CommandType.Chat;

    public override string Usage => "/addxp <amount>";

    public override void Action(CommandCaller caller, string input, string[] args) {
      if (!ServerConfig.Instance.CommandsEnabled) {
        Main.NewText(Language.GetTextValue("Commands.EnableCommandsError"));
        return;
      }
      LevelPlusModPlayer player = caller.Player.GetModPlayer<LevelPlusModPlayer>();
      player.AddXP(long.Parse(args[0]), true);
    }
  }
}
