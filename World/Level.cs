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

namespace Transmission.World
{
    public class Level
    {
        private Texture2D mCursorTexture;
        private Rectangle mMouseRectangle;
        private SpriteBatch mSpriteBatch;

        private List<Transmitter> mTransmitters;

        public Level(string pFileName)
        {
            IGame game = Transmission.Instance();

            mCursorTexture = game.CM().Load<Texture2D>("pixel");
            mMouseRectangle = new Rectangle(0, 0, DGS.MOUSE_WIDTH, DGS.MOUSE_HEIGHT);
            mSpriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);

            mTransmitters = new List<Transmitter>();

            StreamReader reader = new StreamReader(pFileName);

            while(!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                switch (values[0].ToLower())
                {
                    case "transmitter":
                        mTransmitters.Add(new Transmitter(int.Parse(values[1]), int.Parse(values[2])));
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
        }

        public void Draw(float pSeconds)
        {
            mSpriteBatch.Begin();

            for(int i = 0; i < mTransmitters.Count; i++)
            {
                mSpriteBatch.Draw(mCursorTexture, mTransmitters[i].Rect, Color.Goldenrod);
            }

            mSpriteBatch.Draw(mCursorTexture, mMouseRectangle, Color.White);

            mSpriteBatch.End();
        }
    }
}
