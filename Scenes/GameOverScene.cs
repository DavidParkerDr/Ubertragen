using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission.Scenes
{
    public class GameOverScene : IScene
    {
        Texture2D mBackgroundTexture = null;
        SpriteBatch mSpriteBatch = null;
        Rectangle mRectangle;
        float mSecondsLeft;
        public GameOverScene(Texture2D pBackgroundTexture)
        {
            IGame game = Transmission.Instance();
            mBackgroundTexture = pBackgroundTexture;
            mSpriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);
            int screenWidth = game.GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = game.GDM().GraphicsDevice.Viewport.Height;
            int height = screenHeight / 2;
            int width = (int)(mBackgroundTexture.Width * (float)height / mBackgroundTexture.Height);
            int x = (screenWidth - width) / 2;
            int y = (screenHeight - height) / 2;
            mRectangle = new Rectangle(x, y, width, height);
            mSecondsLeft = DGS.SECONDS_TO_DISPLAY_FLASH_SCREEN;
        }

        public void Update(float pSeconds)
        {
            mSecondsLeft -= pSeconds;
            if (mSecondsLeft <= 0.0f)
            {
                IGame game = Transmission.Instance();
                game.SM().Pop();
                game.SM().Push(new StartScene());
            }
        }
        public void Draw(float pSeconds)
        {
            Transmission.Instance().GDM().GraphicsDevice.Clear(Color.Black);
            mSpriteBatch.Begin();

            mSpriteBatch.Draw(mBackgroundTexture, mRectangle, Color.White);

            mSpriteBatch.End();
        }
    }
}
