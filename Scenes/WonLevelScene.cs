using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Transmission.Scenes
{
    public class WonLevelScene : IScene
    {
        IGame game = Transmission.Instance();
        Vector2 wonTextPos;
        string wonText = "You Win!";

        private SpriteBatch sb;
        private SpriteFont font;

        public WonLevelScene()
        {
            font = game.CM().Load<SpriteFont>("Fonts/Eurostile");
            int screenWidth = game.GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = game.GDM().GraphicsDevice.Viewport.Height;

            sb = new SpriteBatch(game.GDM().GraphicsDevice);

            var mainTextSize = font.MeasureString(wonText);
            wonTextPos = new Vector2(
                screenWidth - mainTextSize.X / 2,
                screenHeight * 0.2f
            );
        }

        public void Draw(float pSeconds)
        {
            sb.Begin();

            sb.DrawString(font, wonText, wonTextPos, Color.White);

            sb.End();
        }

        public void HandleInput(float pSeconds)
        {
        }

        public void Update(float pSeconds)
        {
        }
    }
}
