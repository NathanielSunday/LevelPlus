using LevelPlus.Common.Config;
using LevelPlus.Common.Player;
using LevelPlus.Common.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content.Item;

public class Respec : ModItem
{
    public override string Texture => $"{Mod.Name}/Assets/Textures/Items/Respec";

    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 28;
        Item.useTime = 20;
        Item.useAnimation = ItemUseStyleID.HoldUp;
        Item.maxStack = 1;
        Item.consumable = true;
        Item.value = Terraria.Item.sellPrice(gold: 5);
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item4;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.GoldBar, 2)
            .AddIngredient<Essence>(100)
            .AddTile(TileID.MythrilAnvil)
            .DisableDecraft()
            .Register();
        
        CreateRecipe()
            .AddIngredient(ItemID.PlatinumBar, 2)
            .AddIngredient<Essence>(100)
            .AddTile(TileID.MythrilAnvil)
            .DisableDecraft()
            .Register();
    }

    public override bool? UseItem(Player player)
    {
        ModContent.GetInstance<StatSystem>().GetStats(player.whoAmI).ForEach(s => s.Value = 0);
        
        var levelPlayer = player.GetModPlayer<LevelPlayer>();
        var config = PlayConfiguration.Instance;

        levelPlayer.Points = levelPlayer.Level * config.Level.Points + config.StartingPoints;
        
        return true;
    }
}