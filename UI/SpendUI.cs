using Terraria.UI;
using Terraria;

namespace levelplus.UI {
    class SpendUI : UIState {
        public static bool visible;

        private StatCircle circle;

        public override void OnInitialize() {
            base.OnInitialize();

            visible = false;

            float circleDiameter = 300f;
            circle = new StatCircle(circleDiameter);
            circle.Left.Set((Main.screenWidth / 2f) - (circleDiameter / 2f), 0f);
            circle.Top.Set((Main.screenHeight / 2f) - (circleDiameter / 2f), 0f);
            
            Append(circle);
        }

        public override void OnDeactivate() {
            base.OnDeactivate();
            circle = null;
        }
    }
}
