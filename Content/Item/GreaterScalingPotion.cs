using Terraria.ID;

namespace LevelPlus.Content.Item;

public class GreaterScalingPotion : ScalingPotion
{
    protected override float HealLifePercent => 0.3f;
    protected override float HealManaPercent => 0.5f;
    
    public override string Texture => $"{Mod.Name}/Assets/Textures/Items/GreaterScalingPotion";

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.value = Terraria.Item.buyPrice(gold: 5);
        Item.rare = new Terraria.Item(ItemID.GreaterHealingPotion).rare + 1;
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
        
        CreateRecipe()
            .AddIngredient<Essence>(25)
            .AddIngredient<ScalingPotion>()
            .AddTile(TileID.Bottles)
            .DisableDecraft()
            .Register();
    }
}