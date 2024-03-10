// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.ID;

namespace LevelPlus.Content.Items.Consumables.Potions.Healing;

public class GreaterHealingScalingPotion : BaseScalingPotion
{
  protected override float HealLife => 0.3f;

  public override void AddRecipes()
  {
    CreateRecipe()
      .AddIngredient<Essence>()
      .AddIngredient(ItemID.GreaterHealingPotion)
      .AddTile(TileID.Bottles)
      .Register();
    
    CreateRecipe(3)
      .AddIngredient<Essence>(3)
      .AddIngredient(ItemID.BottledWater, 3)
      .AddIngredient(ItemID.PixieDust, 3)
      .AddIngredient(ItemID.CrystalShard)
      .AddTile(TileID.Bottles)
      .Register();
    
    CreateRecipe(3)
      .AddIngredient<HealingScalingPotion>(3)
      .AddIngredient(ItemID.BottledWater, 3)
      .AddIngredient(ItemID.PixieDust, 3)
      .AddIngredient(ItemID.CrystalShard)
      .AddTile(TileID.Bottles)
      .DisableDecraft()
      .Register();
  }
}