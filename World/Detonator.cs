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
    public class Detonator : Node
    {
        public Detonator(int pX, int pY, Color pColour) : base(pColour, new Circle(new Point(pX - DGS.TRANSMITTER_RADIUS / 2, pY - DGS.TRANSMITTER_RADIUS / 2), DGS.TRANSMITTER_RADIUS), Transmission.Instance().CM().Load<Texture2D>("unhackable"))
        {
            Reset();
        }

        public override void IntersectCheck(Wave pWave)
        {
            throw new NotImplementedException();
        }

        public override bool MouseClick(Point pPosition)
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        public override void Update(float pSeconds)
        {
            throw new NotImplementedException();
        }

        public override bool IsWon()
        {
            throw new NotImplementedException();
        }
    }
}
