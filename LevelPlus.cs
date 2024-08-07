// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using LevelPlus.Network.Packets;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus;

public class LevelPlus : Mod
{
  public string LocalizationPrefix => "Mods." + Name + ".";
  public string AssetPath => Name + "/Assets/";
  public bool IsCalamityModLoaded;
  public bool IsThoriumModLoaded;

  public override string Name => "LevelPlus";

  public static LevelPlus Instance => ModContent.GetInstance<LevelPlus>();
  
  public override void Load()
  {
    IsCalamityModLoaded = ModLoader.HasMod("CalamityMod");
    IsThoriumModLoaded = ModLoader.HasMod("ThoriumMod");
    
    Logger.Info($"Calamity: {IsCalamityModLoaded}");
    Logger.Info($"Thorium: {IsThoriumModLoaded}");
  }

  public override void Unload()
  {
  }

  // I decided against copying tMod's hard-code nightmare :)
  public override void HandlePacket(BinaryReader reader, int whoAmI)
  {
    var type = Type.GetType(reader.ReadString());
    try
    {
      var packet = Activator.CreateInstance(type!) as BasePacket;
      packet?.Read(reader, whoAmI);
    }
    catch (Exception)
    {
      Logger.ErrorFormat(Language.GetTextValue(LocalizationPrefix + "Network.UnhandledPacketError", type));
    }
  }
}