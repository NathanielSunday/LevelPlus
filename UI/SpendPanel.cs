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
    private const float SquareThickness = 28f;
    private const float PointWidth = 44f;
    private const float BarWidth = 140f;
    private const float PanelWidth = 270f;
    private const float PanelHeight = 300f;
    private const float StatWidth = 224f;
    private const float StatHeight = 40f;

    private const float QuotientScalar = 1f - BorderThickness * 5 / PanelWidth - 2 * SquareThickness / PanelWidth -
                                         PointWidth / PanelWidth;

    private SpendBackground background;

    public override void OnInitialize()
    {
        base.OnInitialize();

        background = new SpendBackground
        {
            Width = StyleDimension.FromPixels(PanelWidth),
            Height = StyleDimension.FromPixels(PanelHeight)
        };

        var level = new UIText("0")
        {
            Width = StyleDimension.FromPixels(SquareThickness),
            Height = StyleDimension.FromPixels(SquareThickness),
            Left = StyleDimension.FromPixels(BorderThickness),
            Top = StyleDimension.FromPixels(BorderThickness),
            TextOriginX = 0.5f,
            TextOriginY = 0.5f
        };
        level.OnDraw += delegate
        {
            var player = Main.LocalPlayer.GetModPlayer<LevelPlayer>();
            level.SetText(player.Level.ToString());
            if (!level.IsMouseHovering) return;
            UICommon.TooltipMouseText(player.Description.Value);
        };
        background.Append(level);

        var points = new UIText("0")
        {
            Width = StyleDimension.FromPixels(PointWidth),
            Height = StyleDimension.FromPixels(SquareThickness),
            Left = StyleDimension.FromPixelsAndPercent(-2 * BorderThickness - SquareThickness - PointWidth, 1f),
            Top = StyleDimension.FromPixels(BorderThickness),
            TextOriginX = 0.5f,
            TextOriginY = 0.5f
        };
        points.OnDraw += delegate { points.SetText(Main.LocalPlayer.GetModPlayer<LevelPlayer>().Points.ToString()); };
        background.Append(points);

        var close = new UIText("X")
        {
            Width = StyleDimension.FromPixels(SquareThickness),
            Height = StyleDimension.FromPixels(SquareThickness),
            Left = StyleDimension.FromPixelsAndPercent(-BorderThickness - SquareThickness, 1f),
            Top = StyleDimension.FromPixels(BorderThickness),
            TextOriginX = 0.5f,
            TextOriginY = 0.5f
        };
        close.OnLeftClick += delegate { ModContent.GetInstance<StatUISystem>().Toggle(); };
        background.Append(close);

        var bar = new UIImage(ModContent.GetInstance<LevelPlus>().Assets
            .Request<Texture2D>("Assets/Textures/UI/Panel_Bar"))
        {
            Width = StyleDimension.FromPercent(0f),
            Height = StyleDimension.FromPixels(SquareThickness + 2 * BorderThickness),
            Left = StyleDimension.FromPixels(2 * BorderThickness + SquareThickness),
            ScaleToFit = true,
            Color = Color.LawnGreen // new Color(50, 205, 30)
        };
        bar.OnDraw += delegate
        {
            // Current level progress experience / Experience needed to get to from current level to next level
            var player = Main.LocalPlayer.GetModPlayer<LevelPlayer>();
            var quotient = QuotientScalar *
                           (player.Experience - LevelPlayer.LevelToExperience(player.Level)) /
                           (LevelPlayer.LevelToExperience(player.Level + 1) -
                            LevelPlayer.LevelToExperience(player.Level));

            bar.Width.Percent = quotient;
            if (bar.IsMouseHovering) UICommon.TooltipMouseText(player.ExperienceTooltip.Value);
        };
        background.Append(bar);

        var scrollbar = new StatScrollbar
        {
            Width = StyleDimension.FromPixels(20f),
            Height = StyleDimension.FromPixelsAndPercent(-3 * BorderThickness - SquareThickness - 2 * Padding, 1f),
            Left = StyleDimension.FromPixelsAndPercent(-20f - BorderThickness - Padding, 1f),
            Top = StyleDimension.FromPixels(2 * BorderThickness + SquareThickness + Padding),
        };
        background.Append(scrollbar);

        var stats = new UIList
        {
            Width = StyleDimension.FromPercent(StatWidth),
            Height = StyleDimension.FromPixelsAndPercent(-SquareThickness - 3 * BorderThickness, 1f),
            Left = StyleDimension.FromPixels(BorderThickness),
            Top = StyleDimension.FromPixels(SquareThickness + 2 * BorderThickness),
            ListPadding = Padding,
            PaddingLeft = Padding,
            PaddingRight = Padding,
            PaddingTop = Padding,
            PaddingBottom = Padding,
            ManualSortMethod = e => { }
        };
        stats.SetScrollbar(scrollbar);
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

internal class StatScrollbar : UIScrollbar
{
    private Asset<Texture2D> peg;

    public override void OnInitialize()
    {
        peg = ModContent.GetInstance<LevelPlus>().Assets.Request<Texture2D>("Assets/Textures/UI/Scrollbar_Peg");
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        var dimensions = GetDimensions().ToRectangle();
        spriteBatch.Draw(peg.Value, new Rectangle(
            dimensions.X, 
            dimensions.Y + (int)((dimensions.Height - 20) * (ViewPosition / (MaxViewSize - dimensions.Height))), 
            dimensions.Width, 
            20), Color.White);
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