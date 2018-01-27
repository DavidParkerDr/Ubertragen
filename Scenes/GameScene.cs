using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Input;
using Transmission.World;

namespace Transmission.Scenes
{
    public class GameScene : IScene
    {
        Level mLevel;

        public GameScene(string pLevelFile)
        {
            mLevel = new Level(pLevelFile);
        }


        public void Draw(float pSeconds)
        {
            Transmission.Instance().GDM().GraphicsDevice.Clear(Color.Black);

            mLevel.Draw(pSeconds);

        }

        public void Update(float pSeconds)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Transmission.Instance().Exit();
            }

            mLevel.Update(pSeconds);
        }

    
    }
}

