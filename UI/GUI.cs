using Microsoft.Xna.Framework;
using Terraria.UI;

namespace levelplus.UI
{
	internal class GUI : UIState
	{
		public static bool visible;

        private ResourceBar xp;
        private XPBarButton button;
        private float width = 80;
        private float height = 24;
        private Vector2 placement;

        public override void OnInitialize()
		{
			base.OnInitialize();
            //465, 35 
            //placement = Main.GetScreenOverdrawOffset().ToVector2();
            placement = new Vector2(465, 35);

            xp = new ResourceBar(ResourceBarMode.XP, width);
            button = new XPBarButton(height);

            xp.Left.Set(placement.X + (height * 186 / 186), 0f);
            xp.Top.Set(placement.Y, 0f);

            button.Left.Set(placement.X, 0f);
            button.Top.Set(placement.Y, 0f);



            base.Append(xp);
            base.Append(button);
            visible = true;
		}
	}
}

