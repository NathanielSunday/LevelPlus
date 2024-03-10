// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.ID;

namespace LevelPlus.Content.Items.Consumables.Potions.Healing;

public class HealingScalingPotion : BaseScalingPotion
{
  protected override float HealLife => 0.2f;

  public override void AddRecipes()
  {
    CreateRecipe()
      .AddIngredient<Essence>()
      .AddIngredient(ItemID.HealingPotion)
      .AddTile(TileID.Bottles)
      .Register();
    
    var potionsMade = 1;
    var potionsRequired = 2;
    for (var vanillaPotions = 0; vanillaPotions <= potionsRequired; ++vanillaPotions)
      CreateRecipe(potionsMade)
        .AddIngredient<Essence>(potionsMade + vanillaPotions)
        .AddIngredient<LesserHealingScalingPotion>(potionsRequired - vanillaPotions)
        .AddIngredient(ItemID.LesserHealingPotion, vanillaPotions)
        .AddIngredient(ItemID.GlowingMushroom)
        .AddTile(TileID.Bottles)
        .Register();
  }
}