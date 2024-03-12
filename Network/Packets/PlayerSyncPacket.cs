// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.IO;
using LevelPlus.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Network.Packets;
/// This should called when a player joins the server, and when any changes are made to a player
/// <remarks>Needs XP, and only what stats changed, and whoAmI (Index) from player.</remarks> 
public class PlayerSyncPacket : BasePacket
{
  /// <summary>The XP of the player that is joining</summary>
  public int Level { get; set; }
  /// <summary>The stats of the player that is joining</summary>
  public Dictionary<string, int> Stats { get; set; } = new();
  /// <summary>The whoAmI (Index) of the player that is joining</summary>
  public int Index { get; set; }
  
  protected override void OnRead(BinaryReader reader)
  {
    Index = reader.ReadByte();
    Level = reader.ReadInt32();
    // Read the stats that change here (either a do while or a very odd for)
    while(reader.BaseStream.Position < reader.BaseStream.Length)
    {
      Stats.Add(reader.ReadString(), reader.ReadInt32());
    }
  }
  
  protected override void Handle()
  {
    var player = Main.player[Index].GetModPlayer<StatPlayer>();
    player.SetXp(StatPlayer.LevelToXp(Level));
    
    foreach (var stat in Stats) {
      player.SetStat(stat.Key, stat.Value);
    }
    
    if (Main.netMode == NetmodeID.Server) Send();
  }
  
  protected override void OnSend(ref ModPacket packet)
  {
    if (Main.netMode == NetmodeID.Server) IgnoreClient = Index;
    packet.Write((byte)Index);
    packet.Write(Level);
    
    foreach (var stat in Stats) {
      packet.Write(stat.Key);
      packet.Write(stat.Value);
    }
  }
}