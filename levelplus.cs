// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.ModLoader;
using System.IO;
using Terraria.Localization;
using System;

namespace LevelPlus {
  public class LevelPlus : Mod {
    public static LevelPlus Instance { get; private set; }
    public LevelPlus() { Instance = this; }

    public static ModKeybind SpendUIHotKey;
    public static ModKeybind SpendModFive;
    public static ModKeybind SpendModTen;
    public static ModKeybind SpendModTwentyFive;

    public override void Load() {
      base.Load();
      SpendUIHotKey = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods." + Name + ".Keybind.UI"), Microsoft.Xna.Framework.Input.Keys.P);
      SpendModFive = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods." + Name + ".Keybind.Five"), Microsoft.Xna.Framework.Input.Keys.LeftShift);
      SpendModTen = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods." + Name + ".Keybind.Ten"), Microsoft.Xna.Framework.Input.Keys.LeftControl);
      SpendModTwentyFive = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods." + Name + ".Keybind.TwentyFive"), Microsoft.Xna.Framework.Input.Keys.LeftAlt);
    }

    public override void Unload() {
      base.Unload();
      SpendUIHotKey = null;
      SpendModFive = null;
      SpendModTen = null;
      SpendModTwentyFive = null;
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
