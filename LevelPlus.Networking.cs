// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace LevelPlus {
  partial class LevelPlus {
    public override void HandlePacket(BinaryReader reader, int whoAmI) {
      /*
      try {
        Type type = Type.GetType(reader.ReadString());
        Network.Packet packet = (Network.Packet)Activator.CreateInstance(type);
        packet.Read(reader, whoAmI);
      }
      catch (Exception) {
        Logger.ErrorFormat("Level+: Could not handle packet");
      }
      */
    }

    //I don't know whether we should conform to tMods non-OO nightmare, or stick with the changes I've made
    //I've decided I will move it back to here, but make methods to write packets here, instead of hardcoded like tMod
    public void WritePacket() {

    }
  }
}
