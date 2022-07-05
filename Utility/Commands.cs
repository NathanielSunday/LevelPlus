using System;
using Terraria;
using Terraria.ModLoader;

namespace levelplus.Commands {
    //class AddPointsCommand : ModCommand {
    //    public override string Command => "addpoints";
    //
    //    public override string Description => "(Level+) Adds a given amount of skill points to the player's available skill points.";
    //    public override CommandType Type => CommandType.Chat;
    //
    //    public override string Usage => "/addpoints <amount>";
    //
    //    public override void Action(CommandCaller caller, string input, string[] args) {
    //        if (!levelplusConfig.Instance.CommandsEnabled) {
    //            Main.NewText("Enable commands in the Level+ config to use this command.");
    //            return;
    //        }
    //        levelplusModPlayer player = caller.Player.GetModPlayer<levelplusModPlayer>();
    //        player.AddPoints(int.Parse(args[0]));
    //    }
    //}
    //
    //class SetPointsCommand : ModCommand {
    //    public override string Command => "setpoints";
    //
    //    public override string Description => "(Level+) Sets the player's available skill points to a given amount.";
    //    public override CommandType Type => CommandType.Chat;
    //
    //    public override string Usage => "/setpoints <amount>";
    //
    //    public override void Action(CommandCaller caller, string input, string[] args) {
    //        if (!levelplusConfig.Instance.CommandsEnabled) {
    //            Main.NewText("Enable commands in the Level+ config to use this command.");
    //            return;
    //        }
    //        levelplusModPlayer player = caller.Player.GetModPlayer<levelplusModPlayer>();
    //        player.SetPoints(int.Parse(args[0]));
    //    }
    //}

    class AddXpCommand : ModCommand {
        public override string Command => "addxp";

        public override string Description => "(Level+) Adds a given amount of experience to the player's current experience.";
        public override CommandType Type => CommandType.Chat;

        public override string Usage => "/addxp <amount>";

        public override void Action(CommandCaller caller, string input, string[] args) {
            if (!levelplusConfig.Instance.CommandsEnabled) {
                Main.NewText("Enable commands in the Level+ config to use this command.");
                return;
            }
            levelplusModPlayer player = caller.Player.GetModPlayer<levelplusModPlayer>();
            player.AddXp(ulong.Parse(args[0]), true);
        }
    }

    class SetXpCommand : ModCommand {
        public override string Command => "setxp";

        public override string Description => "(Level+) Sets player experience to a given amount.";
        public override CommandType Type => CommandType.Chat;

        public override string Usage => "/setxp <amount>";

        public override void Action(CommandCaller caller, string input, string[] args) {
            if (!levelplusConfig.Instance.CommandsEnabled) {
                Main.NewText("Enable commands in the Level+ config to use this command.");
                return;
            }
            levelplusModPlayer player = caller.Player.GetModPlayer<levelplusModPlayer>();
            player.SetXp(ulong.Parse(args[0]));
        }
    }

    class AddLevelsCommand : ModCommand {
        public override string Command => "addlevel";

        public override string Description => "(Level+) Adds a given amount of levels to the current player level.";
        public override CommandType Type => CommandType.Chat;

        public override string Usage => "/addlevel <amount>";

        public override void Action(CommandCaller caller, string input, string[] args) {
            if (!levelplusConfig.Instance.CommandsEnabled) {
                Main.NewText("Enable commands in the Level+ config to use this command.");
                return;
            }
            levelplusModPlayer player = caller.Player.GetModPlayer<levelplusModPlayer>();
            player.AddLevel(ushort.Parse(args[0]));
        }
    }

    class SetLevelCommand : ModCommand {
        public override string Command => "setlevel";

        public override string Description => "(Level+) Sets the player level to a given amount.";
        public override CommandType Type => CommandType.Chat;

        public override string Usage => "/setlevel <amount>";

        public override void Action(CommandCaller caller, string input, string[] args) {
            if (!levelplusConfig.Instance.CommandsEnabled) {
                Main.NewText("Enable commands in the Level+ config to use this command.");
                return;
            }
            levelplusModPlayer player = caller.Player.GetModPlayer<levelplusModPlayer>();
            player.SetLevel(Math.Clamp(uint.Parse(args[0]), 1, 65536) - 1);
        }
    }

    class InvestCommand : ModCommand {
        public override string Command => "invest";

        public override string Description => "(Level+) Invests a given amount of levels into a stat of your choice."
            + "\nFor the first command argument: 0 is Constitution, 1-9 is all the other stats in a circle going right starting from just after Constitution."
            + "\nFor the second command argument: The amount you'd like to invest. Specify no amount to invest all available skill points.";
        public override CommandType Type => CommandType.Chat;

        public override string Usage => "/invest <stat invest id> <amount, defaults to 65535>";

        public override void Action(CommandCaller caller, string input, string[] args) {
            levelplusModPlayer player = caller.Player.GetModPlayer<levelplusModPlayer>();
            if (args.Length == 1) {
                player.InvestParticularAmount(ushort.Parse(args[0]));
            }
            else {
                player.InvestParticularAmount(ushort.Parse(args[0]), ushort.Parse(args[1]));
            }
        }
    }

    class SetInvestmentCommand : ModCommand {
        public override string Command => "setinvestment";

        public override string Description => "(Level+) Sets the investment in a stat of your choice to a given amount."
            + "\nWhen setting the investment lower than the previous amount, the points removed will be returned to the player's available skill points."
            + "\nFor the first command argument: 0 is Constitution, 1-9 is all the other stats in a circle going right starting from just after Constitution."
            + "\nFor the second command argument: The amount you'd like to have invested. Specify no amount to return all the skill points from that stat.";
        public override CommandType Type => CommandType.Chat;

        public override string Usage => "/setinvestment <stat invest id> <amount, defaults to 0>";

        public override void Action(CommandCaller caller, string input, string[] args) {
            if (!levelplusConfig.Instance.CommandsEnabled) {
                Main.NewText("Enable commands in the Level+ config to use this command.");
                return;
            }
            levelplusModPlayer player = caller.Player.GetModPlayer<levelplusModPlayer>();
            if (args.Length == 1) {
                player.SetInvestmentToParticularAmount(ushort.Parse(args[0]));
            }
            else {
                player.SetInvestmentToParticularAmount(ushort.Parse(args[0]), ushort.Parse(args[1]));
            }
        }
    }
}

