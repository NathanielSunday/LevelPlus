using LevelPlus.Common.Config;
using LevelPlus.Common.Player;
using LevelPlus.Common.UI;
using LevelPlus.Content;
using LevelPlus.Network;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Common.GlobalNPC;

public class ScalingNPC : Terraria.ModLoader.GlobalNPC
{
    private static float Scalar = LevelPlayer.GetAverageLevel() * PlayConfiguration.Instance.ScalingPercentage;

    public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
    {
        // Invalid NPC checks
        return !(
            // I don't think you can kill a target dummy, but better to be safe
            entity.type == NPCID.TargetDummy ||
            // No farming
            entity.SpawnedFromStatue ||
            // No NPC murder either
            entity.friendly ||
            // No critters shall be harmed in the making of this mod
            entity.CountsAsACritter ||
            // Should take care of most projectiles giving xp
            entity.lifeMax < 5 ||
            // Killing segments shouldn't get you more xp
            entity.realLife > -1
        );
    }

    public static int CalculateExperience(NPC npc)
    {
        npc.CloneDefaults(npc.netID);
        return npc.lifeMax / 10 + npc.defense + npc.defDamage / 3;
    }

    public override void ModifyHitPlayer(NPC npc, Terraria.Player target, ref Terraria.Player.HurtModifiers modifiers)
    {
        modifiers.SourceDamage += Scalar;
    }

    public override void OnSpawn(NPC npc, IEntitySource source)
    {
        npc.life = npc.lifeMax = (int)(npc.lifeMax * (1 + Scalar)); //, npc.lifeMax, int.MaxValue);
    }

    public override void OnKill(NPC npc)
    {
        if (!npc.AnyInteractions()) return;

        var experience = CalculateExperience(npc);

        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.LocalPlayer.GetModPlayer<LevelPlayer>().GainExperience(experience);
        }

        if (Main.netMode != NetmodeID.Server) return;

        for (var i = 0; i < npc.playerInteraction.Length; i++)
        {
            if (!npc.playerInteraction[i]) continue;

            var packet = new GainExperiencePacket
            {
                Amount = experience
            };

            packet.Send(i);
        }
    }
    
    public override void ModifyGlobalLoot(GlobalLoot globalLoot)
    {
        globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<Essence>(), 100, 1, 5));
    }

    public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.Add(new BestiaryExperienceElement(npc));
    }
}