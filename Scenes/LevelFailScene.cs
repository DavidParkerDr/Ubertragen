using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transmission.World;

namespace Transmission.Scenes
{
    public class LevelFailScene : IScene
    {
        private Texture2D mGUIBackgroundTexture;
        private Rectangle mGUIBackgroundRect;
        private Texture2D mRetryButtonTexture;
        private Rectangle mRetryButtonRect;
        private Texture2D mQuitButtonTexture;
        private Rectangle mQuitButtonRect;

        private Level mFailedLevel;
        private SpriteBatch mSpriteBatch;

        public LevelFailScene(Level pLevel)
        {
            mFailedLevel = pLevel;
            mGUIBackgroundTexture = Transmission.Instance().CM().Load<Texture2D>("pixel");
            mRetryButtonTexture = Transmission.Instance().CM().Load<Texture2D>("pixel");
            mQuitButtonTexture = Transmission.Instance().CM().Load<Texture2D>("pixel");

            mGUIBackgroundRect = new Rectangle();
            mRetryButtonRect = new Rectangle();
            mQuitButtonRect = new Rectangle();

            mSpriteBatch = new SpriteBatch(Transmission.Instance().GDM().GraphicsDevice);
        }

        public void Draw(float pSeconds)
        {
            mFailedLevel.Draw(pSeconds);

            mSpriteBatch.Begin();
            mSpriteBatch.Draw(mGUIBackgroundTexture, mGUIBackgroundRect, Color.White);
            mSpriteBatch.Draw(mQuitButtonTexture, mQuitButtonRect, Color.White);
            mSpriteBatch.Draw(mRetryButtonTexture, mRetryButtonRect, Color.White);
            mSpriteBatch.End();
        }

        public void Update(float pSeconds)
        {
            mFailedLevel.Update(pSeconds);
        }

        public void HandleInput(float pSeconds)
        {

        }

        public void ClickQuit()
        {
            Transmission.Instance().SM().Pop();
        }

        public void ClickRetry()
        {
            mFailedLevel.Reset();
            Transmission.Instance().SM().Pop();
            Transmission.Instance().SM().Push(new GameScene(mFailedLevel));
        }
    }
}
