using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content;

public class SuperScalingPotion : ScalingPotion
{
    protected override float HealLifePercent => 0.4f;
    protected override float HealManaPercent => 0.75f;
    
    public override string Texture => ((LevelPlus)Mod).AssetPath + "Textures/SuperScalingPotion";

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.value = Item.buyPrice(gold: 20);
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<Essence>(2)
            .AddIngredient(ItemID.SuperHealingPotion)
            .AddIngredient(ItemID.SuperManaPotion)
            .AddTile(TileID.Bottles)
            .DisableDecraft()
            .Register();
    }
}