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
        Texture2D coverTexture;
        Texture2D titleTexture;
        Texture2D clickTexture;
        Point clickSize = new Point(500, 77);

        SpriteFont font;

        Rectangle coverRectangle;
        Rectangle titleRectangle;
        Vector2 clickLocation;
        float elapsedTime = 0;

        string clickText = "Click here to start";

        public TitleScene()
        {
            this.spriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);
            this.titleTexture = game.CM().Load<Texture2D>("Title/Title");
            this.coverTexture = game.CM().Load<Texture2D>("Title/Cover");
            this.clickTexture = game.CM().Load<Texture2D>("Title/click");

            font = game.CM().Load<SpriteFont>("Fonts/Eurostile");

            int screenWidth = game.GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = game.GDM().GraphicsDevice.Viewport.Height;

            coverRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            var desiredTitleWidth = screenWidth * 0.8f;
            var titleScale =  desiredTitleWidth/ titleTexture.Width; 
            titleRectangle = new Rectangle(
                (int)(screenWidth * 0.1f), 
                (int)(screenHeight * 0.1f), 
                (int)(titleTexture.Width * titleScale),
                (int)(titleTexture.Height * titleScale));

            var clickTextSize = font.MeasureString(clickText);
            clickLocation = new Vector2(
                ((screenWidth - clickTextSize.X) / 2f), 
                (screenHeight * 0.8f));
        }

        public void Draw(float pSeconds)
        {
            Transmission.Instance().GDM().GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(coverTexture, coverRectangle, Color.White);


            spriteBatch.Draw(titleTexture, titleRectangle, Color.White);

            if ((int)elapsedTime % 2 == 0)
                spriteBatch.DrawString(font, clickText, clickLocation, Color.White);

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
