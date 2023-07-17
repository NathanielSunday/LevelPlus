// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using System;
using LevelPlus.Config;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Core
{
    class LevelPlusGlobalNPC : GlobalNPC {
    public override bool InstancePerEntity => true;

    float xpScalar = 1.0f;
    int numPlayers = 0;
    /// <summary>
    /// Calculate xp gain from npc stats
    /// </summary>
    /// <returns>the amount of xp that should go to a single player</returns>
    public long CalculateMobXP(int npcLife, int npcDamage, int npcDefence) {
      float playerScalar = numPlayers == 1 ? 1.0f : (float)(Math.Log(numPlayers - 1) + 1.25f) / numPlayers;
      return (long)(
        (npcDamage / xpScalar / 2
        + npcLife / xpScalar / 3
        + npcDefence)
        * playerScalar);
    }
    public int CalculateMaxHP(int maxHP) {
      return (int)Math.Clamp(maxHP * xpScalar, 0, int.MaxValue);
    }
    public int CalculateDamage(int damage) {
      return (int)Math.Clamp(damage * xpScalar, 0, int.MaxValue);
    }
    public override void OnSpawn(NPC npc, IEntitySource source) {
      base.OnSpawn(npc, source);
      float averageLevel = 0;
      if (Main.netMode == NetmodeID.Server) {
        foreach (Player i in Main.player) {
          if (i.active) {
            numPlayers++;
            averageLevel += LevelPlusModPlayer.XPToLevel(i.GetModPlayer<LevelPlusModPlayer>().XP);
          }
        }
      }
      else if (Main.netMode == NetmodeID.SinglePlayer) {
        averageLevel += LevelPlusModPlayer.XPToLevel(Main.LocalPlayer.GetModPlayer<LevelPlusModPlayer>().XP);
        numPlayers++;
      }

      if (!ServerConfig.Instance.ScalingEnabled) return;

      averageLevel /= numPlayers;
      xpScalar += averageLevel * LEVEL_SCALAR;
      npc.lifeMax = CalculateMaxHP(npc.lifeMax);
    }
    public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit) {
      base.ModifyHitPlayer(npc, target, ref damage, ref crit);
      damage = CalculateDamage(damage);
    }
    public override void OnKill(NPC npc) {
      base.OnKill(npc);
      if (npc.type != NPCID.TargetDummy && !npc.SpawnedFromStatue && !npc.friendly && !npc.townNPC) {
        long amount = CalculateMobXP(npc.lifeMax, CalculateDamage(npc.damage), npc.defense);

        if (Main.netMode == NetmodeID.SinglePlayer) {
          Main.LocalPlayer.GetModPlayer<LevelPlusModPlayer>().AddXP(amount);
        }
        else if (Main.netMode == NetmodeID.Server) {
          for (int i = 0; i < npc.playerInteraction.Length; ++i) {
            if (npc.playerInteraction[i]) {
              LevelPlus.Network.Packet.XPPacket.WritePacket(i, amount);
            }
          }
        }
      }
    }
  }
}

