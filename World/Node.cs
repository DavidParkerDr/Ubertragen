using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transmission.Helpers;

namespace Transmission.World
{
    public abstract class Node
    {
        protected Color mColour;
        protected Texture2D mTexture;

        public Color Colour { get { return mColour; } }

        protected Circle Circle { get; private set; }

        public Rectangle Rect { get { return new Rectangle((int)(Circle.Position.X - Circle.Radius), (int)(Circle.Position.Y - Circle.Radius), (int)Circle.Radius * 2, (int)Circle.Radius * 2); } }

        public Texture2D Texture { get { return mTexture; } }

        protected Node(Color pColour, Circle pCircle, Texture2D pTexture)
        {
            mColour = pColour;
            mTexture = pTexture;
            Circle = pCircle;
        }

        public abstract void IntersectCheck(Wave pWave);
        public abstract bool MouseClick(Point pPosition);
        public abstract void Update(float pSeconds);

        public abstract void Reset();
        public abstract bool IsWon();
    }
}
