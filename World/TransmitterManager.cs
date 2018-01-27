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
                    mTransmitters[i].HackTransmitter(pWave.Colour);
                }
            }
        }

        public void AddTransmitter(Transmitter pTransmitter)
        {
            mTransmitters.Add(pTransmitter);
        }

        public void Draw(SpriteBatch pSpriteBatch, float pSeconds)
        {
            for (int i = 0; i < mTransmitters.Count; i++)
            {
                pSpriteBatch.Draw(mWhiteDisk, mTransmitters[i].Rect, mTransmitters[i].Colour);
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
                    mTransmitters[i].HackTransmitter(Color.Red);
                }
            }
        }
    }
}
