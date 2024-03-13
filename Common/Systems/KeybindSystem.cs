// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Common.Systems;

[Autoload(Side = ModSide.Client)]
public class KeybindSystem : ModSystem
{
  public static ModKeybind OpenSpendUI { get; private set; }
  public static ModKeybind SpendFive { get; private set; }
  public static ModKeybind SpendTen { get; private set; }
  public static ModKeybind SpendAll { get; private set; }

  public override void Load()
  {
    OpenSpendUI =
      KeybindLoader.RegisterKeybind(Mod, Language.GetTextValue(LevelPlus.Instance.LocalizationPrefix + "OpenSpendUI"), Microsoft.Xna.Framework.Input.Keys.P);
    SpendFive = KeybindLoader.RegisterKeybind(Mod, Language.GetTextValue(LevelPlus.Instance.LocalizationPrefix + "SpendFive"),
      Microsoft.Xna.Framework.Input.Keys.LeftShift);
    SpendTen = KeybindLoader.RegisterKeybind(Mod, Language.GetTextValue(LevelPlus.Instance.LocalizationPrefix + "SpendTen"),
      Microsoft.Xna.Framework.Input.Keys.LeftControl);
    SpendAll = KeybindLoader.RegisterKeybind(Mod, Language.GetTextValue(LevelPlus.Instance.LocalizationPrefix + "SpendAll"),
      Microsoft.Xna.Framework.Input.Keys.LeftAlt);
  }

  public override void Unload()
  {
    OpenSpendUI = null;
    SpendFive = null;
    SpendTen = null;
    SpendAll = null;
  }
}
