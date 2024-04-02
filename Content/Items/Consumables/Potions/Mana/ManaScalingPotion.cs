// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using Terraria.ID;

namespace LevelPlus.Content.Items.Consumables.Potions.Mana;

public class ManaScalingPotion : BaseScalingPotion
{
  public override string Texture => "LevelPlus/Assets/Textures/Items/Consumables/ManaScalingPotion";
  protected override short BasePotionId => ItemID.ManaPotion;
  protected override float HealMana => 0.25f;

  public override void AddRecipes()
  {
    CreateRecipe()
      .AddIngredient<Essence>()
      .AddIngredient(ItemID.ManaPotion)
      .AddTile(TileID.Bottles)
      .Register();
    
    var potionsMade = 1;
    var potionsRequired = 2;
    for (var vanillaPotions = 0; vanillaPotions <= potionsRequired; ++vanillaPotions)
      CreateRecipe(potionsMade)
        .AddIngredient<Essence>(potionsMade + vanillaPotions)
        .AddIngredient<LesserManaScalingPotion>(potionsRequired - vanillaPotions)
        .AddIngredient(ItemID.LesserManaPotion, vanillaPotions)
        .AddIngredient(ItemID.GlowingMushroom)
        .AddTile(TileID.Bottles)
        .Register();
  }
}