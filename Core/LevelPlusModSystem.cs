// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.UI;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace LevelPlus.Core {
  class LevelPlusModSystem : ModSystem {

    internal GUI gui;
    public static UserInterface guiInterface;

    internal SpendUI statUI;
    public static UserInterface statInterface;

    public override void Load() {
      base.Load();
      //modify our mana cap
      IL.Terraria.Player.Update += PlayerManaUpdate;
      //makes sure UI isn't opened server side
      if (!Main.dedServ) {
        gui = new GUI();
        gui.Activate();
        guiInterface = new UserInterface();
        guiInterface.SetState(gui);

        statUI = new SpendUI();
        statUI.Activate();
        statInterface = new UserInterface();
        statInterface.SetState(statUI);
      }
    }

    public override void Unload() {
      base.Unload();
      if (!Main.dedServ) {
        if (statInterface != null && guiInterface != null) {
          statInterface.SetState(null);
          guiInterface.SetState(null);
        }

        gui = null;
        statUI = null;
        UITexture.textures.Clear();
      }
    }

    public override void UpdateUI(GameTime gameTime) {
      if (GUI.Visible)
        guiInterface?.Update(gameTime);

      if (SpendUI.Visible)
        statInterface?.Update(gameTime);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
      base.ModifyInterfaceLayers(layers);

      int resourceBarsIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
      layers.Insert(resourceBarsIndex, new LegacyGameInterfaceLayer("Level+: Resource Bars", delegate {
        if (GUI.Visible)
          guiInterface.Draw(Main.spriteBatch, new GameTime());
        if (SpendUI.Visible)
          statInterface.Draw(Main.spriteBatch, new GameTime());

        return true;
      }, InterfaceScaleType.UI));
    }

    /// <summary>
    /// Goes to the max mana check in Terraria's IL, and replaces the check with 200000 instead of 400
    /// </summary>
    private void PlayerManaUpdate(ILContext il) {
      ILCursor c = new(il);
      if (!c.TryGotoNext(MoveType.Before,
          i => i.MatchLdfld("Terraria.Player", "statManaMax2"),
          i => i.MatchLdcI4(400))
      ) {
        LevelPlus.Instance.Logger.FatalFormat("Could not find instruction");
        return;
      }

      c.Next.Next.Operand = 200000;
    }
  }
}
