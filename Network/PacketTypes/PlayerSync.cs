// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using LevelPlus.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Network.PacketTypes {
  /// <summary>This should called when a player joins the server, and when any changes are made to a player</summary>
  /// <remarks>Needs XP, Stats, and whoAmI (Index) from player</remarks> 
  internal class PlayerSync : Packet {
    /// <summary>The XP of the player that is joining</summary>
    public long XP { get; set; }
    /// <summary>The stats of the player that is joining</summary>
    public int[] Stats { get; set; } = new int[System.Enum.GetValues(typeof(LevelPlusModPlayer.Stat)).Length];
    /// <summary>The whoAmI (Index) of the player that is joining</summary>
    public int Index { get; set; }
    protected override void OnRead(BinaryReader reader) {
      Index = reader.ReadByte();
      XP = reader.ReadInt64();
      foreach (LevelPlusModPlayer.Stat stat in System.Enum.GetValues(typeof(LevelPlusModPlayer.Stat))) {
        Stats[(int)stat] = reader.ReadInt32();
      }
    }
    protected override void Handle() {
      LevelPlusModPlayer player = Main.player[Index].GetModPlayer<LevelPlusModPlayer>();
      player.SetXP(XP);
      foreach (LevelPlusModPlayer.Stat stat in System.Enum.GetValues(typeof(LevelPlusModPlayer.Stat))) {
        player.SetStat(stat, Stats[(int)stat]);
      }
      if (Main.netMode == NetmodeID.Server) Send();
    }
    protected override void OnSend(ref ModPacket packet) {
      if (Main.netMode == NetmodeID.Server) ignoreClient = Index;
      packet.Write((byte)Index);
      packet.Write(XP);
      foreach (int i in System.Enum.GetValues(typeof(LevelPlusModPlayer.Stat))) {
        packet.Write(Stats[i]);
      }
    }
  }
}
