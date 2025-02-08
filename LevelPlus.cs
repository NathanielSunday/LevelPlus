using System;
using System.IO;
using LevelPlus.Network;
using Terraria.ModLoader;

namespace LevelPlus;

public class LevelPlus : Mod
{
    public string LocalizationPrefix => "Mods." + Name + ".";
    public string AssetPath => Name + "/Assets/";
    public bool IsCalamityLoaded { get; private set; }
    public bool IsThoriumLoaded { get; private set; }

    public override void Load()
    {
        IsCalamityLoaded = ModLoader.HasMod("CalamityMod");
        IsThoriumLoaded = ModLoader.HasMod("ThoriumMod");
        
        Logger.InfoFormat("{0}: Calamity = {1}", Name, IsCalamityLoaded);
        Logger.InfoFormat("{0}: Thorium = {1}", Name, IsThoriumLoaded);
    }

    public override void Unload()
    {
        IsCalamityLoaded = false;
        IsThoriumLoaded = false;
    }
    
    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        var typeString = reader.ReadString();
        var type = Type.GetType(typeString);
        
        if (type is null || 
            !type.IsAssignableFrom(typeof(Packet)))
        {
            Logger.WarnFormat("{0}: Packet of unknown type \"{1}\"", Name, typeString);
            return;
        }

        var packet = (Packet)Activator.CreateInstance(type);
        
        if (packet is null)
        {
            Logger.WarnFormat("{0}: Failed to instantiate Packet of type \"{1}\"", Name, typeString);
            return;
        }
        
        packet.Receive(reader, whoAmI);
    }
}