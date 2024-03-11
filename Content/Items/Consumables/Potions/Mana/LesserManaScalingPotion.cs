// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.ID;

namespace LevelPlus.Content.Items.Consumables.Potions.Mana;

public class LesserManaScalingPotion : BaseScalingPotion
{
  public override string Texture => "LevelPlus/Assets/Textures/Items/Consumables/LesserManaScalingPotion";
  protected override short BasePotionId => ItemID.LesserManaPotion;
  protected override float HealMana => 0.125f;

  public override void AddRecipes()
  {
    CreateRecipe()
      .AddIngredient<Essence>()
      .AddIngredient(ItemID.LesserManaPotion)
      .AddTile(TileID.Bottles)
      .Register();
  }
}