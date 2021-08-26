using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace levelplus
{
	class levelplusGlobalNPC : GlobalNPC
		
	{
		private List<levelplusModPlayer> playerHits = new List<levelplusModPlayer>();

		public override bool InstancePerEntity => true;

		public override void SetDefaults(NPC npc)
		{
			base.SetDefaults(npc);
			float averageLevel = 0;

			Player player;
			levelplusModPlayer modPlayer;

			for (int i = 0; i < Main.PlayerList.Count; ++i)
			{
				player = Main.player[i];
				modPlayer = player.GetModPlayer<levelplusModPlayer>();
				averageLevel += modPlayer.getLevel();
			}

			averageLevel /= Main.PlayerList.Count;

			npc.damage += (int)(npc.damage * (averageLevel / 20));
			npc.lifeMax += (int)(npc.lifeMax * (averageLevel / 20));
		}


        public override void OnKill(NPC npc) {

			if (playerHits != null && !npc.SpawnedFromStatue && npc.type != NPCID.TargetDummy)
				foreach (levelplusModPlayer i in playerHits) {
					if (npc.boss) {
						i.gainXP(npc.lifeMax / 3);
					} else {
						i.gainXP(npc.lifeMax / 4);
					}


				}
			else {
				Player player = Main.player[npc.FindClosestPlayer()];
				levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();

				modPlayer.gainXP(npc.lifeMax / 4);
			}
		}

		public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
		{
			if (npc.type != NPCID.TargetDummy && !npc.SpawnedFromStatue)
			{	
				bool check = false;
				if(playerHits != null)
					foreach (levelplusModPlayer i in playerHits)
					{
						if(i == player.GetModPlayer<levelplusModPlayer>())
						{
							check = true;
							break;
						}
					}
				if (!check)
				{
					playerHits.Add(player.GetModPlayer<levelplusModPlayer>());
				}
				
			}
		}

		public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
		{
			if(npc.type != NPCID.TargetDummy && !npc.SpawnedFromStatue)
			{
				bool check = false;
				Player player = Main.player[projectile.owner];
				if(playerHits != null)
					foreach (levelplusModPlayer i in playerHits)
					{
						if (i == player.GetModPlayer<levelplusModPlayer>())
						{
							check = true;
							break;
						}
					}
				if (!check)
				{
					playerHits.Add(player.GetModPlayer<levelplusModPlayer>());
				}
			}
		}
	}
}
