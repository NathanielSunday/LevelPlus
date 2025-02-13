using System;
using LevelPlus.Common.System;
using LevelPlus.Network;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.Player;

// A class to make developing stats faster
public abstract class Stat : ModPlayer
{
    // The value of, or amount of points invested in, the stat.
    public int Value { get; set; }

    // The value that would be while spending
    public int ProjectedValue
    {
        get
        {
            var keybind = ModContent.GetInstance<KeybindSystem>();
            return Value + Math.Min(ModContent.GetInstance<StatSystem>().Points,
                (keybind.SpendMultFive.Current ? 5 : 1) *
                (keybind.SpendMultTen.Current ? 10 : 1) *
                (keybind.SpendMultTwenty.Current ? 20 : 1)
            );
        }
    }

    // The LocalizedText for the name of the Stat
    public virtual LocalizedText Name =>
        Language.GetText(((LevelPlus)Mod).LocalizationPrefix + "Stats." + Id + ".DisplayName");

    // The LocalizedText for the description. Should be pre-formatted with args.
    public virtual LocalizedText Description =>
        Language.GetText(((LevelPlus)Mod).LocalizationPrefix + "Stats." + Id + ".Tooltip");

    // The LocalizedText for the description for next point(s) spent. Should be pre-formatted with args.
    public virtual LocalizedText SpendTooltip => Description;

    // The path of the icon to be used in the UI.
    public virtual string IconPath => ((LevelPlus)Mod).AssetPath + "Icons/" + Id;
    
    // The color to modify the UI element by.
    public virtual Color Color => Color.White;
    
    // The access key for stat, usually the name.
    public abstract string Id { get; }
    
    public override void LoadData(TagCompound tag)
    {
        Value = tag.ContainsKey(Id) ? tag.GetInt(Id) : 0;
    }

    public override void SaveData(TagCompound tag)
    {
        tag.Add(Id, Value);
    }

    public override void CopyClientState(ModPlayer targetCopy)
    {
        ((Stat)targetCopy).Value = Value;
    }

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
        if (!newPlayer) return;

        var packet = new StatPacket
        {
            Id = Id,
            Value = Value,
        };

        packet.Send(toWho, fromWho);
    }

    public override void SendClientChanges(ModPlayer clientPlayer)
    {
        if (((Stat)clientPlayer).Value == Value) return;
        SyncPlayer(0, 0, true);
    }
}