using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Network;

public abstract class Packet : ModType
{
    protected abstract void Write(BinaryWriter writer);
    public abstract void Handle(BinaryReader reader, int whoAmI);

    public void Send(int toClient = -1, int ignoreClient = -1)
    {
        ModPacket packet = Mod.GetPacket();
        
        packet.Write(GetType().Name);
        Write(packet);
        
        packet.Send(toClient, ignoreClient);
    }
    
    public void Spread()
    {
        switch (Main.netMode)
        {
            case NetmodeID.MultiplayerClient:
                Send(-1, Main.myPlayer);
                break;
            
            case NetmodeID.Server:
                Send();
                break;
        }
    }
}