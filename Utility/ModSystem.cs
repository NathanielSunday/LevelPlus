using levelplus.UI;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System.Collections.Generic;
using System.Reflection.Emit;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace levelplus {
    class levelplusModSystem : ModSystem {

        internal GUI gui;
        public static UserInterface guiInterface;

        internal SpendUI levelUI;
        public static UserInterface levelInterface;

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

                levelUI = new SpendUI();
                levelUI.Activate();
                levelInterface = new UserInterface();
                levelInterface.SetState(levelUI);
            }
        }

        public override void Unload() {
            base.Unload();
            if (!Main.dedServ) {
                SpendUI.visible = false;
                GUI.visible = false;
                if (levelInterface != null && guiInterface != null) {
                    levelInterface.SetState(null);
                    guiInterface.SetState(null);
                }

                gui = null;
                levelUI = null;
                UITexture.textures.Clear();
            }
        }

        public override void UpdateUI(GameTime gameTime) {
            if (GUI.visible)
                guiInterface?.Update(gameTime);

            if (SpendUI.visible)
                levelInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            base.ModifyInterfaceLayers(layers);

            int resourceBarsIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            //layers.RemoveAt(resourceBarsIndex);
            layers.Insert(resourceBarsIndex, new LegacyGameInterfaceLayer("Level+: Resource Bars", delegate {
                if (GUI.visible)
                    guiInterface.Draw(Main.spriteBatch, new GameTime());
                if (SpendUI.visible)
                    levelInterface.Draw(Main.spriteBatch, new GameTime());

                return true;
            }, InterfaceScaleType.UI));
        }

        //goes to the max mana check in Terraria's IL, and replaces the check with 200000 instead of 400
        private void PlayerManaUpdate(ILContext il) {
            ILCursor c = new(il);
            if (!c.TryGotoNext(MoveType.Before,
                i => i.MatchLdfld("Terraria.Player", "statManaMax2"),
                i => i.MatchLdcI4(400))
            ) {
                levelplus.Instance.Logger.FatalFormat("Could not find instruction");
                return;
            }

            c.Next.Next.Operand = 200000;
        }
    }
}
