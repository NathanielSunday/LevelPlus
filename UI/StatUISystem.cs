using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace LevelPlus.UI;

// [Autoload(Side = ModSide.Client)]
public class StatUISystem : ModSystem
{
    private UserInterface currentInterface;
    private ExperienceBar experienceBar;
    private SpendPanel spendPanel;


    public void Toggle()
    {
        currentInterface.SetState(currentInterface.CurrentState.Equals(experienceBar)
            ? spendPanel
            : experienceBar);
    }

    public void UpdateConfigPositions()
    {
        experienceBar?.OnActivate();
        spendPanel?.OnActivate();
    }

    public override void Load()
    {
        experienceBar = new ExperienceBar();
        experienceBar.Activate();

        spendPanel = new SpendPanel();
        spendPanel.Activate();

        currentInterface = new UserInterface();
        currentInterface.SetState(experienceBar);
    }

    public override void UpdateUI(GameTime gameTime)
    {
        currentInterface?.Update(gameTime);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        var resourceBarsIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
        if (resourceBarsIndex == -1) return;
        layers.Insert(resourceBarsIndex, new LegacyGameInterfaceLayer(
            "Level+: Spend Interface",
            delegate
            {
                if (currentInterface?.CurrentState is null) return false;

                currentInterface.Draw(Main.spriteBatch, new GameTime());

                return true;
            },
            InterfaceScaleType.UI)
        );
    }
}