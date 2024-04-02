// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.ID;

namespace LevelPlus.Content.Items.Consumables.Potions.Mana;

public class GreaterManaScalingPotion : BaseScalingPotion
{
  public override string Texture => "LevelPlus/Assets/Textures/Items/Consumables/GreaterManaScalingPotion";
  protected override short BasePotionId => ItemID.GreaterManaPotion;
  protected override float HealMana => 0.5f;

  public override void AddRecipes()
  {
    CreateRecipe()
      .AddIngredient<Essence>()
      .AddIngredient(ItemID.GreaterManaPotion)
      .AddTile(TileID.Bottles)
      .Register();

    CreateRecipe(5)
      .AddIngredient<Essence>(5)
      .AddIngredient<ManaScalingPotion>(5)
      .AddIngredient(ItemID.CrystalShard)
      .AddTile(TileID.Bottles)
      .DisableDecraft()
      .Register();
  }
}