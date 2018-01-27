using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission.Helpers
{
    public static class StringHelpers
    {
        public static Color ToColour(this string pString)
        {
            switch (pString.ToLower().Trim())
            {
                case "red":
                    return Color.Red;
                case "green":
                    return Color.Green;
                case "blue":
                    return Color.Blue;
                default:
                    throw new Exception("Unrecognised colour string " + pString);
            }
        }
    }
}
