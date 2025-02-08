using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Network;

public abstract class Packet
{
    // Forward this packet to all other clients
    protected abstract bool Forward { get; }

    protected abstract void Write(BinaryWriter writer);

    protected abstract void Read(BinaryReader reader, int whoAmI);
    
    public void Send(int toClient = -1, int ignoreClient = -1)
    {
        var packet = ModContent.GetInstance<LevelPlus>().GetPacket();

        packet.Write(GetType().ToString());
        Write(packet);

        packet.Send(toClient, ignoreClient);
    }

    public void Receive(BinaryReader reader, int whoAmI)
    {
        Read(reader, whoAmI);
        if (Forward) ForwardAll(whoAmI);
    }

    private void ForwardAll(int whoAmI)
    {
        if (Main.netMode == NetmodeID.Server)
        {
            Send(-1, whoAmI);
        }
    }
}