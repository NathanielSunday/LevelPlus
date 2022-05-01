using Terraria.ModLoader;

namespace levelplus.Commands {
    class PointCommand : ModCommand {
        public override string Command => "points";

        public override string Description => "Adds points";
        public override CommandType Type => CommandType.Chat;

        public override string Usage => "/points <amount>";

        public override void Action(CommandCaller caller, string input, string[] args) {
            levelplusModPlayer player = caller.Player.GetModPlayer<levelplusModPlayer>();

            player.AddPoints(int.Parse(args[0]));
        }
    }

    class XPCommand : ModCommand {
        public override string Command => "xp";

        public override string Description => "Adds xp";
        public override CommandType Type => CommandType.Chat;

        public override string Usage => "/xp <amount>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            levelplusModPlayer player = caller.Player.GetModPlayer<levelplusModPlayer>();

            player.AddXp(ulong.Parse(args[0]));
        }
    }

    class LevelCommand : ModCommand {
        public override string Command => "level";

        public override string Description => "Adds levels";
        public override CommandType Type => CommandType.Chat;

        public override string Usage => "/level <amount>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            levelplusModPlayer player = caller.Player.GetModPlayer<levelplusModPlayer>();

            player.AddLevel(ushort.Parse(args[0]));
        }
    }
}

