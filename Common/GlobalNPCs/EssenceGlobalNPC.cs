// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Content.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace LevelPlus.Common.GlobalNPCs;

// ReSharper disable once InconsistentNaming
public class EssenceGlobalNPC : GlobalNPC
{
  public override void ModifyGlobalLoot(GlobalLoot globalLoot)
  {
    globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<Essence>(), 100, 1, 5));
  }
}