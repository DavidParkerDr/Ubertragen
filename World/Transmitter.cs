using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transmission;
using Transmission.Helpers;

namespace Transmission.World
{
    public class Transmitter
    {
        public enum TransmitterState { NORMAL, HACKED }

        private float mTimeToWave;

        private Color mColour;

        public Color Colour { get { return mColour; } }

        public Circle Circle { get; private set; }
        public TransmitterState State { get; private set; }

        public Rectangle Rect {  get { return new Rectangle((int)(Circle.Position.X - Circle.Radius), (int)(Circle.Position.Y - Circle.Radius), (int)Circle.Radius * 2, (int)Circle.Radius * 2); } }

        public Transmitter(int pX, int pY, Color pColour)
        {
            Circle = new Circle(new Point(pX - DGS.TRANSMITTER_RADIUS / 2, pY - DGS.TRANSMITTER_RADIUS / 2), DGS.TRANSMITTER_RADIUS);
            State = TransmitterState.NORMAL;
            mTimeToWave = 0;
            mColour = pColour;
        }

        public void HackTransmitter(Color pColour)
        {
            State = TransmitterState.HACKED;
            mColour.R = (byte)(Math.Min(mColour.R + pColour.R, 255));
            mColour.G = (byte)(Math.Min(mColour.G + pColour.G, 255));
            mColour.B = (byte)(Math.Min(mColour.B + pColour.B, 255));
        }

        public void Update(float pSeconds)
        {
            if(State == TransmitterState.HACKED)
            {
                mTimeToWave -= pSeconds;
                if(mTimeToWave <= 0)
                {
                    mTimeToWave += DGS.TIME_BETWEEN_WAVES;
                    WaveManager.Instance().AddWave(Circle.Position, mColour);
                }
            }
        }
    }
}
