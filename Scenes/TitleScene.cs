using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Transmission.Scenes
{
    public class TitleScene : IScene
    {
        IGame game = Transmission.Instance();

        SpriteBatch spriteBatch;
        Texture2D titleTexture;
        Texture2D clickTexture;
        Point titleSize = new Point(500, 77);
        Point clickSize = new Point(500, 77);

        Rectangle titleRectangle;
        Rectangle clickRectangle;
        float elapsedTime = 0;

        public TitleScene()
        {
            this.spriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);
            this.titleTexture = game.CM().Load<Texture2D>("Title/title");
            this.clickTexture = game.CM().Load<Texture2D>("Title/click");

            int screenWidth = game.GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = game.GDM().GraphicsDevice.Viewport.Height;

            titleRectangle = new Rectangle((int)((screenWidth - titleSize.X) / 2f), (int)(screenHeight * 0.1f), titleSize.X, titleSize.Y);
            clickRectangle = new Rectangle((int)((screenWidth - clickSize.X) / 2f), (int)(screenHeight * 0.8f), clickSize.X, clickSize.Y);
        }

        public void Draw(float pSeconds)
        {
            Transmission.Instance().GDM().GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(titleTexture, titleRectangle, Color.White);

            if ((int)elapsedTime % 2 == 0)
                spriteBatch.Draw(clickTexture, clickRectangle, Color.White);

            spriteBatch.End();
        }

        public void Update(float pSeconds)
        {
            elapsedTime += pSeconds;

            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed) {
                game.SM().Pop();
                game.SM().Push(new StoryScene("Data/Story/Intro.json"));
            }
        }
    }
}
