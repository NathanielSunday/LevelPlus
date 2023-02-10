using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus {
    class levelplusGlobalNPC : GlobalNPC {

        public override bool InstancePerEntity => true;

        public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale) {
            base.ScaleExpertStats(npc, numPlayers, bossLifeScale);
            if (LevelPlusConfig.Instance.ScalingEnabled) {
                float averageLevel = 0;



                foreach (Player i in Main.player)
                    if (i.active) {
                        //numPlayers++; //not needed here, since numPlayers should get the number of active players on the server already
                        averageLevel += i.GetModPlayer<LevelPlusModPlayer>().Level;
                    }

                averageLevel /= numPlayers;
                float xpScalar = 1 + averageLevel * Utility.MobScalar;
                npc.damage = (int)Math.Clamp(npc.damage * (long)Math.Round(xpScalar), 0, int.MaxValue);
                npc.lifeMax = (int)Math.Clamp(npc.lifeMax * (long)Math.Round(xpScalar), 0, int.MaxValue);
            }
        }


        public override void OnKill(NPC npc) {
            base.OnKill(npc);

            if (npc.type != NPCID.TargetDummy && !npc.SpawnedFromStatue && !npc.friendly && !npc.townNPC) {
                
                int numPlayers = 0;
                float averageLevel = 0;
                
                foreach (Player i in Main.player) {
                    if (i.active) {
                        numPlayers++;
                        averageLevel += i.GetModPlayer<LevelPlusModPlayer>().Level;
                    }
                }

                averageLevel /= numPlayers;
                float xpScalar = (levelplusConfig.Instance.ScalingEnabled) ? 1 + averageLevel * Utility.MobScalar : 1.0f;

                ulong amount = (ulong)(Utility.CalculateMobXp((int)(npc.lifeMax / xpScalar), (int)(npc.damage / xpScalar), npc.defense)
                    * ((numPlayers == 1) ? 1 : (Math.Log(numPlayers - 1) + 1.25f) / numPlayers));

                if (Main.netMode == NetmodeID.SinglePlayer) {
                    Main.LocalPlayer.GetModPlayer<LevelPlusModPlayer>().AddXp(amount);
                }
                else if (Main.netMode == NetmodeID.Server) {
                    for (int i = 0; i < npc.playerInteraction.Length; ++i) {
                        if (npc.playerInteraction[i]) {
                            ModPacket packet = LevelPlus.Instance.GetPacket();
                            packet.Write((byte)Utility.PacketType.XP);

                            packet.Write(amount);
                            packet.Send(i);
                        }
                    }
                }
            }
        }
    }
}

