using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace levelplus {
    class levelplusGlobalNPC : GlobalNPC {

        public override bool InstancePerEntity => true;

        public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale) {
            base.ScaleExpertStats(npc, numPlayers, bossLifeScale);
            float averageLevel = 0;

            //doesnt work in multiplayer, might have to make this an expert only mechanic

            foreach (Player i in Main.ActiveWorld.Players)
                if (i.active) {
                    numPlayers++;
                    averageLevel += i.GetModPlayer<levelplusModPlayer>().GetLevel();
                }

            averageLevel /= numPlayers;

            npc.damage += (int)(npc.damage * (averageLevel / 40.0f));
            npc.lifeMax += (int)(npc.lifeMax * (averageLevel / 40.0f));
        }


        public override void OnKill(NPC npc) {
            base.OnKill(npc);

            if (npc.type != NPCID.TargetDummy && !npc.SpawnedFromStatue && !npc.friendly && !npc.townNPC) {
                ulong amount;
                if (npc.boss) {
                    amount = (ulong)(npc.lifeMax / 4);
                } else {
                    amount = (ulong)(npc.lifeMax / 3);
                }

                if (Main.netMode == NetmodeID.SinglePlayer) {
                    Main.LocalPlayer.GetModPlayer<levelplusModPlayer>().AddXp(amount);
                } else if (Main.netMode == NetmodeID.Server) {
                    levelplus.Instance.Logger.WarnFormat("" + npc.playerInteraction.Length);
                    for (int i = 0; i < npc.playerInteraction.Length; ++i)
                        if (npc.playerInteraction[i]) {
                            ModPacket packet = levelplus.Instance.GetPacket();
                            packet.Write((byte)PacketType.XP);
                            packet.Write(amount);
                            packet.Send(i);
                        }
                }
            }
        }
    }
}

