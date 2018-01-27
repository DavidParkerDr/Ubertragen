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

            Transmission.Instance().SM().Push(new ConvoScene("Data/Convos/test.json"));
        }


        public void Draw(float pSeconds)
        {
            Transmission.Instance().GDM().GraphicsDevice.Clear(Color.CornflowerBlue);

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

