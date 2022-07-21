

namespace levelplus {
    static class Utility {

        //Move the Stat Enum & Packet Enum here

        internal enum Stat : byte {
            CONSTITUTION,   // 0000 0000
            STRENGTH,       // 0000 0001
            INTELLIGENCE,   // 0000 0010
            CHARISMA,       // 0000 0011
            DEXTERITY,      // 0000 0100
            MOBILITY,       // 0000 0101
            EXCAVATION,     // 0000 0110
            ANIMALIA,       // 0000 0111
            LUCK,           // 0000 1000
            MYSTICISM       // 0000 1001
        }

        internal enum PacketType : byte {
            XP,             // 0000 0000
            PlayerSync,     // 0000 0001
            StatsChanged    // 0000 0010
        }

        internal enum Weapon {
            SWORD,//
            YOYO,//
            SUMMON,//
            SPEAR,//
            BOOMERANG,//
            MAGIC,//
            BOW,//
            GUN,
            THROWN
        }

        // Mob //
        public static float MobScalar = 0.025f;

        // Level //
        public const int HealthPerLevel = 1;
        public const int ManaPerLevel = 0;
        public const int PointsPerLevel = 3;
        public const int StartingPoints = PointsPerLevel;

        // Constitution //
        public const int HealthPerPoint = 5;
        public const int DefensePerPoint = 2;
        public const int HRegenPerPoint = 1; //dim

        // Intelligence //
        public const float MagicDamagePerPoint = .01f;
        public const int MagicCritPerPoint = 5; //dim

        // Strength //
        public const float MeleeDamagePerPoint = .01f;
        public const int MeleeCritPerPoint = 5; //dim

        // Dexterity //
        public const float RangedDamagePerPoint = .01f;
        public const int RangedCritPerPoint = 5; //dim

        // Charisma //
        public const float SummonDamagePerPoint = .01f;
        public const int SummonCritPerPoint = 5; //dim

        // Animalia //
        public const float FishSkillPerPoint = .02f;
        //this is obsolete currently
        //public static float MinionKnockBack = .02f; 
        public const int MinionPerPoint = 1; //dim

        // Excavation //
        public const float PickSpeedPerPoint = .01f;
        public const float BuildSpeedPerPoint = .02f;
        public const int RangePerPoint = 1;

        // Mobility //
        public const float RunSpeedPerPoint = .01f;
        public const float AccelPerPoint = .02f;
        public const float WingPerPoint = .02f;

        // Luck //
        public const float XPPerPoint = .01f;
        public const float AmmoPerPoint = .05f;

        // Mysticism //
        public const int ManaPerPoint = 2;
        public const int ManaRegPerPoint = 1; //dim
        public const float ManaCostPerPoint = .01f; //dim


        public static ulong CalculateMobXp(int mobLife, int mobDamage, int mobDefence) {
            return (ulong)(mobDamage / 2 + mobLife / 3 + mobDefence);
        }
        public static ulong CalculateNeededXp(ulong currentXp) {
            return CalculateNeededXp(XpToLevel(currentXp));
        }
        public static ulong CalculateNeededXp(int level) {
            return LevelToXp(level + 1);
        }
        public static int XpToLevel(ulong xp) {
            return (int)System.MathF.Pow(xp / 100, 5 / 11);
        }
        public static ulong LevelToXp(int level) {
            return (ulong)(100 * System.MathF.Pow(level, 11 / 5));
        }
        //read the player data, taking in the Player.whoAmI as a parameter
        public static void ParsePlayer(System.IO.BinaryReader reader, byte whoAmI) {
            levelplusModPlayer copy = Terraria.Main.player[whoAmI].GetModPlayer<levelplusModPlayer>();
            copy.XP = reader.ReadUInt64();
            foreach (int i in System.Enum.GetValues(typeof(Stat))) {
                copy.Stats[i] = (uint)reader.ReadInt32();
            }
        }
        //copies a players stats and defines what player they are in a packet
        public static void AddSyncToPacket(Terraria.ModLoader.ModPacket packet, levelplusModPlayer player) { 
            packet.Write((byte)player.Player.whoAmI);
            packet.Write(player.XP);
            foreach (int i in System.Enum.GetValues(typeof(Stat))) {
                packet.Write(player.Stats[i]);
            }
        }
        //returns true if stats between two players match
        public static bool PlayerStatsMatch(levelplusModPlayer player, levelplusModPlayer compare) { 
            if (compare.XP != player.XP)
                return false;
            foreach (int i in System.Enum.GetValues(typeof(Stat))) {
                if (compare.Stats[i] != player.Stats[i]) return false;
            }
            return true;
        }
    }
}
