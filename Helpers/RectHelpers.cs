using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission.Helpers
{
    public static class RectHelpers
    {
        public static bool IsInside(this Rectangle pRect, Point pPoint)
        {
            return pPoint.X > pRect.X && pPoint.X < pRect.X + pRect.Width
                && pPoint.Y > pRect.Y && pPoint.Y < pRect.Y + pRect.Height;
        }
    }
}
