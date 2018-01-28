using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transmission.Helpers;
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

        private Texture2D mCursorTexture;
        private Rectangle mMouseRectangle;

        private Level mFailedLevel;
        private SpriteBatch mSpriteBatch;
        private SoundEffectInstance levelFailSound;

        private float elapsedTime = 0f;

        public LevelFailScene(Level pLevel)
        {
            mFailedLevel = pLevel;
            IGame game = Transmission.Instance();
            mGUIBackgroundTexture = game.CM().Load<Texture2D>("pixel");
            mRetryButtonTexture = game.CM().Load<Texture2D>("pixel");
            mQuitButtonTexture = game.CM().Load<Texture2D>("pixel");

            mGUIBackgroundRect = new Rectangle(0, 0, 800, 400);
            mRetryButtonRect = new Rectangle(200, 100, 200, 100);
            mQuitButtonRect = new Rectangle(400, 100, 200, 100);

            mCursorTexture = game.CM().Load<Texture2D>("pixel");
            mMouseRectangle = new Rectangle(0, 0, DGS.MOUSE_WIDTH, DGS.MOUSE_HEIGHT);
            levelFailSound = game.GetSoundManager().GetSoundEffectInstance("Sounds/game over");

            mSpriteBatch = new SpriteBatch(Transmission.Instance().GDM().GraphicsDevice);
        }

        public void Draw(float pSeconds)
        {
            mFailedLevel.Draw(pSeconds);

            mSpriteBatch.Begin();
            mSpriteBatch.Draw(mGUIBackgroundTexture, mGUIBackgroundRect, Color.White);
            mSpriteBatch.Draw(mQuitButtonTexture, mQuitButtonRect, Color.Red);
            mSpriteBatch.Draw(mRetryButtonTexture, mRetryButtonRect, Color.Green);
            mSpriteBatch.Draw(mCursorTexture, mMouseRectangle, Color.White);
            mSpriteBatch.End();
        }

        public void Update(float pSeconds)
        {
            if (elapsedTime == 0) {
                this.levelFailSound.Play();
            }

            elapsedTime += pSeconds;

            mFailedLevel.Update(pSeconds);

            mMouseRectangle.X = Mouse.GetState().Position.X - DGS.MOUSE_WIDTH / 2;
            mMouseRectangle.Y = Mouse.GetState().Position.Y - DGS.MOUSE_HEIGHT / 2;

        }

        public void HandleInput(float pSeconds)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if(mQuitButtonRect.IsInside(Mouse.GetState().Position))
                {
                    ClickQuit();
                }
                else if(mRetryButtonRect.IsInside(Mouse.GetState().Position))
                {
                    ClickRetry();
                }
            }
        }

        public void ClickQuit()
        {
            Transmission.Instance().SM().Pop();
            Transmission.Instance().SM().Pop();
            Transmission.Instance().SM().Push(new TitleScene());
        }

        public void ClickRetry()
        {
            mFailedLevel.Reset();
            Transmission.Instance().SM().Pop();
        }

        public void OnPop()
        {
            this.levelFailSound.Stop();
        }
    }
}
