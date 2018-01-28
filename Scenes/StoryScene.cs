using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Transmission.Helpers;
using Transmission.Scenes.Story;
using Microsoft.Xna.Framework.Audio;

namespace Transmission.Scenes
{
    public class StoryScene : IScene
    {
        enum StorySceneState
        {
            Animating,
            Static
        };

        IGame game = Transmission.Instance();
        float timePerChar = 0.06f;

        SpriteBatch spriteBatch;
        SpriteFont titleFont;
        SpriteFont bodyFont;

        StoryPage page;
        StorySceneState state = StorySceneState.Animating;

        string visibleText = "";
        float timeSinceChar = 0f;

        int visibleWidth;
        int lineHeight = 30;
        Rectangle textRectangle;
        bool mouseDown = false;
        bool mButtonPressed = false;
        bool mButtonReleased = false;

        SoundEffectInstance mVoiceover;

        float timeInState = 0f;

        public StoryScene(string filename)
        {
            int screenWidth = game.GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = game.GDM().GraphicsDevice.Viewport.Height;

            visibleWidth = screenWidth - 80;

            this.spriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);
            this.titleFont = game.CM().Load<SpriteFont>("Fonts/8BitLimit");
            this.bodyFont = game.CM().Load<SpriteFont>("Fonts/PressStart2P");

            textRectangle = new Rectangle(
                40,
                (int)(screenHeight * 0.4f),
                screenWidth - 80,
                (int)(screenHeight * 0.4f));

            page = JsonConvert.DeserializeObject<StoryPage>(File.ReadAllText(filename));
            mVoiceover = game.GetSoundManager().GetSoundEffectInstance(page.Voiceover);
            mVoiceover.Play();
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                mButtonReleased = false;
            }
            else
            {
                mButtonReleased = true;
            }
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
                textRectangle);

            spriteBatch.End();
        }

        public void Update(float pSeconds)
        {
            timeInState += pSeconds;

            timeSinceChar += pSeconds;

            var tpc = mouseDown ? timePerChar / 2 : timePerChar;

            if (state == StorySceneState.Animating)
            {
                if (timeSinceChar > tpc)
                {
                    if (visibleText.Length < page.Text.Length)
                    {
                        timeSinceChar = 0;
                        visibleText = page.Text.Substring(0, visibleText.Length + 1);
                    }
                    else
                    {
                        this.changeState(StorySceneState.Static);
                    }
                }
            }
        }

        public void HandleInput(float pSeconds)
        {
            var mouseState = Mouse.GetState();



            if (mouseDown && 
                mouseState.LeftButton == ButtonState.Released &&
                state == StorySceneState.Static &&
                timeInState > 0.5f)
            {
                mVoiceover.Stop();
                game.SM().GotoScene(page.Next);
            }

            if (mButtonReleased && mouseState.LeftButton == ButtonState.Pressed)
            {
                mButtonPressed = true;
            }

            if(!mButtonPressed && mouseState.LeftButton == ButtonState.Released)
            {
                mButtonReleased = true;
            }

            mouseDown = mouseState.LeftButton == ButtonState.Pressed;
        }

        private void changeState(StorySceneState state)
        {
            this.state = state;
            this.timeInState = 0f;
        }


        public void OnPop()
        {
            
        }
    }
}
