using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        enum LevelState
        {
            Playing,
            Won,
            Lost,
            Propagating
        };

        private float propagatingTime = 3f;

        IGame game;
        GameScene scene;
        private Texture2D mCursorTexture;
        private Texture2D mOuterRingTexture;
        private Rectangle mVerticalMouseRectangle, mHorizontalMouseRectangle;
        private Texture2D mWhiteCircle;
        private Texture2D mWhiteDisk;
        private Texture2D mHacksUIBackground;
        private Rectangle mHacksUIRect;
        private SpriteBatch mSpriteBatch;
        private int mStartingNumberOfHacks;
        private int mHacksRemaining;
        private SpriteFont mFont;

        private LevelState State;
        private float timeInState;
        public bool HasFocus { get; set; }

        private SoundEffectInstance bgmLoop;
        private List<SoundEffectInstance> booms;

        public Level(GameScene scene, string pFileName)
        {
            this.scene = scene;

            game = Transmission.Instance();

            mCursorTexture = game.CM().Load<Texture2D>("pixel");
            bgmLoop = game.GetSoundManager().GetSoundEffectInstance("Sounds/music_loop");
            booms = new List<SoundEffectInstance>()
            {
                game.GetSoundManager().GetSoundEffectInstance("Sounds/boom_1"),
                game.GetSoundManager().GetSoundEffectInstance("Sounds/boom_2"),
                game.GetSoundManager().GetSoundEffectInstance("Sounds/boom_3")
            };

            mWhiteCircle = game.CM().Load<Texture2D>("white_circle");
            mOuterRingTexture = game.CM().Load<Texture2D>("white_disk");
            mHacksUIBackground = game.CM().Load<Texture2D>("UI/UI-09");
            mHacksUIRect = new Rectangle(0, 0, mHacksUIBackground.Width / 15, mHacksUIBackground.Height / 15);
            mVerticalMouseRectangle = new Rectangle(0, 0, 2, game.GDM().GraphicsDevice.Viewport.Height);
            mHorizontalMouseRectangle = new Rectangle(0, 0, game.GDM().GraphicsDevice.Viewport.Width, 2);
            mSpriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);
            mFont = game.CM().Load<SpriteFont>("Fonts/EurostileBold");
            StreamReader reader = new StreamReader(pFileName);

            string firstLine = reader.ReadLine();

            mStartingNumberOfHacks = int.Parse(firstLine.Substring(firstLine.IndexOf(':') + 1));
            mHacksRemaining = mStartingNumberOfHacks;
            this.State = LevelState.Playing;

            NodeManager nodeManager = NodeManager.Instance();
            nodeManager.ResetManager();
            WaveManager.Instance().Reset();
            bgmLoop.IsLooped = true;
            bgmLoop.Play();


            while(!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');
        
                switch (values[0].ToLower().Trim())
                {
                    case "transmitter":
                        nodeManager.AddNode(new Transmitter(int.Parse(values[1]), int.Parse(values[2]), values[3].ToColour()));
                        break;
                    case "unhackable":
                        nodeManager.AddNode(new Unhackable(int.Parse(values[1]), int.Parse(values[2]), values[3].ToColour()));
                        break;
                    case "absorber":
                        nodeManager.AddNode(new Absorber(int.Parse(values[1]), int.Parse(values[2]), values[3].ToColour()));
                        break;
                    case "cycler":
                        Color[] colours = new Color[values.Length - 3];
                        for(int i = 3; i < values.Length; i++)
                        {
                            colours[i - 3] = values[i].ToColour();
                        }
                        nodeManager.AddNode(new Cycler(int.Parse(values[1]), int.Parse(values[2]), colours));
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
            timeInState += pSeconds;

            if (HasFocus) {
                mVerticalMouseRectangle.X = Mouse.GetState().Position.X - 1;
                mHorizontalMouseRectangle.Y = Mouse.GetState().Position.Y - 1;
            }

            if (State == LevelState.Playing && HasFocus)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (mHacksRemaining > 0)
                    {
                        if (NodeManager.Instance().CheckMouseClick(Mouse.GetState().Position))
                        {
                            booms[(int)timeInState % 3].Play();

                            mHacksRemaining--;

                            if (mHacksRemaining == 0) {
                                this.changeState(LevelState.Propagating);
                            }
                        }
                    }
                }
            }

            if (State == LevelState.Playing ||
                State == LevelState.Propagating) {
                if (NodeManager.Instance().Won())
                {
                    this.changeState(LevelState.Won);
                }
            }

            if (State == LevelState.Propagating &&
                timeInState > propagatingTime) {
                this.changeState(LevelState.Lost);
                Transmission.Instance().SM().Push(new LevelFailScene(this));
            }

            if (State == LevelState.Won &&
                timeInState > propagatingTime &&
                this.HasFocus) {
                this.LevelWin();
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

            if (HasFocus)
            {
                mSpriteBatch.Draw(mCursorTexture, mVerticalMouseRectangle, Color.SlateGray);
                mSpriteBatch.Draw(mCursorTexture, mHorizontalMouseRectangle, Color.SlateGray);
            }

            mSpriteBatch.DrawString(mFont, 
                                    mHacksRemaining >= 10 ? 
                                        mHacksRemaining.ToString() : 
                                        "0" + mHacksRemaining.ToString(), 
                                    new Vector2(110, 28), 
                                    mHacksRemaining == 0 ? Color.Red : 
                                    (State == LevelState.Won ? Color.LimeGreen : Color.White));
            mSpriteBatch.End();
        }

        public void Reset()
        {
            State = LevelState.Playing;
            mHacksRemaining = mStartingNumberOfHacks;
            WaveManager.Instance().Reset();
            NodeManager.Instance().ResetNodes();
        }

        public void LevelWin()
        {
            game.SM().Push(new WonLevelScene(scene));
        }

        public void LevelFail()
        {
            Transmission.Instance().SM().Pop();
            Transmission.Instance().SM().Push(new LevelFailScene(this));
        }

        private void changeState(LevelState ls) {
            this.State = ls;
            this.timeInState = 0f;
        }

        public void OnPop() {
            this.bgmLoop.Stop();
        }
    }
}
