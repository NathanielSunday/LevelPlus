// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Configs;
using System;
using LevelPlus.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Common.GlobalNPCs;

// ReSharper disable once InconsistentNaming
class ScalingGlobalNPC : GlobalNPC
{
  public override bool InstancePerEntity => true;

  float scalar = 1.0f;
  int numPlayers = 1;
  //float topDamage;

  private long CalculateMobXp(int npcLife, int npcDefence)
  {
    numPlayers = Math.Max(numPlayers, 1); // Avoid divide by zero
    float playerScalar = numPlayers == 1 ? 1.0f : (float)(Math.Log(numPlayers - 1) + 1.25f) / numPlayers;
    return (long)(
      (npcLife / scalar / 3
       + npcDefence)
      * playerScalar);
  }

  private int CalculateMaxLife(int maxLife)
  {
    return (int)Math.Clamp(maxLife * scalar, 0, int.MaxValue);
  }

  public override void OnSpawn(NPC npc, IEntitySource source)
  {
    if (!MobConfig.Instance.ScalingEnabled) return;

    float averageLevel = 0;

    foreach (Player player in Main.player)
    {
      if (!player.active) continue;
      numPlayers++;
      averageLevel += StatPlayer.XpToLevel(player.GetModPlayer<StatPlayer>().Xp);
    }

    averageLevel /= numPlayers;
    scalar += averageLevel * MobConfig.Instance.LevelScalar;
    npc.lifeMax = CalculateMaxLife(npc.lifeMax);
  }

  public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
  {
    if (modifiers.PvP) return;
    modifiers.SourceDamage.Scale(scalar);
  }

  public override void OnKill(NPC npc)
  {
    // A list of the reasons to NOT give XP
    if (npc.lastInteraction == 255 ||
        npc.type == NPCID.TargetDummy ||
        npc.SpawnedFromStatue ||
        npc.friendly ||
        npc.townNPC ||
        npc.CountsAsACritter ||
        npc.immortal)
      return;
    long amount = CalculateMobXp((int)(npc.lifeMax * (npc.aiStyle != NPCAIStyleID.Worm ? 1.0f : 0.166f)),
      npc.defense);

    // Bestiary increments only when player kills the mob. Double the xp for the first kill.
    int killCount = Main.BestiaryTracker.Kills.GetKillCount(npc);
    if (killCount == 1)
    {
      amount *= 2;
      CombatText.NewText(npc.getRect(), Color.Aqua, Language.GetTextValue("Popup.BestiaryUnlocked"),
        true);
    }

    if (Main.netMode == NetmodeID.SinglePlayer)
    {
      Main.LocalPlayer.GetModPlayer<StatPlayer>().Xp += amount;
    }
    else if (Main.netMode == NetmodeID.Server)
    {
      for (int i = 0; i < npc.playerInteraction.Length; ++i)
      {
        if (npc.playerInteraction[i])
        {
          //LevelPlus.Network.Packet.XPPacket.WritePacket(i, amount);
        }
      }
    }
  }
}