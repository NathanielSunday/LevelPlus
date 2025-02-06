using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Network;

public abstract class Packet
{
    public void Send(int toClient = -1, int ignoreClient = -1)
    {
        ModPacket packet = ModContent.GetInstance<LevelPlus>().GetPacket();

        packet.Write(GetType().Name);
        Write(packet);

        packet.Send(toClient, ignoreClient);
    }

    public void Receive(BinaryReader reader, int whoAmI)
    {
        Read(reader, whoAmI);
    }

    protected abstract void Write(BinaryWriter writer);

    protected abstract void Read(BinaryReader reader, int whoAmI);

    protected void Spread()
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