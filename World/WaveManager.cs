using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission.World
{
    public class WaveManager
    {
        private List<Wave> mWaves;
        private static WaveManager mInstance = null;
        private Texture2D mWhiteCircle;

        private WaveManager()
        {
            mWaves = new List<Wave>();
            mWhiteCircle = Transmission.Instance().CM().Load<Texture2D>("white_circle");
        }

        public static WaveManager Instance()
        {
            if(mInstance == null)
            {
                mInstance = new WaveManager();
            }
            return mInstance;
        }

        public void AddWave(Point pPosition)
        {
            mWaves.Add(new Wave(pPosition));
        }

        public void Update(float pSeconds)
        {
            for (int i = 0; i < mWaves.Count; i++)
            {
                mWaves[i].Update(pSeconds);
                TransmitterManager.Instance().CheckWave(mWaves[i]);
            }

            for(int i = mWaves.Count - 1; i >= 0; i--)
            {
                if(mWaves[i].LifeLeft <= 0)
                {
                    mWaves.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch pSpriteBatch, float pSeconds)
        {
            for (int i = 0; i < mWaves.Count; i++)
            {
                pSpriteBatch.Draw(mWhiteCircle, mWaves[i].Rect, Color.Red);
            }
        }
    }
}
