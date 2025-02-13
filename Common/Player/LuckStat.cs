using System;
using LevelPlus.Common.Config;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LevelPlus.Common.Player;

public class LuckStat : Stat
{
    private Random rng;
    
    public override LocalizedText Description => base.Description.WithFormatArgs(Crit(), Luck(), Ammo());

    public override LocalizedText SpendTooltip =>
        base.SpendTooltip.WithFormatArgs(Crit(true), Luck(true), Ammo(true));

    public override string Id => "Luck";

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

    public override void Load()
    {
        rng = new Random();
    }

    public override void Unload()
    {
        rng = null;
    }

    public override void ModifyWeaponCrit(Item item, ref float crit)
    {
        crit += Crit();
    }

    public override void PostUpdateMiscEffects()
    {
        Player.luck += Luck();
    }

    public override bool CanConsumeAmmo(Item weapon, Item ammo)
    {
        return Ammo() < rng.Next(100);
    }
}