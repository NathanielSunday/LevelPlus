// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.ID;

namespace LevelPlus.Content.Items.Consumables.Potions.Healing;

public class SuperHealingScalingPotion : BaseScalingPotion
{
  public override string Texture => "LevelPlus/Assets/Textures/Items/Consumables/SuperHealingScalingPotion";
  protected override short BasePotionId => ItemID.SuperHealingPotion;
  protected override float HealLife => 0.4f;

  public override void AddRecipes()
  {
    CreateRecipe()
      .AddIngredient<Essence>()
      .AddIngredient(ItemID.SuperHealingPotion)
      .AddTile(TileID.Bottles)
      .Register();
    
    var potionsMade = 4;
    var potionsRequired = 4;
    for (var vanillaPotions = 0; vanillaPotions <= potionsRequired; ++vanillaPotions)
      CreateRecipe(potionsMade)
        .AddIngredient<Essence>(potionsMade + vanillaPotions)
        .AddIngredient<GreaterHealingScalingPotion>(potionsRequired - vanillaPotions)
        .AddIngredient(ItemID.GreaterHealingPotion, vanillaPotions)
        .AddIngredient(ItemID.FragmentNebula)
        .AddIngredient(ItemID.FragmentSolar)
        .AddIngredient(ItemID.FragmentStardust)
        .AddIngredient(ItemID.FragmentVortex)
        .AddTile(TileID.Bottles)
        .Register();
  }
}