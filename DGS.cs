using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission
{
    static class DGS
    {
        public const float SECONDS_TO_DISPLAY_FLASH_SCREEN = 1.5f;
        public const float STARTING_CHARGE = 10; // ten percent charge on every port to begin with
        public static float CHARGE_AMOUNT = 5f;
        public static float TRACK_DEPLETION_RATE = 20f;
        public static float INITIAL_CHARGE = 50f;
        public static float BULLET_RADIUS_POWER_SCALE = 5f;
        public static float TANK_SPEED = 2f;
        public static float BASE_TANK_ROTATION_ANGLE = 0.01f;
        public static float BASE_TURRET_ROTATION_ANGLE = 0.01f;
        public static float BULLET_SPEED_SCALER = 500f;
        public static float MAX_CHARGE = 200f;
        public static float BULLET_CHARGE_DEPLETION = 50f;
        public static int TANK_RADIUS = 25;
        public static int BLAST_DELAY = 15;
        public static float BULLET_SPEED = 50;
        public static bool HAVE_CONTROLLER = true;
    }
}
