using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission.Helpers
{
    public struct Circle
    {
        public float Radius { get; set; }
        public Point Position { get; set; }

        public Circle(Point pPosition, float pRadius)
        {
            Position = pPosition;
            Radius = pRadius;
        }

        public bool Intersects(Point pPoint)
        {
            return (Math.Pow(Position.X - pPoint.X, 2) + Math.Pow(Position.Y - pPoint.Y, 2) < Radius * Radius) ;
        }

        public bool Intersects(Circle pCircle)
        {
            pCircle.Radius += Radius;
            return pCircle.Intersects(Position);
        }
    }
}
