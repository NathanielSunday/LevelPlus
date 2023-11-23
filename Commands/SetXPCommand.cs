// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Config;
using LevelPlus.Core;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Commands
{
    class SetXpCommand : ModCommand {
    public override string Command => "setxp";

    public override string Description => "(" + Mod.Name + ")" + Language.GetTextValue("Commands.SetXPCommand.Description");

    public override CommandType Type => CommandType.Chat;

    public override string Usage => "/setxp <amount>";

    public override void Action(CommandCaller caller, string input, string[] args) {
      if (!ServerConfig.Instance.CommandsEnabled) {
        Main.NewText(Language.GetTextValue("Commands.EnableCommandsError"));
        return;
      }
      LevelPlusModPlayer player = caller.Player.GetModPlayer<LevelPlusModPlayer>();
      player.SetXP(long.Parse(args[0]));
    }
  }
}