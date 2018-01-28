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
using Transmission.Scenes.Story;

namespace Transmission.Scenes
{
    public class GameScene : IScene
    {
        public Stage stage;
        Level mLevel;

        public GameScene(Stage stage)
        {
            this.stage = stage;

            mLevel = new Level(this, stage.Level);
        }

        public GameScene(Level pLevel)
        {
            mLevel = pLevel;
        }

        public void Draw(float pSeconds)
        {
            Transmission.Instance().GDM().GraphicsDevice.Clear(DGS.BLACK);

            mLevel.Draw(pSeconds);

        }

        public void Update(float pSeconds)
        {
            mLevel.HasFocus = (this == Transmission.Instance().SM().Top);

            mLevel.Update(pSeconds);
        }

        public void HandleInput(float pSeconds)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Transmission.Instance().Exit();
            }
        }

        public void OnPop() {
            this.mLevel.OnPop();
        }
    }
}

