using LevelPlus.Common.Config;
using LevelPlus.Common.Player;
using LevelPlus.Network;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using ThoriumMod.Items.Donate;

namespace LevelPlus.Common.GlobalNPC;

public class ScalingNPC : Terraria.ModLoader.GlobalNPC
{
    private int experience;
    private float scalar;

    public override bool InstancePerEntity => true;

    private static int CalculateExperience(NPC npc)
    {
        return npc.lifeMax / 3 + npc.defense;
    }

    public override void ModifyHitPlayer(NPC npc, Terraria.Player target, ref Terraria.Player.HurtModifiers modifiers)
    {
        modifiers.SourceDamage += scalar;
    }

    public override void OnSpawn(NPC npc, IEntitySource source)
    {
        scalar = LevelPlayer.GetAverageLevel() * PlayConfiguration.Instance.ScalingPercentage;
        experience = CalculateExperience(npc);
    }

    public override void OnKill(NPC npc)
    {
        if (!npc.AnyInteractions() ||
            npc.type == NPCID.TargetDummy ||
            npc.SpawnedFromStatue ||
            npc.friendly ||
            npc.CountsAsACritter)
            return;

        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.LocalPlayer.GetModPlayer<LevelPlayer>().GainExperience(experience);
        }

        if (Main.netMode == NetmodeID.Server)
        {
            for (int i = 0; i < npc.playerInteraction.Length; i++)
            {
                if (!npc.playerInteraction[i]) continue;

                var packet = new GainExperiencePacket
                {
                    Amount = experience
                };
                
                packet.Send(i);
            }
        }
    }
}