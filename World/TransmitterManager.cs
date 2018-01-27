using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission.World
{
    public class TransmitterManager
    {
        private List<Transmitter> mTransmitters;
        private static TransmitterManager mInstance = null;
        private Texture2D mWhiteDisk;

        private TransmitterManager()
        {
            mTransmitters = new List<Transmitter>();
            mWhiteDisk = Transmission.Instance().CM().Load<Texture2D>("white_disk");
        }

        public static TransmitterManager Instance()
        {
            if (mInstance == null)
            {
                mInstance = new TransmitterManager();
            }
            return mInstance;
        }

        public void CheckWave(Wave pWave)
        {
            for(int i= 0; i < mTransmitters.Count; i++)
            {
                if(pWave.Circle.Intersects(mTransmitters[i].Circle))
                {
                    mTransmitters[i].HackTransmitter();
                }
            }
        }

        public void AddTransmitter(Transmitter pTransmitter)
        {
            mTransmitters.Add(pTransmitter);
        }

        public void Draw(SpriteBatch pSpriteBatch, float pSeconds)
        {
            Color color;
            for (int i = 0; i < mTransmitters.Count; i++)
            {
                color = mTransmitters[i].State == Transmitter.TransmitterState.NORMAL ? Color.Goldenrod : Color.OrangeRed;
                pSpriteBatch.Draw(mWhiteDisk, mTransmitters[i].Rect, color);
            }
        }

        public void Update(float pSeconds)
        {
            for (int i = 0; i < mTransmitters.Count; i++)
            {
                mTransmitters[i].Update(pSeconds);
            }
        }

        public void CheckMouseClick(Point pPosition)
        {
            for (int i = 0; i < mTransmitters.Count; i++)
            {
                if (mTransmitters[i].Circle.Intersects(pPosition))
                {
                    mTransmitters[i].HackTransmitter();
                }
            }
        }
    }
}
