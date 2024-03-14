// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace LevelPlus.Common.UI;

public class UITexture : UIElement
{
  private Texture2D texture;
  private readonly bool mouseInterface;

  public Color Color { get; set; } = Color.White;

  private static readonly Dictionary<string, Texture2D> textures = new();

  public UITexture(string path, bool mouseInterface = false)
  {
    if (!textures.TryGetValue(path, out texture))
    {
      textures.Add(path, ModContent.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad).Value);
      texture = textures[path];
    }

    this.mouseInterface = mouseInterface;
  }

  public override void OnDeactivate()
  {
    base.OnDeactivate();
    texture = null;
  }

  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);

    if (mouseInterface && ContainsPoint(Main.MouseScreen))
    {
      Main.LocalPlayer.mouseInterface = true;
    }
  }

  protected override void DrawSelf(SpriteBatch spriteBatch)
  {
    spriteBatch.Draw(texture, GetDimensions().ToRectangle(), Color);
  }
}

