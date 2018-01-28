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
    public class Unhackable : Node
    {
        public enum TransmitterState { NORMAL, HACKED }

        public TransmitterState State { get; private set; }

        private float mTimeToWave;

        public Unhackable(int pX, int pY, Color pColour):base(pColour, new Circle(new Point(pX - DGS.TRANSMITTER_RADIUS / 2, pY - DGS.TRANSMITTER_RADIUS / 2), DGS.TRANSMITTER_RADIUS), Transmission.Instance().CM().Load<Texture2D>("unhackable"))
        {
            Reset();
        }

        public void HackTransmitter()
        {
            State = TransmitterState.HACKED;
        }

        public void HackTransmitter(Color pColour)
        {
            if (pColour == mColour)
            {
                State = TransmitterState.HACKED;
            }
        }

        public override void Reset()
        {
            State = TransmitterState.NORMAL;
            mTimeToWave = 0;
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
                HackTransmitter(pWave.Colour);
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
            return State == TransmitterState.HACKED;
        }
    }
}
