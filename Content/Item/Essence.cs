using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content;

public class Essence : ModItem
{
    public override string Texture => ((LevelPlus)Mod).AssetPath + "Textures/Essence";

    public override void SetStaticDefaults()
    {
        // TODO Set up item animation textures
    }

    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(silver: 35);
        Item.rare = ItemRarityID.Green;
    }
}