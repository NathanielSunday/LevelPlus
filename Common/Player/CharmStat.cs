using LevelPlus.Common.Config;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Common.Player;

public class CharmStat : Stat
{
    public override LocalizedText Description => base.Description.WithFormatArgs(Damage(), MaxMinions(), MaxSentries(), FishingLevel());

    public override LocalizedText SpendTooltip =>
        base.SpendTooltip.WithFormatArgs(Damage(true), MaxMinions(true), MaxSentries(true), FishingLevel(true));

    public override string Id => "Charm";

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
        return (projected ? ProjectedValue : Value) * PlayConfiguration.Instance.Deft.PlacementSpeed;
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