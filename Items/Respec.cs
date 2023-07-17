// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Core;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Items
{
    public class Respec : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault(Language.GetTextValue("Mods." + Mod.Name + ".DisplayName." + Name));
            Tooltip.SetDefault(Language.GetTextValue("Mods." + Mod.Name + ".Tooltip." + Name));
        }

        public override void SetDefaults() {
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = 1;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 0, 80, 0);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item4;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 1)
                .AddIngredient(ItemID.SoulofFlight, 1)
                .AddIngredient(ItemID.SoulofNight, 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 1)
                .AddIngredient(ItemID.SoulofFlight, 1)
                .AddIngredient(ItemID.SoulofLight, 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 1)
                .AddIngredient(ItemID.SoulofFlight, 1)
                .AddIngredient(ItemID.SoulofNight, 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 1)
                .AddIngredient(ItemID.SoulofFlight, 1)
                .AddIngredient(ItemID.SoulofLight, 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool? UseItem(Player player) {
            LevelPlusModPlayer modPlayer = player.GetModPlayer<LevelPlusModPlayer>();
            modPlayer.StatReset();

            return true;
        }
    }
}