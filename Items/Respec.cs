using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace levelplus.Items
{
	public class Respec : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Stat Token"); 
			Tooltip.SetDefault("Consume to reset your stats.");
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
			Item.value = Item.buyPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item2;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ItemID.RangerEmblem, 1)
				.AddIngredient(ItemID.LifeCrystal, 1)
				.AddIngredient(ItemID.CrystalShard, 10)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.WarriorEmblem, 1)
				.AddIngredient(ItemID.LifeCrystal, 1)
				.AddIngredient(ItemID.CrystalShard, 10)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.SorcererEmblem, 1)
				.AddIngredient(ItemID.LifeCrystal, 1)
				.AddIngredient(ItemID.CrystalShard, 10)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.SummonerEmblem, 1)
				.AddIngredient(ItemID.LifeCrystal, 1)
				.AddIngredient(ItemID.CrystalShard, 10)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}

		public override bool? UseItem(Player player)
		{

			levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();
			modPlayer.StatReset();

			return true;
		}
	}
}