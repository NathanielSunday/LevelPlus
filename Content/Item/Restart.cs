using LevelPlus.Common.Config;
using LevelPlus.Common.Player;
using LevelPlus.Common.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content;

public class Restart : ModItem
{
    public override string Texture => ((LevelPlus)Mod).AssetPath + "Textures/Restart";

    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 20;
        Item.useAnimation = ItemUseStyleID.HoldUp;
        Item.maxStack = 1;
        Item.consumable = true;
        Item.value = Item.sellPrice(silver: 20);
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item4;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<Essence>()
            .AddTile(TileID.WorkBenches)
            .DisableDecraft()
            .Register();
    }

    public override bool? UseItem(Player player)
    {
        ModContent.GetInstance<StatSystem>().GetStats(player.whoAmI).ForEach(s => s.Value = 0);
        
        var levelPlayer = player.GetModPlayer<LevelPlayer>();

        levelPlayer.Experience = 0;
        levelPlayer.Points = PlayConfiguration.Instance.StartingPoints;
        
        return true;
    }
}