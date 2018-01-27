using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transmission.Helpers;

namespace Transmission.World
{
    public class Wave
    {
        private Rectangle mRectangle;
        
        public Color Colour { get; private set; }

        public Rectangle Rect {  get { return mRectangle; } }

        private Circle mCircle;
        public float LifeLeft { get; private set; }
        public float Speed { get; private set; }
        public Circle Circle { get { return mCircle; } }

        public Wave(Point pPosition, Color pColour)
        {
            LifeLeft = DGS.WAVE_LIFETIME;
            Speed = DGS.WAVE_SPEED;
            mCircle = new Circle(pPosition, 0);
            Colour = pColour;
        }

        public void Update(float pSeconds)
        {
            mCircle.Radius += Speed * pSeconds;
            LifeLeft -= pSeconds;
            mRectangle.Width = (int)(mCircle.Radius * 2);
            mRectangle.Height = (int)(mCircle.Radius * 2);
            mRectangle.X = (int)(mCircle.Position.X - mCircle.Radius);
            mRectangle.Y = (int)(mCircle.Position.Y - mCircle.Radius);
        }
    }
}
