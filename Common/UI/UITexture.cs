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

internal class UITexture : UIElement
{
  public static Dictionary<string, Texture2D> textures = new();
  private Texture2D _texture;
  public Color backgroundColor = Color.White;
  private readonly bool mouseInterface;

  public UITexture(string path, bool mouseInterface = false)
  {
    if (!textures.TryGetValue(path, out _texture))
    {
      textures.Add(path, ModContent.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad).Value);
      _texture = textures[path];
    }

    this.mouseInterface = mouseInterface;
  }

  public override void OnDeactivate()
  {
    base.OnDeactivate();
    _texture = null;
  }

  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);

    if (mouseInterface && ContainsPoint(new Vector2(Main.mouseX, Main.mouseY))) Main.LocalPlayer.mouseInterface = true;
  }

  protected override void DrawSelf(SpriteBatch spriteBatch)
  {
    base.DrawSelf(spriteBatch);
    spriteBatch.Draw(_texture, GetDimensions().ToRectangle(), backgroundColor);
  }
}