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
using Transmission.Scenes;

namespace Transmission.World
{
    public class Level
    {
        private Texture2D mCursorTexture;
        private Rectangle mMouseRectangle;
        private Texture2D mWhiteCircle;
        private Texture2D mWhiteDisk;
        private Texture2D mHacksUIBackground;
        private Rectangle mHacksUIRect;
        private SpriteBatch mSpriteBatch;
        private int mStartingNumberOfHacks;
        private int mHacksRemaining;
        private SpriteFont mFont;

        public Level(string pFileName)
        {
            IGame game = Transmission.Instance();

            mCursorTexture = game.CM().Load<Texture2D>("pixel");
            mWhiteCircle = game.CM().Load<Texture2D>("white_circle");
            mWhiteDisk = game.CM().Load<Texture2D>("white_disk");
            mHacksUIBackground = game.CM().Load<Texture2D>("UI/UI-09");
            mHacksUIRect = new Rectangle(0, 0, mHacksUIBackground.Width / 15, mHacksUIBackground.Height / 15);
            mMouseRectangle = new Rectangle(0, 0, DGS.MOUSE_WIDTH, DGS.MOUSE_HEIGHT);
            mSpriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);
            mFont = game.CM().Load<SpriteFont>("Fonts/EurostileBold");
            StreamReader reader = new StreamReader(pFileName);

            string firstLine = reader.ReadLine();

            mStartingNumberOfHacks = int.Parse(firstLine.Substring(firstLine.IndexOf(':') + 1));
            mHacksRemaining = mStartingNumberOfHacks;
            while(!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                NodeManager nodeManager = NodeManager.Instance();
                switch (values[0].ToLower().Trim())
                {
                    case "transmitter":
                        nodeManager.AddNode(new Transmitter(int.Parse(values[1]), int.Parse(values[2]), values[3].ToColour()));
                        break;
                    case "unhackable":
                        nodeManager.AddNode(new Unhackable(int.Parse(values[1]), int.Parse(values[2])));
                        break;
                    case "absorber":
                        nodeManager.AddNode(new Absorber(int.Parse(values[1]), int.Parse(values[2])));
                        break;
                    case "mover":
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

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (mHacksRemaining > 0)
                {
                    if(NodeManager.Instance().CheckMouseClick(Mouse.GetState().Position))
                    {
                        mHacksRemaining--;
                    }
                }
            }
            NodeManager.Instance().Update(pSeconds);
            WaveManager.Instance().Update(pSeconds);


        }

        public void Draw(float pSeconds)
        {
            mSpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);

            WaveManager.Instance().Draw(mSpriteBatch, pSeconds);

            NodeManager.Instance().Draw(mSpriteBatch, pSeconds);
            mSpriteBatch.End();

            mSpriteBatch.Begin();
            mSpriteBatch.Draw(mHacksUIBackground, mHacksUIRect, Color.White);

            mSpriteBatch.Draw(mCursorTexture, mMouseRectangle, Color.White);
            mSpriteBatch.DrawString(mFont, mHacksRemaining >= 10 ? mHacksRemaining.ToString() : "0" + mHacksRemaining.ToString(), new Vector2(110, 28), Color.White);
            mSpriteBatch.End();
        }

        public void Reset()
        {
            mHacksRemaining = mStartingNumberOfHacks;
            WaveManager.Instance().Reset();
            NodeManager.Instance().ResetNodes();
        }

        public void LevelWin()
        {
            throw new NotImplementedException();
        }

        public void LevelFail()
        {
            Transmission.Instance().SM().Pop();
            Transmission.Instance().SM().Push(new LevelFailScene(this));
        }
    }
}
