using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace levelplus {
    class levelplusGlobalNPC : GlobalNPC {

        public override bool InstancePerEntity => true;

        public override void SetDefaults(NPC npc) {

            base.SetDefaults(npc);
            float averageLevel = 0;
            int playerCount = 0;

            foreach (Player i in Main.player) {
                if (i.active) {
                    ++playerCount;
                    averageLevel += i.GetModPlayer<levelplusModPlayer>().getLevel();
                }
            }

            averageLevel /= playerCount;

            npc.damage += (int)(npc.damage * (averageLevel / 25f));
            npc.lifeMax += (int)(npc.lifeMax * (averageLevel / 25f));
        }

        public override void OnKill(NPC npc) {

            if (npc.type != NPCID.TargetDummy && !npc.SpawnedFromStatue && !npc.friendly && !npc.townNPC) {
                double amount;
                if (npc.boss) {
                    amount = npc.lifeMax / 3;
                } else {
                    amount = npc.lifeMax / 4;
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

