// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content.Items;

public class Respec : ModItem
{
  public override string Texture => "LevelPlus/Assets/Textures/Items/Respec";

  public override void SetDefaults()
  {
    Item.width = 40;
    Item.height = 40;
    Item.useTime = 20;
    Item.useAnimation = 20;
    Item.useStyle = ItemUseStyleID.HoldUp;
    Item.maxStack = 1;
    Item.consumable = true;
    Item.value = Item.buyPrice(silver: 80);
    Item.rare = ItemRarityID.Lime;
    Item.UseSound = SoundID.Item4;
  }

  public override void AddRecipes()
  {
    CreateRecipe()
      .AddIngredient(ItemID.GoldBar)
      .AddIngredient<Essence>(50)
      .AddTile(TileID.MythrilAnvil)
      .Register();

    CreateRecipe()
      .AddIngredient(ItemID.PlatinumBar)
      .AddIngredient<Essence>(50)
      .AddTile(TileID.MythrilAnvil)
      .Register();
  }

  public override bool? UseItem(Player player)
  {
    //LevelStat modPlayer = player.GetModPlayer<LevelStat>();
    //modPlayer.StatReset();

    return true;
  }
}