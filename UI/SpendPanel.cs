using System;
using System.Linq;
using System.Reflection;
using LevelPlus.Configs;
using LevelPlus.Players;
using LevelPlus.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.UI;

namespace LevelPlus.UI;

public class SpendPanel : UIState
{
    private SpendBackground background;
    private UIList stats;

    public override void OnInitialize()
    {
        base.OnInitialize();

        background = new SpendBackground()
        {
            Width = StyleDimension.FromPixels(160f),
            Height = StyleDimension.FromPixels(200f)
        };

        stats = new UIList()
        {
            Width =  StyleDimension.FromPercent(1f),
            Height = StyleDimension.FromPercent(1f)
        };
        
        // Get all loaded stats and add them to the interface
        ModContent.GetInstance<LevelPlus>().Logger.Debug("Adding stats to UI: ");
        // TODO understand why it's just stacking them on top of each other
        stats.AddRange(
            ModContent.GetInstance<StatSystem>().Stats
            .Select(s => new StatInterface(s)
                {
                    Width = StyleDimension.FromPercent(1f),
                    Height = StyleDimension.FromPixels(20f)
                }
            ));
        stats.Recalculate();

        Append(background);
        background.Append(stats);
    }

    public override void OnActivate()
    {
        base.OnActivate();

        var placement = UIConfig.Instance.SpendPanel;

        background.Left.Set(placement.X, 0);
        background.Top.Set(placement.Y, 0);
    }
}

internal class StatInterface : UIElement
{
    private Asset<Texture2D> background;
    private UIImage icon;
    private UIText valueText;

    private Stat stat;

    public StatInterface(Stat stat)
    {
        var mod = ModContent.GetInstance<LevelPlus>();
        this.stat = stat;
        mod.Logger.Debug(stat.Id);

        // Request our assets
        background = mod.Assets.Request<Texture2D>("Assets/Textures/UI/Stat_Background");
        // icon = new UIImage(mod.Assets.Request<Texture2D>(stat.IconPath));

        // TODO Apply text values
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        // TODO implement hover features
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        var inner = GetInnerDimensions().ToRectangle();

        spriteBatch.Draw(background.Value, inner, Color.White);
    }
}

internal class SpendBackground : DraggableUIElement
{
    // Create a background for the full spend UI
    private Asset<Texture2D> texture;

    public SpendBackground()
    {
        var mod = ModContent.GetInstance<LevelPlus>();
        texture = mod.Assets.Request<Texture2D>("Assets/Textures/UI/Panel_Background");

        OnDragEnd += UpdateConfig;
    }

    private void UpdateConfig(UIMouseEvent evt, UIElement element)
    {
        UIConfig.Instance.SpendPanel = new Vector2(Left.Pixels, Top.Pixels);

        // Thank you SheepishShepherd, I did not know the configs didn't save in-code modification
        var saveMethodInfo = typeof(ConfigManager).GetMethod("Save", BindingFlags.Static | BindingFlags.NonPublic);
        saveMethodInfo?.Invoke(null, [UIConfig.Instance]);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        var inner = GetInnerDimensions().ToRectangle();

        spriteBatch.Draw(texture.Value, inner, Color.White);
    }
}