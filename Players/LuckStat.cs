using System;
using LevelPlus.Configs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;

namespace LevelPlus.Players;

public class LuckStat : Stat
{
    private static Random rng;

    public override LocalizedText Description => base.Description.WithFormatArgs($"{Crit():P0}", $"{Luck():P0}", $"{Ammo()}%");

    public override LocalizedText SpendTooltip => base.SpendTooltip.WithFormatArgs($"{Crit():P0}", $"{Luck():P0}", $"{Ammo()}%",
        $"{Crit(true):P0}", $"{Luck(true):P0}", $"{Ammo(true)}%");

    public override string Id => "Luck";

    public override Color Color => Color.LawnGreen;

    private float Crit(bool projected = false)
    {
        return (projected ? ProjectedValue : Value) * PlayConfiguration.Instance.Luck.Crit;
    }

    private float Luck(bool projected = false)
    {
        return (projected ? ProjectedValue : Value) * PlayConfiguration.Instance.Luck.TerrariaLuck;
    }

    private float Ammo(bool projected = false)
    {
        return Math.Min(1, (projected ? ProjectedValue : Value) * PlayConfiguration.Instance.Luck.Ammo) * 100;
    }

    public override void Initialize()
    {
        base.Initialize();
        rng = new Random(DateTime.Now.Millisecond);
    }

    public override void ModifyWeaponCrit(Item item, ref float crit)
    {
        crit += Crit();
    }

    public override void ModifyLuck(ref float luck)
    {
        luck += Luck();
    }

    public override bool CanConsumeAmmo(Item weapon, Item ammo)
    {
        return Ammo() < rng.Next(100);
    }
}