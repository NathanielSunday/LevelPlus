using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content;

public class ScalingPotion : ModItem
{
    protected virtual float HealLifePercent => 0.2f;
    protected virtual float HealManaPercent => 0.25f;
    
    public override string Texture => ((LevelPlus)Mod).AssetPath + "Textures/ScalingPotion";

    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 17;
        Item.useAnimation = ItemUseStyleID.DrinkLiquid;
        Item.UseSound = SoundID.Item3;
        Item.useTurn = true;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.potion = true;
        Item.healLife = (int)(HealLifePercent * 500);
        Item.healMana = (int)(HealManaPercent * 200);
        Item.value = Item.buyPrice(gold: 1);
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<Essence>(2)
            .AddIngredient(ItemID.HealingPotion)
            .AddIngredient(ItemID.ManaPotion)
            .AddTile(TileID.Bottles)
            .DisableDecraft()
            .Register();
    }

    public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
    {
        healValue = (int)(player.statLifeMax2 * HealLifePercent);
    }
    
    public override void GetHealMana(Player player, bool quickHeal, ref int healValue)
    {
        healValue = (int)(player.statManaMax2 * HealManaPercent);
    }
}