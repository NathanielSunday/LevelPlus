using Terraria.ModLoader;
using System.IO;
using Terraria;
using Terraria.ID;

namespace levelplus {

    internal enum PacketType {
        XP,
        PlayerSync,
        StatsChanged
    }

    public class levelplus : Mod {
        public const string modID = "levelplus";
        public static levelplus Instance { get; private set; }
        public levelplus() { Instance = this; }



        public override void HandlePacket(BinaryReader reader, int whoAmI) {
            byte msgType = reader.ReadByte();
            switch ((PacketType)msgType) {
                case PacketType.XP: //xp gain
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        Main.LocalPlayer.GetModPlayer<levelplusModPlayer>().AddXp(reader.ReadUInt64());
                    break;
                case PacketType.PlayerSync: //sync the players properly
                    ParsePlayer(reader, reader.ReadByte());
                    break;
                case PacketType.StatsChanged: //this is called on SendClientChanges
                    byte index = reader.ReadByte();
                    ParsePlayer(reader, index);
                    if(Main.netMode == NetmodeID.Server) {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)PacketType.StatsChanged);
                        Main.player[index].GetModPlayer<levelplusModPlayer>().AddSyncToPacket(packet);
                        packet.Send(-1, index);
                    }
                    break;
                default:
                    Logger.WarnFormat("levelplus: Unknown message type {0}", msgType);
                    break;
            }
        }

        public void ParsePlayer(BinaryReader reader, byte index) {
            levelplusModPlayer copy = Main.player[index].GetModPlayer<levelplusModPlayer>();
            copy.level = reader.ReadUInt16();
            copy.constitution = reader.ReadUInt16();
            copy.strength = reader.ReadUInt16();
            copy.intelligence = reader.ReadUInt16();
            copy.charisma = reader.ReadUInt16();
            copy.dexterity = reader.ReadUInt16();
            copy.mysticism = reader.ReadUInt16();
            copy.mobility = reader.ReadUInt16();
            copy.animalia = reader.ReadUInt16();
            copy.luck = reader.ReadUInt16();
            copy.excavation = reader.ReadUInt16();
        }
    }
}
