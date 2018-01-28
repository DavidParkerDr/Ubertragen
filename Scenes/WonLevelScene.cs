using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Transmission.Scenes
{
    public class WonLevelScene : IScene
    {
        IGame game = Transmission.Instance();
        GameScene gameScene;
        Vector2 wonTextPos;
        Vector2 clickTextPos;
        string wonText = "Level Complete!";
        string clickText = "Click to continue";

        private SpriteBatch sb;
        private SpriteFont font;

        float elapsedTime;

        public WonLevelScene(GameScene scene)
        {
            gameScene = scene;
            font = game.CM().Load<SpriteFont>("Fonts/EurostileBold");
            int screenWidth = game.GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = game.GDM().GraphicsDevice.Viewport.Height;

            sb = new SpriteBatch(game.GDM().GraphicsDevice);

            var mainTextSize = font.MeasureString(wonText);
            wonTextPos = new Vector2(
                (screenWidth - mainTextSize.X) / 2f,
                screenHeight * 0.2f
            );

            var clickTextSize = font.MeasureString(clickText);
            clickTextPos = new Vector2(
                ((screenWidth - clickTextSize.X) / 2f),
                (screenHeight * 0.8f));
        }

        public void Draw(float pSeconds)
        {
            sb.Begin();

            sb.DrawString(font, wonText, wonTextPos, Color.White);

            if ((int)pSeconds % 2 == 0)
            {
                sb.DrawString(font, clickText, clickTextPos, Color.White);
            }

            sb.End();
        }

        public void HandleInput(float pSeconds)
        {
            elapsedTime += pSeconds;

            if (elapsedTime > 2f)
            {
                var mouseState = Mouse.GetState();

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    // TODO: Not this.
                    game.SM().Pop();
                    game.SM().Pop();
                    game.SM().GotoScene(gameScene.stage.Next);
                }
            }
        }

        public void Update(float pSeconds)
        {
            elapsedTime += pSeconds;
        }
    }
}
