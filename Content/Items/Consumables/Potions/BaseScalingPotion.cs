// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LevelPlus.Content.Items.Consumables.Potions;

public abstract class BaseScalingPotion : ModItem
{
  protected abstract short BasePotionId { get; }
  
  protected virtual float HealLife => 0f;
  protected virtual float HealMana => 0f;
  
  public override void SetDefaults()
  {
    Item.width = 40;
    Item.height = 40;
    Item.useTime = 17;
    Item.useAnimation = 17;
    Item.useStyle = ItemUseStyleID.DrinkLiquid;
    Item.UseSound = SoundID.Item3;
    Item.useTurn = true;
    Item.maxStack = 30;
    Item.consumable = true;
    Item.potion = true;
    Item.healLife = (int)(HealLife * 100);
    Item.healMana = (int)(HealMana * 100);
    
    var basePrice = new Item(BasePotionId).value;
    Item.value = basePrice + Essence.Price;
  }

  public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
  {
    healValue = (int)(HealLife * player.statLifeMax2);
  }

  public override void GetHealMana(Player player, bool quickHeal, ref int healValue)
  {
    healValue = (int)(HealMana * player.statManaMax2);
  }
}