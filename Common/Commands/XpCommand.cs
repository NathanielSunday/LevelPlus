// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using LevelPlus.Common.Players;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Common.Commands {
  class XpCommand : ModCommand {
    public override string Command => "xp";

    public override string Description => "(" + Mod.Name + ")" + Language.GetTextValue("Commands.XpCommand.Description");

    public override CommandType Type => CommandType.Chat;

    public override string Usage => "/xp add|set <amount>";

    public override void Action(CommandCaller caller, string input, string[] args) {
      if (!ServerConfig.Instance.Commands_Enabled) {
        Main.NewText(Language.GetTextValue("Commands.EnableCommandsError"));
        return;
      }
      if (!long.TryParse(args[1], out long value)) {
        Main.NewText(Language.GetTextValue("Commands.InvalidArguments"));
        return;
      }
      LevelPlayer player = caller.Player.GetModPlayer<LevelPlayer>();
      switch (args[0].ToLower()) {
        case "add":
          player.AddXp(value, true);
          break;
        case "set":
          player.SetXp(value);
          break;
        default:
          Main.NewText(Language.GetTextValue("Commands.InvalidArguments"));
          break;
      }
    }
  }
}
