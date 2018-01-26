using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transmission;

namespace Transmission.World
{
    public class Transmitter
    {
        public Rectangle Rect { get; private set; }

        public Transmitter(int pX, int pY)
        {
            Rect = new Rectangle(pX - DGS.TRANSMITTER_WIDTH / 2, pY - DGS.TRANSMITTER_HEIGHT / 2, DGS.TRANSMITTER_WIDTH, DGS.TRANSMITTER_HEIGHT);
        }


    }
}
