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
                    return DGS.RED;
                case "cyan":
                    return DGS.CYAN;
                case "gold":
                    return DGS.GOLD;
                case "fuchsia":
                    return DGS.FUCHSIA;
                default:
                    throw new Exception("Unrecognised colour string " + pString);
            }
        }
    }
}
