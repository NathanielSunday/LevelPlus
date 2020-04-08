using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Tile_Entities;
using Terraria.ModLoader;

namespace levelplus
{
	class levelplusGlobalNPC : GlobalNPC
	{

		public override void NPCLoot(NPC npc)
		{
			base.NPCLoot(npc);


			//levelplusModPlayer.gainXP();
		}

		public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
		{
			if (npc.type != 488)
			{
				levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();

				double xp;

				if (npc.aiStyle == 6)
				{
					//Main.NewText("Worm");
					xp = damage;
				}
				else
					if (damage > npc.life)
						{
						xp = npc.life + damage;
						}
						else
						{
							xp = damage;
						}

				modPlayer.gainXP(xp);
			}
			

		}

		public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
		{
			if(npc.type != 488)
			{
				Player player = Main.player[projectile.owner];

				levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();
			
				double xp;

				if (npc.aiStyle == 6)
				{
					//Main.NewText("Worm");
					xp = damage;
				}
				else
					if (damage > npc.life)
					{
						xp = npc.life + damage;
					}
					else
					{
						xp = damage;
					}

			modPlayer.gainXP(xp);
			}
			
		}
	}
}
