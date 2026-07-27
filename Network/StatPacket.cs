using System.IO;
using LevelPlus.Players;
using LevelPlus.Systems;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Network;

public class StatPacket : Packet
{
    // I need to write the player ID due to server forwarding
    public int Player { get; set; }
    
    public string Id { get; set; }

    public int Value { get; set; }

    protected override bool Forward => true;

    public void SetStat(Stat stat)
    {
        Player = stat.Player.whoAmI;
        Id = stat.Id;
        Value = stat.Value;
    }

    protected override void Write(BinaryWriter writer)
    {
        writer.Write(Player);
        writer.Write(Id);
        writer.Write(Value);
    }

    protected override void Read(BinaryReader reader, int whoAmI)
    {
        Player = reader.ReadInt32();
        Id = reader.ReadString();
        Value = reader.ReadInt32();

        if (Id == "level")
            Main.player[Player].GetModPlayer<LevelPlayer>().Level = Value;
        else
            ModContent.GetInstance<StatSystem>().GetStatOfPlayer(Player, Id).Value = Value;
    }
}