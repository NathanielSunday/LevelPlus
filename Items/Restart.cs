// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Core;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Items
{
    public class Restart : ModItem {
        public override void SetStaticDefaults() {

        }

        public override void SetDefaults() {
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = 1;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 0, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item4;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override bool? UseItem(Player player) {
            player.GetModPlayer<LevelPlusModPlayer>().StatInitialize();
            
            return true;
        }
    }
}