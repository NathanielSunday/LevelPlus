// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content.Items
{
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
      Item.value = Item.buyPrice(0, 0, 80, 0);
      Item.rare = ItemRarityID.Lime;
      Item.UseSound = SoundID.Item4;
    }

    public override void AddRecipes()
    {
      CreateRecipe()
          .AddIngredient(ItemID.GoldBar, 1)
          .AddIngredient(ItemID.SoulofFlight, 1)
          .AddIngredient(ItemID.SoulofNight, 1)
          .AddTile(TileID.MythrilAnvil)
          .DisableDecraft()
          .Register();

      CreateRecipe()
          .AddIngredient(ItemID.GoldBar, 1)
          .AddIngredient(ItemID.SoulofFlight, 1)
          .AddIngredient(ItemID.SoulofLight, 1)
          .AddTile(TileID.MythrilAnvil)
          .DisableDecraft()
          .Register();

      CreateRecipe()
          .AddIngredient(ItemID.PlatinumBar, 1)
          .AddIngredient(ItemID.SoulofFlight, 1)
          .AddIngredient(ItemID.SoulofNight, 1)
          .AddTile(TileID.MythrilAnvil)
          .DisableDecraft()
          .Register();

      CreateRecipe()
          .AddIngredient(ItemID.PlatinumBar, 1)
          .AddIngredient(ItemID.SoulofFlight, 1)
          .AddIngredient(ItemID.SoulofLight, 1)
          .AddTile(TileID.MythrilAnvil)
          .DisableDecraft()
          .Register();
    }

    public override bool? UseItem(Player player)
    {
      LevelPlayer modPlayer = player.GetModPlayer<LevelPlayer>();
      modPlayer.StatReset();

      return true;
    }
  }
}