// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace LevelPlus.Common.Systems;

public class ManaSystem : ModSystem
{
  public override void Load()
  {
    IL_Player.Update += delegate(ILContext il)
    {
      ILCursor c = new(il);
      if (!c.TryGotoNext(MoveType.Before,
        i => i.MatchLdfld("Terraria.Player", "statManaMax2"),
        i => i.MatchLdcI4(400))
        )
      {
        Mod.Logger.FatalFormat("Could not find instruction");
        return;
      }

      c.Next!.Next.Operand = 200000;
    };
  }
}

