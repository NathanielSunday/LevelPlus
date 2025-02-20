using System.Collections.Generic;
using LevelPlus.Common.Player;
using LevelPlus.Network;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LevelPlus.Common.System;

public class OreExperienceSystem : ModSystem
{
    public static List<Point16> placedOres;

    public override void ClearWorld()
    {
        placedOres = [];
    }

    public override void LoadWorldData(TagCompound tag)
    {
        placedOres = tag.Get<List<Point16>>("placedOres");
    }

    public override void SaveWorldData(TagCompound tag)
    {
        tag["placedOres"] = placedOres;
    }
}

public class OreExperienceTile : GlobalTile
{
    private void AddOre(Point16 pos)
    {
        switch (Main.netMode)
        {
            case NetmodeID.SinglePlayer:
                OreExperienceSystem.placedOres.Add(pos);
                break;
            case NetmodeID.MultiplayerClient:
                var packet = new OrePlacePacket()
                {
                    Position = pos
                };
                
                packet.Send();
                break;
        }
    }
    
    // Using CanPlace since it doesn't explicitly say "called on client" like PlaceInWorld
    public override bool CanPlace(int i, int j, int type)
    {
        // Grab the base (I'm not overly sure if this or any derivative of this is necessary)
        var canPlace = base.CanPlace(i, j, type);

        // If it's not an ore or not a host, return whatever base is
        if (!TileID.Sets.Ore[type]) return canPlace;

        // If it's false, don't bother adding it to the list
        if (!canPlace) return false;

        AddOre(new Point16(i, j));

        return true;
    }

    // Disallow the ability to use the swap functionality to get higher xp on a "xp legit" coord
    public override bool CanReplace(int i, int j, int type, int tileTypeBeingPlaced)
    {
        var canReplace = base.CanReplace(i, j, type, tileTypeBeingPlaced);
    
        // If it's not an ore or not a host, return whatever base is
        if (!TileID.Sets.Ore[type]) return canReplace;
    
        // If it's false, don't bother adding it to the list
        if (!canReplace) return false;
    
        AddOre(new Point16(i, j));
    
        return true;
    }

    public override void Drop(int i, int j, int type)
    {
        // If it's not an ore or not a host, don't give XP
        if (!TileID.Sets.Ore[type]) return;

        // Remove the ore from the list and don't give XP
        if (OreExperienceSystem.placedOres.Contains(new Point16(i, j)))
        {
            OreExperienceSystem.placedOres.Remove(new Point16(i, j));
            return;
        }

        // Grab the pos and experience this should give
        var worldPos = new Vector2(i, j) * 16;
        var experience = new Item(TileLoader.GetItemDropFromTypeAndStyle(type)).value / 200;


        switch (Main.netMode)
        {
            case NetmodeID.SinglePlayer:
                Main.LocalPlayer.GetModPlayer<LevelPlayer>().GainExperience(experience);
                break;
            
            case NetmodeID.Server:
                var player = Main.player[0];

                foreach (var ap in Main.ActivePlayers)
                {
                    if (ap.position.Distance(worldPos) < player.position.Distance(worldPos))
                    {
                        player = ap;
                    }
                }

                var packet = new GainExperiencePacket
                {
                    Amount = experience
                };

                packet.Send(player.whoAmI);
                break;
        }
    }
}