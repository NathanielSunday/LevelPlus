using System.Reflection;
using LevelPlus.Configs;
using LevelPlus.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace LevelPlus.UI;

public class ExperienceBar : UIState
{
    private const float BorderThickness = 6f;
    private const float PanelWidth = 120f;
    private const float PanelHeight = 26f;
    private const float SquareThickness = 14f;

    private const float QuotientScalar =
        1f - (SquareThickness + 2 * BorderThickness) / PanelWidth - BorderThickness / PanelWidth;

    private BarBackground background;

    private LevelPlayer LevelPlayer => Main.LocalPlayer.GetModPlayer<LevelPlayer>();

    public override void OnInitialize()
    {
        base.OnInitialize();

        background = new BarBackground
        {
            Width = StyleDimension.FromPixels(PanelWidth),
            Height = StyleDimension.FromPixels(PanelHeight)
        };
        background.OnLeftClick += delegate { ModContent.GetInstance<StatUISystem>().Toggle(); };
        background.OnDraw += delegate
        {
            if (background.IsMouseHovering) UICommon.TooltipMouseText(LevelPlayer.Description.Value);
        };
        Append(background);

        var level = new UIText("0")
        {
            Width = StyleDimension.FromPixels(SquareThickness),
            Height = StyleDimension.FromPixels(SquareThickness),
            Left = StyleDimension.FromPixels(BorderThickness),
            Top = StyleDimension.FromPixels(BorderThickness),
            TextOriginX = 0.5f,
            TextOriginY = 0.5f
        };
        level.OnDraw += delegate { level.SetText(LevelPlayer.Level.ToString()); };
        background.Append(level);

        var bar = new UIImage(ModContent.GetInstance<LevelPlus>().Assets.Request<Texture2D>("Assets/Textures/UI/Bar"))
        {
            Width = StyleDimension.FromPercent(0f),
            Height = StyleDimension.FromPercent(1f),
            Left = StyleDimension.FromPixels(2 * BorderThickness + SquareThickness),
            ScaleToFit = true,
            Color = Color.LawnGreen // new Color(50, 205, 30)
        };
        bar.OnDraw += delegate
        {
            if (bar.IsMouseHovering) UICommon.TooltipMouseText(LevelPlayer.ExperienceTooltip.Value);

            // Current level progress experience / Experience needed to get to from current level to next level
            var quotient = QuotientScalar *
                           (LevelPlayer.Experience - LevelPlayer.LevelToExperience(LevelPlayer.Level)) /
                           (LevelPlayer.LevelToExperience(LevelPlayer.Level + 1) -
                            LevelPlayer.LevelToExperience(LevelPlayer.Level));

            bar.Width.Percent = quotient;
        };
        background.Append(bar);
    }

    public override void OnActivate()
    {
        base.OnActivate();

        var placement = UIConfig.Instance.ExperienceBar;

        background.Left.Set(placement.X, 0);
        background.Top.Set(placement.Y, 0);
    }
}

internal class BarBackground : DraggableUIElement
{
    private Asset<Texture2D> outline;
    private Asset<Texture2D> texture;

    public BarBackground()
    {
        var mod = ModContent.GetInstance<LevelPlus>();
        texture = mod.Assets.Request<Texture2D>("Assets/Textures/UI/Bar_Background");
        outline = mod.Assets.Request<Texture2D>("Assets/Textures/UI/Bar_Outline");

        OnDragEnd += UpdateConfig;
    }

    private void UpdateConfig(UIMouseEvent evt, UIElement element)
    {
        UIConfig.Instance.ExperienceBar = new Vector2(Left.Pixels, Top.Pixels);

        // Thank you SheepishShepherd, I did not know the configs didn't save in-code modification
        var saveMethodInfo = typeof(ConfigManager).GetMethod("Save", BindingFlags.Static | BindingFlags.NonPublic);
        saveMethodInfo?.Invoke(null, [UIConfig.Instance]);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        var inner = GetInnerDimensions().ToRectangle();

        spriteBatch.Draw(texture.Value, inner, Color.White);

        if (!UIConfig.Instance.PointNotifier ||
            Main.LocalPlayer.GetModPlayer<LevelPlayer>().Points <= 0 ||
            IsMouseHovering ||
            Dragging) return;

        spriteBatch.Draw(outline.Value, inner, Main.DiscoColor);
    }
}