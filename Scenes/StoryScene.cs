using System;
using Microsoft.Xna.Framework;

namespace Transmission.Scenes
{
    public class StoryScene : IScene
    {
        public enum StoryPart {
            INTRO
        };

        public StoryScene(StoryPart part)
        {
        }

        public void Draw(float pSeconds)
        {
            Transmission.Instance().GDM().GraphicsDevice.Clear(Color.DarkGray);
   
        }

        public void Update(float pSeconds)
        {
        }
    }
}
