using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Transmission.Scenes.Story;

namespace Transmission.Scenes
{
    public class ConvoScene : IScene
    {
        private IGame game = Transmission.Instance();
        
        private enum State {
            AnimatingIn,
            AnimatingOut,
            AnimatingText,
            Static
        }

        float timePerChar = 0.1f;
        private float heightProp = 0.2f;
        private int height;

        private Convo convo;
        private int segmentNum;

        private SpriteBatch sb;

        SpriteFont bodyFont;
        private Texture2D pixelTexture;
        private Texture2D speakerTexture;
        private Rectangle speakerRectangle;
        private Rectangle textBgRectangle;
        private Rectangle textRectangle;

        private State state = State.AnimatingIn;
        private float animationProgress = 0f;

        float timeSinceChar = 0f;
        string visibleText = "";

        public ConvoScene(string filename)
        {
            convo = JsonConvert.DeserializeObject<Convo>(File.ReadAllText(filename));   

            int screenWidth = game.GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = game.GDM().GraphicsDevice.Viewport.Height;

            sb = new SpriteBatch(game.GDM().GraphicsDevice);
            pixelTexture = game.CM().Load<Texture2D>("pixel");
            this.bodyFont = game.CM().Load<SpriteFont>("Fonts/PressStart2P");

            speakerRectangle = new Rectangle(
                0,
                (int)(screenHeight * (1 - heightProp)), 
                (int)(screenHeight * heightProp), 
                (int)(screenHeight * heightProp));

            textBgRectangle = new Rectangle(
                speakerRectangle.Right,
                speakerRectangle.Top,
                screenWidth - speakerRectangle.Width,
                speakerRectangle.Height
            );

            textRectangle = new Rectangle(
                textBgRectangle.Left + 10,
                textBgRectangle.Top + 10,
                textBgRectangle.Width - 20,
                textBgRectangle.Height - 20);
        }


        public void Draw(float pSeconds)
        {
            sb.Begin();

            var segment = convo.Segments[segmentNum];
            sb.Draw(texForSpeaker(segment.Speaker), speakerRectangle, Color.White);
            sb.Draw(pixelTexture, textBgRectangle, Color.Black);

            Helpers.SpriteFontHelpers.RenderTextWithWrapping(
                sb,
                bodyFont,
                20,
                visibleText,
                textRectangle
            );

            sb.End();
        }

        public void Update(float pSeconds)
        {
            var mouseState = Mouse.GetState();

            timeSinceChar += pSeconds;

            var lmb = mouseState.LeftButton == ButtonState.Pressed;
            var segment = convo.Segments[segmentNum];

            if (state == State.AnimatingIn) {
                state = State.AnimatingText;
            }

            if (state == State.AnimatingText)
            {
                var tpc = lmb ? timePerChar : timePerChar / 2f;

                if (timeSinceChar > tpc)
                {
                    if (visibleText.Length < segment.Text.Length)
                    {
                        timeSinceChar = 0;
                        visibleText = segment.Text.Substring(0, visibleText.Length + 1);
                    }
                    else
                    {
                        this.state = State.Static;
                    }
                }
            }

            if (lmb &&
                state == State.Static) {
                if (segmentNum == convo.Segments.Count - 1)
                {
                    game.SM().Pop();
                } else {
                    visibleText = "";
                    segmentNum++;
                    state = State.AnimatingText;
                }
            }
                
        }

        public void HandleInput(float pSeconds) {
            
        }

        private Texture2D texForSpeaker(Speaker speaker) {
            return pixelTexture;
        }

        private string speakerName(Speaker speaker) {
            switch (speaker){
                case Speaker.Bird:
                    return "Bird";
                default: 
                    return "Unknown";
            }
        }
    }
}
