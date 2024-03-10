// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.ID;

namespace LevelPlus.Content.Items.Consumables.Potions.Mana;

public class SuperManaScalingPotion : BaseScalingPotion
{
  protected override float HealMana => 0.75f;

  public override void AddRecipes()
  {
    CreateRecipe()
      .AddIngredient<Essence>()
      .AddIngredient(ItemID.SuperManaPotion)
      .AddTile(TileID.Bottles)
      .Register();
    
    var potionsMade = 15;
    var potionsRequired = 15;
    for (var vanillaPotions = 0; vanillaPotions <= potionsRequired; ++vanillaPotions)
      CreateRecipe(potionsMade)
        .AddIngredient<Essence>(potionsMade + vanillaPotions)
        .AddIngredient<GreaterManaScalingPotion>(potionsRequired - vanillaPotions)
        .AddIngredient(ItemID.GreaterManaPotion, vanillaPotions)
        .AddIngredient(ItemID.FallenStar)
        .AddIngredient(ItemID.CrystalShard, 3)
        .AddIngredient(ItemID.UnicornHorn)
        .AddTile(TileID.Bottles)
        .Register();
  }
}