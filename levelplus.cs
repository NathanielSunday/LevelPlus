using Terraria.ModLoader;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace LevelPlus {

    internal enum PacketType : byte {
        XP,
        PlayerSync,
        StatsChanged
    }

    public class LevelPlus : Mod {
        public static LevelPlus Instance { get; private set; }
        public LevelPlus() { Instance = this; }

        public static ModKeybind SpendUIHotKey;
        public static ModKeybind SpendModFive;
        public static ModKeybind SpendModTen;
        public static ModKeybind SpendModAll;

        public override void Load() {
            SpendUIHotKey = KeybindLoader.RegisterKeybind(this, "Open SpendUI", Microsoft.Xna.Framework.Input.Keys.P);
            SpendModFive = KeybindLoader.RegisterKeybind(this, "Spend 5 points", Microsoft.Xna.Framework.Input.Keys.LeftShift);
            SpendModTen = KeybindLoader.RegisterKeybind(this, "Spend 10 points", Microsoft.Xna.Framework.Input.Keys.LeftControl);
            SpendModAll = KeybindLoader.RegisterKeybind(this, "Spend all points", Microsoft.Xna.Framework.Input.Keys.LeftAlt);
        }

        public override void Unload() {
            SpendUIHotKey = null;
            SpendModFive = null;
            SpendModTen = null;
            SpendModAll = null;
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI) {
            byte msgType = reader.ReadByte();
            switch ((PacketType) msgType) {
                case PacketType.XP: //xp gain
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        Main.LocalPlayer.GetModPlayer<LevelPlusModPlayer>().AddXp(reader.ReadUInt64());
                    break;
                case PacketType.PlayerSync: //sync the players properly
                    ParsePlayer(reader, reader.ReadByte());
                    break;
                case PacketType.StatsChanged: //this is called on SendClientChanges
                    byte index = reader.ReadByte();
                    ParsePlayer(reader, index);
                    if (Main.netMode == NetmodeID.Server) {
                        ModPacket packet = GetPacket();
                        packet.Write((byte) PacketType.StatsChanged);
                        Main.player[index].GetModPlayer<LevelPlusModPlayer>().AddSyncToPacket(packet);
                        packet.Send(-1, index);
                    }
                    break;
                default:
                    Logger.WarnFormat("levelplus: Unknown message type {0}", msgType);
                    break;
            }
        }

        public void ParsePlayer(BinaryReader reader, byte index) {
            LevelPlusModPlayer copy = Main.player[index].GetModPlayer<LevelPlusModPlayer>();
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
