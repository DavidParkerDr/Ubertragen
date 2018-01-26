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

namespace Transmission.Scenes
{
    public class GameScene : IScene
    {

        Texture2D mCursorTexture;
        Rectangle mMouseRectangle;
        SpriteBatch mSpriteBatch;

        public GameScene()
        {
            IGame game = Transmission.Instance();
            mCursorTexture = game.CM().Load<Texture2D>("pixel");
            mMouseRectangle = new Rectangle(0, 0, DGS.MOUSE_WIDTH, DGS.MOUSE_HEIGHT);
            mSpriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);
        }


        public void Draw(float pSeconds)
        {
            Transmission.Instance().GDM().GraphicsDevice.Clear(Color.CornflowerBlue);

            mSpriteBatch.Begin();
            mSpriteBatch.Draw(mCursorTexture, mMouseRectangle, Color.White);
            mSpriteBatch.End();

        }

        public void Update(float pSeconds)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Transmission.Instance().Exit();
            }

            mMouseRectangle.X = Mouse.GetState().Position.X - DGS.MOUSE_WIDTH / 2;
            mMouseRectangle.Y = Mouse.GetState().Position.Y - DGS.MOUSE_HEIGHT / 2;
        }

    
    }
}

