using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content;

public class LesserScalingPotion : ScalingPotion
{
    protected override float HealLifePercent => 0.1f;
    protected override float HealManaPercent => 0.125f;
    
    public override string Texture => ((LevelPlus)Mod).AssetPath + "Textures/LesserScalingPotion";

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.value = Item.buyPrice(silver: 50);
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<Essence>(2)
            .AddIngredient(ItemID.LesserHealingPotion)
            .AddIngredient(ItemID.LesserManaPotion)
            .AddTile(TileID.Bottles)
            .DisableDecraft()
            .Register();
    }
}