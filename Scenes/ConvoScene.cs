using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            Static
        }

        private int height;

        private Convo convo;
        private int segment;

        private SpriteBatch sb;
        private Texture2D speakerTexture;
        private Rectangle speakerRectangle;

        public ConvoScene(string filename)
        {
            convo = JsonConvert.DeserializeObject<Convo>(File.ReadAllText(filename));   

            int screenWidth = game.GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = game.GDM().GraphicsDevice.Viewport.Height;

            var sb = new SpriteBatch(game.GDM().GraphicsDevice);
        }


        public void Draw(float pSeconds)
        {
            sb.Begin();

            sb.Draw(speakerTexture, speakerRectangle, Color.White);

            sb.End();
        }

        public void Update(float pSeconds)
        {
            
        }

        private Texture2D texForSpeaker(Speaker speaker) {
            return game.CM().Load<Texture2D>("pixel");
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
