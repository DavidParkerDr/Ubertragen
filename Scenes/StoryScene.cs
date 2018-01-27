using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Transmission.Helpers;
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

            SpriteFontHelpers.RenderTextWithWrapping(
                spriteBatch,
                bodyFont,
                lineHeight,
                visibleText,
                new Rectangle(40, 280, 560, 0));

            spriteBatch.End();
        }

        public void Update(float pSeconds)
        {
            timeSinceChar += pSeconds;

            if (timeSinceChar > timePerChar && visibleText.Length < page.Text.Length) {
                timeSinceChar = 0;
                visibleText = page.Text.Substring(0, visibleText.Length + 1);
            }
        }

        public void HandleInput(float pSeconds)
        {
            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                game.SM().GotoScene(page.Next);
            }
        }
    }
}
