// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content.Items;

// A crafting resource for Level+ items
public class Essence : ModItem
{
  //public override string Texture => "LevelPlus/Assets/Textures/Items/Essence";
  public override void SetDefaults()
  {
    Item.width = 40;
    Item.height = 40;
    Item.maxStack = 99;
    Item.value = Item.buyPrice();
    Item.rare = ItemRarityID.Green;
  }
}