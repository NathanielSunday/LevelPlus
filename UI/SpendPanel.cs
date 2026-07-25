using System.Reflection;
using LevelPlus.Configs;
using LevelPlus.Players;
using LevelPlus.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace LevelPlus.UI;

public class SpendPanel : UIState
{
    private const float Padding = 2f;
    private const float BorderThickness = 6f;
    private const float HeaderHeight = 28f + 2 * BorderThickness;
    private const float PanelWidth = 270f;
    private const float PanelHeight = 300f;
    private const float StatWidth = 240f;
    private const float StatHeight = 40f;

    private SpendBackground background;

    public override void OnInitialize()
    {
        base.OnInitialize();

        background = new SpendBackground
        {
            Width = StyleDimension.FromPixels(PanelWidth),
            Height = StyleDimension.FromPixels(PanelHeight)
        };

        var stats = new UIList
        {
            Width = StyleDimension.FromPercent(StatWidth),
            Height = StyleDimension.FromPixelsAndPercent(-HeaderHeight - BorderThickness - 2 * Padding, 1f),
            Left = StyleDimension.FromPixels(BorderThickness + Padding),
            Top = StyleDimension.FromPixels(HeaderHeight + Padding),
            ListPadding = Padding,
            ManualSortMethod = e => { }
        };
        background.Append(stats);

        // Get all loaded stats and add them to the interface
        // AddRange wouldn't work properly for me
        ModContent.GetInstance<StatSystem>().Stats.ForEach(s =>
            stats.Add(new StatInterface(s)
            {
                Width = StyleDimension.FromPixels(StatWidth),
                Height = StyleDimension.FromPixels(StatHeight)
            }));

        Append(background);
    }

    public override void OnActivate()
    {
        base.OnActivate();

        var placement = UIConfig.Instance.SpendPanel;

        background.Left.Set(placement.X, 0);
        background.Top.Set(placement.Y, 0);
    }
}

internal class StatInterface(Stat stat) : UIElement
{
    private const float BorderThickness = 6f;
    private const float IconSize = 28f;
    private const float AddSquare = 28f;

    private Asset<Texture2D> background;

    private Stat StatPlayer => Main.LocalPlayer.GetModPlayer(stat);
    private LevelPlayer LevelPlayer => Main.LocalPlayer.GetModPlayer<LevelPlayer>();

    public override void OnInitialize()
    {
        base.OnInitialize();
        var mod = ModContent.GetInstance<LevelPlus>();

        background = mod.Assets.Request<Texture2D>("Assets/Textures/UI/Stat_Background");

        // Apply icon
        var icon = new UIImage(mod.Assets.Request<Texture2D>(stat.IconPath))
        {
            Width = StyleDimension.FromPixels(IconSize),
            Height = StyleDimension.FromPixels(IconSize),
            Left = StyleDimension.FromPixels(BorderThickness),
            Top = StyleDimension.FromPixels(BorderThickness)
        };
        icon.OnDraw += delegate
        {
            if (icon.IsMouseHovering) UICommon.TooltipMouseText(StatPlayer.Name + "\n" + StatPlayer.Description);
        };
        Append(icon);

        // Apply text value
        var valueText = new UIText("0")
        {
            Width = StyleDimension.FromPixelsAndPercent(-4 * BorderThickness - IconSize - AddSquare, 1f),
            Height = StyleDimension.FromPixelsAndPercent(-2 * BorderThickness, 1f),
            Left = StyleDimension.FromPixels(2 * BorderThickness + IconSize),
            Top = StyleDimension.FromPixels(BorderThickness),
            TextOriginX = 0.5f,
            TextOriginY = 0.5f
        };
        valueText.OnDraw += delegate { valueText.SetText(StatPlayer.Value.ToString()); };
        Append(valueText);

        // Apply plus button
        var addStat = new UIText("+")
        {
            Width = StyleDimension.FromPixels(AddSquare),
            Height = StyleDimension.FromPixels(AddSquare),
            Left = StyleDimension.FromPixelsAndPercent(-BorderThickness - AddSquare, 1f),
            Top = StyleDimension.FromPixels(BorderThickness),
            TextOriginX = 0.5f,
            TextOriginY = 0.5f
        };
        addStat.OnLeftClick += delegate
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            var spent = StatPlayer.ProjectedValue - StatPlayer.Value;
            StatPlayer.Value = StatPlayer.ProjectedValue;
            LevelPlayer.Points -= spent;
        };
        addStat.OnDraw += delegate
        {
            if (addStat.IsMouseHovering) UICommon.TooltipMouseText(StatPlayer.Name + "\n" + StatPlayer.SpendTooltip);
        };
        Append(addStat);
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
    private readonly Asset<Texture2D> texture;

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