using LevelPlus.Common.Config;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Common.Player;

public class BrawnStat : Stat
{
    public override LocalizedText Description => base.Description.WithFormatArgs(Damage(), WingTimeMax(), PickSpeed());

    public override LocalizedText SpendTooltip =>
        base.SpendTooltip.WithFormatArgs(Damage(true), WingTimeMax(true), PickSpeed(true));

    public override string Id => "Brawn";

    private float Damage(bool projected = false)
    {
        return (projected ? ProjectedValue : Value) * PlayConfiguration.Instance.Endurance.Life;
    }

    private float WingTimeMax(bool projected = false)
    {
        return (projected ? ProjectedValue : Value) * PlayConfiguration.Instance.Endurance.Defense;
    }

    private float PickSpeed(bool projected = false)
    {
        return (projected ? ProjectedValue : Value) * PlayConfiguration.Instance.Endurance.LifeRegenCost * 2;
    }

    public override void PostUpdateMiscEffects()
    {
        Player.GetDamage(DamageClass.Melee) += Damage();
        Player.pickSpeed *= 1 + PickSpeed();
        Player.wingTimeMax += (int)(WingTimeMax() * Player.wingTimeMax);
    }
}