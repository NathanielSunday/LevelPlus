using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace levelplus {
    public class levelplusConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static levelplusConfig Instance => ModContent.GetInstance<levelplusConfig>();

        [Label("Scaling")]
        [Tooltip("Turns on mob scaling")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool ScalingEnabled;


        [Label("Commands")]
        [Tooltip("Enables Commands")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool CommandsEnabled;
    }
}
