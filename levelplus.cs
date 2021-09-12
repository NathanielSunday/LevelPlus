using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using Terraria;
using System;
using Terraria.ID;

namespace levelplus {

    internal enum PacketType {
        XP
    }

    public class levelplus : Mod {
        public const string modID = "levelplus";

        public levelplus() {
            Instance = this;
        }
        public static levelplus Instance { get; set; }
        public static SoundEffect LevelUp { get; set; }

        public override void HandlePacket(BinaryReader reader, int whoAmI) {
            byte msgType = reader.ReadByte();
            switch ((PacketType)msgType) {
                case PacketType.XP: //xp gain
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        Main.LocalPlayer.GetModPlayer<levelplusModPlayer>().gainXP(reader.ReadDouble());
                    break;
                default:
                    Logger.WarnFormat("levelplus: Unknown message type {0}", msgType);
                    break;
            }
        }
    }
}
