using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content;

public class GreaterScalingPotion : ScalingPotion
{
    protected override float HealLifePercent => 0.3f;
    protected override float HealManaPercent => 0.5f;
    
    public override string Texture => ((LevelPlus)Mod).AssetPath + "Textures/GreaterScalingPotion";

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.value = Item.buyPrice(gold: 5);
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<Essence>(2)
            .AddIngredient(ItemID.GreaterHealingPotion)
            .AddIngredient(ItemID.GreaterManaPotion)
            .AddTile(TileID.Bottles)
            .DisableDecraft()
            .Register();
    }
}