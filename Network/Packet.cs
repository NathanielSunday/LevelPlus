using Terraria.ModLoader;

namespace LevelPlus.Network;

public interface Packet
{
    void Handle(Mod mod, int whoAmI);
}