// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LevelPlus.Core;

namespace LevelPlus.Network.PacketTypes {
  /// <summary>Used when an enemy dies. Should only be written by Server.</summary>
  /// <remarks>Needs Amount of XP a player should gain</remarks> 
  internal class XP : Packet {
    /// <summary>The amount of XP a player should gain</summary>
    public long Amount { get; set; }
    protected override void OnRead(BinaryReader reader) {
      if (Main.netMode == NetmodeID.MultiplayerClient)
        Amount = reader.ReadInt64();
    }
    protected override void Handle() {
      if (Main.netMode == NetmodeID.MultiplayerClient)
        Main.LocalPlayer.GetModPlayer<LevelPlusModPlayer>().AddXP(Amount);
    }
    protected override void OnSend(ref ModPacket packet) {
      packet.Write(Amount);
    }
  }
}
