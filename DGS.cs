using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission
{
    static class DGS
    {
        public static Random RNG = new Random();

        public const float SECONDS_TO_DISPLAY_FLASH_SCREEN = 1.5f;
        public const int MOUSE_WIDTH = 5;
        public const int MOUSE_HEIGHT = 5;

        public const int TRANSMITTER_RADIUS =30;

        public const float WAVE_LIFETIME = 1.0f;
        public const float WAVE_SPEED = 100.0f;
        public const float TIME_BETWEEN_WAVES = 0.5f;
        public static Color BLACK = new Color(26, 26, 26, 255);
        public static Color CYAN = new Color(0, 255, 255, 255);
        public static Color RED = new Color(250, 50, 60, 255);
        public static Color GOLD = new Color(255, 255, 60, 255);
        public static Color FUCHSIA = new Color(230, 40, 255, 255);
        public const float CYCLE_TIME = 1f;

    }
}
