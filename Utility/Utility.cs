using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace levelplus.Utility {
    static class Utility {

        // Level //
        public static int HealthPerLevel = 1;
        public static int ManaPerLevel = 0;
        public static int PointsPerLevel = 3;
        public static int StartingPoints = PointsPerLevel;

        // Constitution //
        public static int HealthPerPoint = 5;
        public static int DefensePerPoint = 2;
        public static int HRegenPerPoint = 1; //dim
        
        // Intelligence //
        public static float MagicDamagePerPoint = .01f;
        public static int MagicCritPerPoint = 5; //dim

        // Strength //
        public static float MeleeDamagePerPoint = .01f;
        public static int MeleeCritPerPoint = 5; //dim

        // Dexterity //
        public static float RangedDamagePerPoint = .01f;
        public static int RangedCritPerPoint = 5; //dim

        // Charisma //
        public static float SummonDamagePerPoint = .01f;
        public static int SummonCritPerPoint = 5; //dim

        // Animalia //
        public static float FishSkillPerPoint = .02f;
        //this is obsolete currently
        //public static float MinionKnockBack = .02f; 
        public static int MinionPerPoint = 1; //dim

        // Excavation //
        public static float PickSpeedPerPoint = .01f;
        public static float BuildSpeedPerPoint = .02f;
        public static int RangePerPoint = 1;

        // Mobility //
        public static float RunSpeedPerPoint = .01f;
        public static float AccelPerPoint = .02f;
        public static float WingPerPoint = .02f;

        // Luck //
        public static float XPPerPoint = .01f;
        public static float AmmoPerPoint = .05f;

        // Mysticism //
        public static int ManaPerPoint = 2;
        public static int ManaRegPerPoint = 1; //dim
        public static float ManaCostPerPoint = .01f; //dim
    }
}
