// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using LevelPlus.Common.Players;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Common {
  class ScalingGlobalNPC : GlobalNPC {
    public override bool InstancePerEntity => true;

    float xpScalar = 1.0f;
    int numPlayers = 0;
    float topDamage;
    private long CalculateMobXP(int npcLife, int npcDefence) {
      float playerScalar = numPlayers == 1 ? 1.0f : (float)(Math.Log(numPlayers - 1) + 1.25f) / numPlayers;
      return (long)(
        (npcLife / xpScalar / 3
        + npcDefence)
        * playerScalar);
    }
    private int CalculateMaxHP(int maxHP) {
      return (int)Math.Clamp(maxHP * xpScalar, 0, int.MaxValue);
    }
    public override void OnSpawn(NPC npc, IEntitySource source) {
      base.OnSpawn(npc, source);
      float averageLevel = 0;
      if (Main.netMode == NetmodeID.Server) {
        foreach (Player i in Main.player) {
          if (i.active) {
            numPlayers++;
            averageLevel += LevelPlayer.XpToLevel(i.GetModPlayer<LevelPlayer>().Xp);
          }
        }
      }
      else if (Main.netMode == NetmodeID.SinglePlayer) {
        averageLevel += LevelPlayer.XpToLevel(Main.LocalPlayer.GetModPlayer<LevelPlayer>().Xp);
        numPlayers++;
      }

      if (!ServerConfig.Instance.Mob_ScalingEnabled) return;

      averageLevel /= numPlayers;
      xpScalar += averageLevel * ServerConfig.Instance.Mob_LevelScalar;
      npc.lifeMax = CalculateMaxHP(npc.lifeMax);
    }

    public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers) {
      base.ModifyHitPlayer(npc, target, ref modifiers);
      if (modifiers.PvP) return;
      modifiers.SourceDamage.Scale(xpScalar);
    }
    public override void OnKill(NPC npc) {
      base.OnKill(npc);
      if (npc.type != NPCID.TargetDummy && !npc.SpawnedFromStatue && !npc.friendly && !npc.townNPC) {
        long amount = CalculateMobXP((int)(npc.lifeMax * (npc.aiStyle != NPCAIStyleID.Worm ? 1.0f : 0.166f)), npc.defense);

        if (Main.netMode == NetmodeID.SinglePlayer) {
          Main.LocalPlayer.GetModPlayer<LevelPlayer>().AddXp(amount);
        }
        else if (Main.netMode == NetmodeID.Server) {
          for (int i = 0; i < npc.playerInteraction.Length; ++i) {
            if (npc.playerInteraction[i]) {
              //LevelPlus.Network.Packet.XPPacket.WritePacket(i, amount);
            }
          }
        }
      }
    }
  }
}

