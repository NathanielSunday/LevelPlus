// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using LevelPlus.Config;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace LevelPlus.UI
{
    internal class GUI : UIState {
        public static bool Visible { get; private set; }
        private XPBar XPBar;
        private Vector2 placement;

        public override void OnInitialize() {
            base.OnInitialize();
            placement = new Vector2(ClientConfig.Instance.XPBarLeft, ClientConfig.Instance.XPBarTop);

            XPBar = new XPBar(120, 26);

            XPBar.Left.Set(placement.X, 0f);
            XPBar.Top.Set(placement.Y, 0f);
            XPBar.SetSnapPoint("Origin", 0, placement);

            base.Append(XPBar);
        }

        public override void OnDeactivate() {
            base.OnDeactivate();
            XPBar = null;
        }
    }
}

