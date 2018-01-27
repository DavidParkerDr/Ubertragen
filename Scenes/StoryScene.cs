using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Transmission.Scenes.Story;

namespace Transmission.Scenes
{
    public class StoryScene : IScene
    {
        IGame game = Transmission.Instance();
        float timePerChar = 0.1f;

        SpriteBatch spriteBatch;
        SpriteFont titleFont;
        SpriteFont bodyFont;

        StoryPage page;

        string visibleText = "";
        float timeSinceChar = 0f;

        int visibleWidth;
        int lineHeight = 20;

        public StoryScene(string filename)
        {
            int screenWidth = game.GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = game.GDM().GraphicsDevice.Viewport.Height;

            visibleWidth = screenWidth - 80;

            this.spriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);
            this.titleFont = game.CM().Load<SpriteFont>("Fonts/8BitLimit");
            this.bodyFont = game.CM().Load<SpriteFont>("Fonts/PressStart2P");

            page = JsonConvert.DeserializeObject<StoryPage>(File.ReadAllText(filename));
        }

        public void Draw(float pSeconds)
        {
            Transmission.Instance().GDM().GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            string textToRender = visibleText;

            int line = 0;

            while (textToRender.Length > 0)
            {
                int c = 0;

                while (c < textToRender.Length && bodyFont.MeasureString(textToRender.Substring(0, c)).X < visibleWidth)
                {
                    c++;
                }

                if (c != 0)
                {
                    spriteBatch.DrawString(bodyFont, textToRender.Substring(0, c), new Vector2(40, 280 + (line * lineHeight)), Color.White);
                }

                line++;
                textToRender = textToRender.Substring(c);
            }

            spriteBatch.End();
   
        }

        public void Update(float pSeconds)
        {
            timeSinceChar += pSeconds;

            if (timeSinceChar > timePerChar) {
                timeSinceChar = 0;
                visibleText = page.Text.Substring(0, visibleText.Length + 1);
            }

            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                game.SM().Pop();
                game.SM().Push(new StoryScene("Data/Story/Intro.json"));
            }
        }
    }
}
