// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.ModLoader;
using System.IO;
using Terraria.Localization;
using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LevelPlus {
  public class LevelPlus : Mod {
    public static ModKeybind OpenSpendUI;
    public static ModKeybind SpendFive;
    public static ModKeybind SpendTen;
    public static ModKeybind SpendTwentyFive;

    public override void Load() {
      base.Load();
      OpenSpendUI = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("OpenSpendUI"), Microsoft.Xna.Framework.Input.Keys.P);
      SpendFive = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("SpendFive"), Microsoft.Xna.Framework.Input.Keys.LeftShift);
      SpendTen = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("SpendTen"), Microsoft.Xna.Framework.Input.Keys.LeftControl);
      SpendTwentyFive = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("SpendTwentyFive"), Microsoft.Xna.Framework.Input.Keys.LeftAlt);
    }

    public override void Unload() {
      base.Unload();
      OpenSpendUI = null;
      SpendFive = null;
      SpendTen = null;
      SpendTwentyFive = null;
    }

    public override void HandlePacket(BinaryReader reader, int whoAmI) {
      base.HandlePacket(reader, whoAmI);
      try {
        Type type = Type.GetType(reader.ReadString());
        Network.Packet packet = (Network.Packet)Activator.CreateInstance(type);
        packet.Read(reader, whoAmI);
      }
      catch (Exception) {
        Logger.ErrorFormat("Level+: Could not handle packet");
      }
    }
  }
}
