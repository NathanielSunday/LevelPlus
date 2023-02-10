using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus {
    public class LevelPlusConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static LevelPlusConfig Instance => ModContent.GetInstance<LevelPlusConfig>();

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
