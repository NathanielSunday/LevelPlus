// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content.Items
{
  public class Restart : ModItem
  {
    public override string Texture => "LevelPlus/Assets/Textures/Items/Restart";
    public override void SetDefaults()
    {
      Item.width = 40;
      Item.height = 40;
      Item.useTime = 20;
      Item.useAnimation = 20;
      Item.useStyle = ItemUseStyleID.HoldUp;
      Item.maxStack = 1;
      Item.consumable = true;
      Item.value = Item.buyPrice(0, 0, 0, 0);
      Item.rare = ItemRarityID.Lime;
      Item.UseSound = SoundID.Item4;
    }

    public override void AddRecipes()
    {
      CreateRecipe()
          .AddTile(TileID.WorkBenches)
          .Register();
    }

    public override bool? UseItem(Player player)
    {
      player.GetModPlayer<LevelPlayer>().StatInitialize();

      return true;
    }
  }
}