using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace levelplus.Items
{
	public class Restart : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Insignia of Rebirth");
			Tooltip.SetDefault("By hoisting this token, you lose the experience you have gained, and return to level 1.\n[c/ff0000:WHEN USED, ALL PROGRESS FOR Level+ WILL BE LOST FOR THIS CHARACTER.]");
		}

		public override void SetDefaults()
		{
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

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddTile(TileID.WorkBenches)
				.Register();
		}

		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<levelplusModPlayer>().initialize();

			return true;
		}
	}
}