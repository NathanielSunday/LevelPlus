using LevelPlus.Common.Config;
using LevelPlus.Common.Player;
using LevelPlus.Common.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content;

public class Respec : ModItem
{
    public override string Texture => ((LevelPlus)Mod).AssetPath + "Textures/Respec";

    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 20;
        Item.useAnimation = ItemUseStyleID.HoldUp;
        Item.maxStack = 1;
        Item.consumable = true;
        Item.value = Item.sellPrice(gold: 5);
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item4;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.GoldBar)
            .AddIngredient<Essence>(50)
            .AddTile(TileID.MythrilAnvil)
            .DisableDecraft()
            .Register();
        
        CreateRecipe()
            .AddIngredient(ItemID.PlatinumBar)
            .AddIngredient<Essence>(50)
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