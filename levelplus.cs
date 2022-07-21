using Terraria.ModLoader;
using System.IO;
using Terraria;
using Terraria.ID;

namespace levelplus {

    public class levelplus : Mod {
        public static levelplus Instance { get; private set; }
        public levelplus() { Instance = this; }

        public static ModKeybind SpendUIHotKey;
        public static ModKeybind SpendModFive;
        public static ModKeybind SpendModTen;
        public static ModKeybind SpendModTwentyFive;

        public override void Load() {
            SpendUIHotKey = KeybindLoader.RegisterKeybind(this, "Open SpendUI", Microsoft.Xna.Framework.Input.Keys.P);
            SpendModFive = KeybindLoader.RegisterKeybind(this, "Spend 5 points", Microsoft.Xna.Framework.Input.Keys.LeftShift);
            SpendModTen = KeybindLoader.RegisterKeybind(this, "Spend 10 points", Microsoft.Xna.Framework.Input.Keys.LeftControl);
            SpendModTwentyFive = KeybindLoader.RegisterKeybind(this, "Spend 25 points", Microsoft.Xna.Framework.Input.Keys.LeftAlt);
        }

        public override void Unload() {
            SpendUIHotKey = null;
            SpendModFive = null;
            SpendModTen = null;
            SpendModTwentyFive = null;
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI) {
            byte msgType = reader.ReadByte();
            switch ((Utility.PacketType) msgType) {
                //called when a player recieves an XP packet
                case Utility.PacketType.XP: 
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        Main.LocalPlayer.GetModPlayer<levelplusModPlayer>().AddXp(reader.ReadUInt64());
                    break;
                //sync the players properly, after a stat has changed
                case Utility.PacketType.PlayerSync:
                    Utility.ParsePlayer(reader, reader.ReadByte());
                    break;
                //this is called on SendClientChanges, basically when a stat changes
                case Utility.PacketType.StatsChanged: 
                    byte index = reader.ReadByte();
                    Utility.ParsePlayer(reader, index);
                    if (Main.netMode == NetmodeID.Server) {
                        ModPacket packet = GetPacket();
                        packet.Write((byte) Utility.PacketType.StatsChanged);
                        Utility.AddSyncToPacket(packet, Main.player[index].GetModPlayer<levelplusModPlayer>());
                        packet.Send(-1, index);
                    }
                    break;
                //wrong message type recieved
                default:
                    Logger.WarnFormat("levelplus: Unknown message type {0}", msgType);
                    break;
            }
        }
    }
}
