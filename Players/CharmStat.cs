using LevelPlus.Configs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Players;

public class CharmStat : Stat
{
    public override LocalizedText Description =>
        base.Description.WithFormatArgs($"{Damage():P0}", MaxMinions(), MaxSentries(), $"{100 * FishingLevel():F0}");

    public override LocalizedText SpendTooltip =>
        base.SpendTooltip.WithFormatArgs($"{Damage():P0}", MaxMinions(), MaxSentries(), $"{100 * FishingLevel():F0}",
            $"{Damage(true):P0}", MaxMinions(true), MaxSentries(true), $"{100 * FishingLevel(true):F0}");

    public override string Id => "Charm";

    public override Color Color => Color.Cyan;

    private float Damage(bool projected = false)
    {
        return (projected ? ProjectedValue : Value) * PlayConfiguration.Instance.Charm.Damage;
    }

    private int MaxMinions(bool projected = false)
    {
        return (projected ? ProjectedValue : Value) / PlayConfiguration.Instance.Charm.MinionCost;
    }

    private int MaxSentries(bool projected = false)
    {
        return (projected ? ProjectedValue : Value) / PlayConfiguration.Instance.Charm.SentryCost;
    }

    private float FishingLevel(bool projected = false)
    {
        return (projected ? ProjectedValue : Value) * PlayConfiguration.Instance.Charm.Fishing;
    }

    public override void PostUpdateMiscEffects()
    {
        Player.GetDamage(DamageClass.Summon) += Damage();
        Player.maxMinions += MaxMinions();
        Player.maxTurrets += MaxSentries();
    }

    public override void GetFishingLevel(Item fishingRod, Item bait, ref float fishingLevel)
    {
        fishingLevel += FishingLevel();
    }
}