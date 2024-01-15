// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Common.Systems {
  [Autoload(Side = ModSide.Client)]
  public class KeybindSystem : ModSystem {
    public static ModKeybind OpenSpendUI { get; private set; }
    public static ModKeybind SpendFive { get; private set; }
    public static ModKeybind SpendTen { get; private set; }
    public static ModKeybind SpendTwentyFive { get; private set; }

    public override void Load() {
      OpenSpendUI = KeybindLoader.RegisterKeybind(Mod, Language.GetTextValue("OpenSpendUI"), Microsoft.Xna.Framework.Input.Keys.P);
      SpendFive = KeybindLoader.RegisterKeybind(Mod, Language.GetTextValue("SpendFive"), Microsoft.Xna.Framework.Input.Keys.LeftShift);
      SpendTen = KeybindLoader.RegisterKeybind(Mod, Language.GetTextValue("SpendTen"), Microsoft.Xna.Framework.Input.Keys.LeftControl);
      SpendTwentyFive = KeybindLoader.RegisterKeybind(Mod, Language.GetTextValue("SpendTwentyFive"), Microsoft.Xna.Framework.Input.Keys.LeftAlt);
    }

    public override void Unload() {
      OpenSpendUI = null;
      SpendFive = null;
      SpendTen = null;
      SpendTwentyFive = null;
    }
  }
}
