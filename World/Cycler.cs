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
    public class Cycler : Node
    {
        private Color[] mColours;
        private int mColourIndex;
        private float mTimeTillNextColour;

        public enum TransmitterState { NORMAL, HACKED }

        public TransmitterState State { get; private set; }

        private float mTimeToWave;

        public Cycler(int pX, int pY, Color[] pColours) : base(pColours[0], new Circle(new Point(pX - DGS.TRANSMITTER_RADIUS / 2, pY - DGS.TRANSMITTER_RADIUS / 2), DGS.TRANSMITTER_RADIUS), Transmission.Instance().CM().Load<Texture2D>("hackable"))
        {
            mColours = pColours;
            Reset();
        }

        public override void IntersectCheck(Wave pWave)
        {
            if (Circle.Intersects(pWave.Circle))
            {
                State = TransmitterState.HACKED;
            }
        }

        public override void Update(float pSeconds)
        {
            if (State == TransmitterState.HACKED)
            {
                mTimeToWave -= pSeconds;
                if (mTimeToWave <= 0)
                {
                    mTimeToWave += DGS.TIME_BETWEEN_WAVES + DGS.RNG.Next(0, 10) * 0.1f - 0.05f;
                    WaveManager.Instance().AddWave(Circle.Position, mColour);
                }
            }
            else
            {
                mTimeTillNextColour -= pSeconds;
                if(mTimeTillNextColour <= 0)
                {
                    mTimeTillNextColour += DGS.CYCLE_TIME;
                    mColourIndex++;
                    if(mColourIndex >= mColours.Length)
                    {
                        mColourIndex = 0;
                    }
                    mColour = mColours[mColourIndex];
                }
            }
        }

        public override bool IsWon()
        {
            return this.State == TransmitterState.HACKED;
        }

        public override bool MouseClick(Point pPosition)
        {
            if (State != TransmitterState.HACKED)
            {
                if (Circle.Intersects(pPosition))
                {
                    State = TransmitterState.HACKED;
                    return true;
                }
            }
            return false;
        }

        public override void Reset()
        {
            mColour = mColours[0];
            mColourIndex = 0;
            mTimeTillNextColour = DGS.CYCLE_TIME;
        }
    }
}
