using LevelPlus.Content;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace LevelPlus.Common.GlobalNPC;

public class EssenceNPC : Terraria.ModLoader.GlobalNPC
{
     public override void ModifyGlobalLoot(GlobalLoot globalLoot)
     {
         globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<Essence>(), 100, 1, 5));
     }
}