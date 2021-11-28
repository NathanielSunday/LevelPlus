using levelplus.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace levelplus {
    class levelplusModSystem : ModSystem {

		internal GUI gui;
		public static UserInterface guiInterface;

		internal LevelUI levelUI;
		public static UserInterface levelInterface;

		public override void Load() {
			base.Load();
			//makes sure UI isn't opened server side
			if (!Main.dedServ) {
				gui = new GUI();
				gui.Activate();
				guiInterface = new UserInterface();
				guiInterface.SetState(gui);

				levelUI = new LevelUI();
				levelUI.Activate();
				levelInterface = new UserInterface();
				levelInterface.SetState(levelUI);


			}
		}

		public override void Unload() {
			base.Unload();
			if (!Main.dedServ) {
				LevelUI.visible = false;
				GUI.visible = false;
				if (levelInterface != null && guiInterface != null) {
					levelInterface.SetState(null);
					guiInterface.SetState(null);
				}


				gui = null;
				levelUI = null;
			}
		}

		public override void UpdateUI(GameTime gameTime) {
			if (GUI.visible)
                guiInterface?.Update(gameTime);

			if (LevelUI.visible)
                levelInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			base.ModifyInterfaceLayers(layers);

			int resourceBarsIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			//layers.RemoveAt(resourceBarsIndex);
			layers.Insert(resourceBarsIndex, new LegacyGameInterfaceLayer("Level+: Resource Bars", delegate {
				if (GUI.visible)
					guiInterface.Draw(Main.spriteBatch, new GameTime());
				if (LevelUI.visible)
					levelInterface.Draw(Main.spriteBatch, new GameTime());

				return true;
			}, InterfaceScaleType.UI));
		}
	}
}
