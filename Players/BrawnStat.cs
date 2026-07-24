using LevelPlus.Configs;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Players;

public class BrawnStat : Stat
{
    public override LocalizedText Description =>
        base.Description.WithFormatArgs(Damage() * 100, WingTimeMax() * 100, PickSpeed() * 100);

    public override LocalizedText SpendTooltip =>
        base.SpendTooltip.WithFormatArgs(Damage() * 100, WingTimeMax() * 100, PickSpeed() * 100,
            Damage(true) * 100, WingTimeMax(true) * 100, PickSpeed(true) * 100);

    public override string Id => "Brawn";

    private float Damage(bool projected = false)
    {
        return (projected ? ProjectedValue : Value) * PlayConfiguration.Instance.Brawn.Damage;
    }

    private float WingTimeMax(bool projected = false)
    {
        return (projected ? ProjectedValue : Value) * PlayConfiguration.Instance.Brawn.MaxWingTime;
    }

    private float PickSpeed(bool projected = false)
    {
        return (projected ? ProjectedValue : Value) * PlayConfiguration.Instance.Brawn.PickSpeed;
    }

    public override void PostUpdateMiscEffects()
    {
        Player.GetDamage(DamageClass.Melee) += Damage();
        Player.pickSpeed *= 1 + PickSpeed();
        Player.wingTimeMax += (int)(WingTimeMax() * Player.wingTimeMax);
    }
}