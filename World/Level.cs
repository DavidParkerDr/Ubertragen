using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transmission;
using Transmission.Helpers;

namespace Transmission.World
{
    public class Level
    {
        private Texture2D mCursorTexture;
        private Rectangle mMouseRectangle;
        private Texture2D mWhiteCircle;
        private Texture2D mWhiteDisk;
        private SpriteBatch mSpriteBatch;

        public Level(string pFileName)
        {
            IGame game = Transmission.Instance();

            mCursorTexture = game.CM().Load<Texture2D>("pixel");
            mWhiteCircle = game.CM().Load<Texture2D>("white_circle");
            mWhiteDisk = game.CM().Load<Texture2D>("white_disk");
            mMouseRectangle = new Rectangle(0, 0, DGS.MOUSE_WIDTH, DGS.MOUSE_HEIGHT);
            mSpriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);

            StreamReader reader = new StreamReader(pFileName);

            while(!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                NodeManager transmitterManager = NodeManager.Instance();
                switch (values[0].ToLower())
                {
                    case "transmitter":
                        transmitterManager.AddNode(new Transmitter(int.Parse(values[1]), int.Parse(values[2]), values[3].ToColour()));
                        break;
                    default:
                        throw new Exception("Unrecognised token " + values[0] + " in " + pFileName);
                }

            }

            reader.Close();
        }

        public void Update(float pSeconds)
        {
            mMouseRectangle.X = Mouse.GetState().Position.X - DGS.MOUSE_WIDTH / 2;
            mMouseRectangle.Y = Mouse.GetState().Position.Y - DGS.MOUSE_HEIGHT / 2;

            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                NodeManager.Instance().CheckMouseClick(Mouse.GetState().Position);
            }
            NodeManager.Instance().Update(pSeconds);
            WaveManager.Instance().Update(pSeconds);
        }

        public void Draw(float pSeconds)
        {
            mSpriteBatch.Begin();

            Color color = Color.Goldenrod;

            WaveManager.Instance().Draw(mSpriteBatch, pSeconds);

            NodeManager.Instance().Draw(mSpriteBatch, pSeconds);


            mSpriteBatch.Draw(mCursorTexture, mMouseRectangle, Color.White);

            mSpriteBatch.End();
        }
    }
}
