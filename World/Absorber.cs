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
    public class Absorber : Node
    {
        public enum TransmitterState { NORMAL, HACKED }

        public TransmitterState State { get; private set; }

        private float mTimeToWave;

        public Absorber(int pX, int pY):base(Color.DarkOrange, new Circle(new Point(pX - DGS.TRANSMITTER_RADIUS / 2, pY - DGS.TRANSMITTER_RADIUS / 2), DGS.TRANSMITTER_RADIUS), Transmission.Instance().CM().Load<Texture2D>("absorber"))
        {
            Reset();
        }

        public override void Reset()
        {
            State = TransmitterState.NORMAL;
            mTimeToWave = 0;
        }

        public void HackTransmitter()
        {
            State = TransmitterState.HACKED;
        }

        public void HackTransmitter(Color pColour)
        {
            State = TransmitterState.HACKED;
            mColour.R = (byte)(Math.Min(mColour.R + pColour.R, 255));
            mColour.G = (byte)(Math.Min(mColour.G + pColour.G, 255));
            mColour.B = (byte)(Math.Min(mColour.B + pColour.B, 255));
        }

        public override bool MouseClick(Point pPosition)
        {
            // TODO Play Donk Sound
            return false;
        }

        public override void IntersectCheck(Wave pWave)
        {
            if (Circle.Intersects(pWave.Circle))
            {
                pWave.AbsorbWave();
            }
        }

        public override void Update(float pSeconds)
        {
            if (State == TransmitterState.HACKED)
            {
                mTimeToWave -= pSeconds;
                if (mTimeToWave <= 0)
                {
                    mTimeToWave += DGS.TIME_BETWEEN_WAVES;
                    WaveManager.Instance().AddWave(Circle.Position, mColour);
                }
            }
        }

        public override bool IsWon()
        {
            return true;
        }
    }
}
