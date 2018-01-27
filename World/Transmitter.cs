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

        public Circle Circle { get; private set; }
        public TransmitterState State { get; private set; }

        public Rectangle Rect {  get { return new Rectangle((int)(Circle.Position.X - Circle.Radius), (int)(Circle.Position.Y - Circle.Radius), (int)Circle.Radius * 2, (int)Circle.Radius * 2); } }

        public Transmitter(int pX, int pY)
        {
            Circle = new Circle(new Point(pX - DGS.TRANSMITTER_RADIUS / 2, pY - DGS.TRANSMITTER_RADIUS / 2), DGS.TRANSMITTER_RADIUS);
            State = TransmitterState.NORMAL;
            mTimeToWave = 0;
        }

        public void HackTransmitter()
        {
            State = TransmitterState.HACKED;
        }

        public void Update(float pSeconds)
        {
            if(State == TransmitterState.HACKED)
            {
                mTimeToWave -= pSeconds;
                if(mTimeToWave <= 0)
                {
                    mTimeToWave += DGS.TIME_BETWEEN_WAVES;
                    WaveManager.Instance().AddWave(Circle.Position);
                }
            }
        }
    }
}
