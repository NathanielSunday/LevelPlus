using LevelPlus.Configs;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Players;

public class BrawnStat : Stat
{
    public override LocalizedText Description =>
        base.Description.WithFormatArgs($"{Damage():P0}", $"{WingTimeMax():P0}", $"{PickSpeed():P0}");

    public override LocalizedText SpendTooltip =>
        base.SpendTooltip.WithFormatArgs($"{Damage():P0}", $"{WingTimeMax():P0}", $"{PickSpeed():P0}",
            $"{Damage(true):P0}", $"{WingTimeMax(true):P0}", $"{PickSpeed(true):P0}");

    public override string Id => "Brawn";

    public override Color Color => Color.DarkOrange;

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