// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.ID;

namespace LevelPlus.Content.Items.Consumables.Potions.Healing;

public class LesserHealingScalingPotion : BaseScalingPotion
{
  public override string Texture => "LevelPlus/Assets/Textures/Items/Consumables/LesserHealingScalingPotion";
  protected override short BasePotionId => ItemID.LesserHealingPotion;
  protected override float HealLife => 0.1f;

  public override void AddRecipes()
  {
    CreateRecipe()
      .AddIngredient<Essence>()
      .AddIngredient(ItemID.LesserHealingPotion)
      .AddTile(TileID.Bottles)
      .Register();

    CreateRecipe(2)
      .AddIngredient<Essence>(2)
      .AddIngredient(ItemID.Mushroom)
      .AddIngredient(ItemID.Gel, 2)
      .AddIngredient(ItemID.Bottle, 2)
      .AddTile(TileID.Bottles)
      .Register();
  }
}