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
                    averageLevel += i.GetModPlayer<levelplusModPlayer>().getLevel();
                }

            averageLevel /= numPlayers;

            npc.damage += (int)(npc.damage * (averageLevel / 40.0f));
            npc.lifeMax += (int)(npc.lifeMax * (averageLevel / 40.0f));
        }


        public override void OnKill(NPC npc) {

            if (npc.type != NPCID.TargetDummy && !npc.SpawnedFromStatue && !npc.friendly && !npc.townNPC) {
                double amount;
                if (npc.boss) {
                    amount = npc.lifeMax / 5;
                } else {
                    amount = npc.lifeMax / 3;
                }

                if (Main.netMode == NetmodeID.SinglePlayer)
                    Main.LocalPlayer.GetModPlayer<levelplusModPlayer>().gainXP(amount);
                else if (Main.netMode == NetmodeID.Server)
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

