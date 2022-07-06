using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace levelplus.Items {
    public class Respec : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Insignia of Reform");
            Tooltip.SetDefault("This token enables the reallocation of Level+ skill points.\nHoist this token to return all Level+ invested skill points to the skill point pool.\nUnallocated points can be reinvested into skills again.");
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
            levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();
            modPlayer.StatReset();

            return true;
        }
    }
}