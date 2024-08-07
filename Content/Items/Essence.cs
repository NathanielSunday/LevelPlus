// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content.Items;

// A crafting resource for Level+ items
public class Essence : ModItem
{
  public override string Texture => "LevelPlus/Assets/Textures/Items/Essence";
  public static int Price => Item.buyPrice(silver: 35);

  public override void SetStaticDefaults()
  {
    Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 5));
    ItemID.Sets.AnimatesAsSoul[Item.type] = true;
    ItemID.Sets.ItemNoGravity[Item.type] = true;
  }

  public override void SetDefaults()
  {
    Item.width = 32;
    Item.height = 32;
    Item.maxStack = 99;
    Item.value = Item.buyPrice();
    Item.rare = ItemRarityID.Green;
    Item.value = Price;
  }
}