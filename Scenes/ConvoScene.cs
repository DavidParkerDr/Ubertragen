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

        float timePerChar = 0.05f;
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
        private Vector2 clickTextPos;

        private State state = State.AnimatingIn;
        private float animationProgress = 0f;

        float timeSinceChar = 0f;
        string visibleText = "";
        float timeInState = 0f;

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
                textBgRectangle.Width - 40,
                textBgRectangle.Height - 20);

            clickTextPos = new Vector2(
                screenWidth * 0.55f,
                screenHeight * 0.95f
            );
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
                24,
                visibleText,
                textRectangle
            );

            if (state == State.Static) {
                if ((int)timeInState % 2 == 0) {
                    sb.DrawString(bodyFont, "Click to continue", clickTextPos, Color.White);
                }
            }

            sb.End();
        }

        public void Update(float pSeconds)
        {
            timeInState += pSeconds;
            var mouseState = Mouse.GetState();

            timeSinceChar += pSeconds;

            var lmb = mouseState.LeftButton == ButtonState.Pressed;
            var segment = convo.Segments[segmentNum];

            if (state == State.AnimatingIn) {
                this.changeState(State.AnimatingText);
            }

            if (state == State.AnimatingText)
            {
                var tpc = lmb ? timePerChar / 2f : timePerChar;

                if (timeSinceChar > tpc)
                {
                    if (visibleText.Length < segment.Text.Length)
                    {
                        timeSinceChar = 0;
                        visibleText = segment.Text.Substring(0, visibleText.Length + 1);
                    }
                    else
                    {
                        this.changeState(State.Static);
                    }
                }
            }

            if (lmb &&
                state == State.Static &&
                timeInState > 0.5f) {
                if (segmentNum == convo.Segments.Count - 1)
                {
                    game.SM().Pop();
                } else {
                    visibleText = "";
                    segmentNum++;
                    this.changeState(State.AnimatingText);
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

        private void changeState(State state) {
            this.state = state;
            this.timeInState = 0f;
        }

        public void OnPop()
        {
            
        }
    }
}
