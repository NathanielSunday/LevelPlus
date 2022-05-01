using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace levelplus.Items
{
	public class Restart : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Reset Token");
      Tooltip.SetDefault("Consume to reset your ENTIRE character.\n\nThis item will set your character's level back to 1,\nas if you had just started a new character.");
    }


		public override void SetDefaults() 
		{
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.maxStack = 1;
			Item.consumable = true;
			Item.value = Item.buyPrice(0, 0, 0, 0);
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item2;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ItemID.DirtBlock, 1)
				.Register();
		}

		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<levelplusModPlayer>().initialize();

			return true;
		}
	}
}