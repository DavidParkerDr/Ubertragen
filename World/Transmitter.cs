using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transmission;
using Transmission.Helpers;

namespace Transmission.World
{
    public class Transmitter : Node
    {
        public enum TransmitterState { NORMAL, HACKED }

        public TransmitterState State { get; private set; }

        private float mTimeToWave;

        public Transmitter(int pX, int pY, Color pColour):base(pColour, new Circle(new Point(pX - DGS.TRANSMITTER_RADIUS / 2, pY - DGS.TRANSMITTER_RADIUS / 2), DGS.TRANSMITTER_RADIUS), Transmission.Instance().CM().Load<Texture2D>("hackable"))
        {
            Reset();
        }

        public override void Reset()
        {
            State = TransmitterState.NORMAL;
            mTimeToWave = 0;
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

        public override void IntersectCheck(Wave pWave)
        {
            if(Circle.Intersects(pWave.Circle))
            {
                State = TransmitterState.HACKED;
            }
        }

        public override void Update(float pSeconds)
        {
            if(State == TransmitterState.HACKED)
            {
                mTimeToWave -= pSeconds;
                if(mTimeToWave <= 0)
                {
                    mTimeToWave += DGS.TIME_BETWEEN_WAVES + DGS.RNG.Next(0, 10) * 0.1f - 0.05f;
                    WaveManager.Instance().AddWave(Circle.Position, mColour);
                }
            }
        }

        public override bool IsWon()
        {
            return this.State == TransmitterState.HACKED;
        }
    }
}
