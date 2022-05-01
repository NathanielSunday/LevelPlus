using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace levelplus
{
	class levelplusGlobalItem : GlobalItem
	{
		public override void OnConsumeItem(Item item, Player player)
		{
			levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();

			switch (item.netID)
			{
				case ItemID.LifeCrystal:

			}
			base.OnConsumeItem(item, player);
		}
	}
}
