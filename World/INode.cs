using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission.World
{
    public interface INode
    {
        Texture2D Texture { get; }
        Rectangle Rect { get; }
        Color Colour { get; }
        void IntersectCheck(Wave pWave);
        bool MouseClick(Point pPosition);
        void Update(float pSeconds);
    }
}
