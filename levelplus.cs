using Terraria.ModLoader;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace LevelPlus {

    public class LevelPlus : Mod {
        public static LevelPlus Instance { get; private set; }
        public LevelPlus() { Instance = this; }

        public static ModKeybind SpendUIHotKey;
        public static ModKeybind SpendModFive;
        public static ModKeybind SpendModTen;
        public static ModKeybind SpendModTwentyFive;

        public override void Load() {
            SpendUIHotKey = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods." + Name + ".Keybind.UI"), Microsoft.Xna.Framework.Input.Keys.P);
            SpendModFive = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods." + Name + ".Keybind.Five"), Microsoft.Xna.Framework.Input.Keys.LeftShift);
            SpendModTen = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods." + Name + ".Keybind.Ten"), Microsoft.Xna.Framework.Input.Keys.LeftControl);
            SpendModTwentyFive = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods." + Name + ".Keybind.TwentyFive"), Microsoft.Xna.Framework.Input.Keys.LeftAlt);
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
                        Main.LocalPlayer.GetModPlayer<LevelPlusModPlayer>().AddXp(reader.ReadUInt64());
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
                        Utility.AddSyncToPacket(packet, Main.player[index].GetModPlayer<LevelPlusModPlayer>());
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
