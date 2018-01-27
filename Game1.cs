using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO.Ports;
using Transmission.Scenes;

namespace Transmission
{
    /// <summary>
    /// This interface specifies things that we want to get global access to (probs everything)
    /// </summary>
    public interface IGame
    {
        SoundManager GetSoundManager();
        SceneManager SM();
        ContentManager CM();
        GraphicsDeviceManager GDM();

        void Exit();
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Transmission : Game, IGame
    {
        private SoundManager mSoundManager;
        private GraphicsDeviceManager mGraphics;
        private SpriteBatch mBatch;
        private SceneManager mSceneManager;
        private static IGame mGameInterface = null;

        public static IGame Instance()
        {
            if(mGameInterface == null)
            {
                mGameInterface = new Transmission();
            }
            return mGameInterface;
        }

        public SoundManager GetSoundManager() { return mSoundManager; }
        public SceneManager SM() { return mSceneManager; }
        public ContentManager CM() { return Content; }
        public GraphicsDeviceManager GDM() { return mGraphics; }
        public Transmission()
        {
            mGraphics               = new GraphicsDeviceManager(this);
            Content.RootDirectory   = "Content";
            mSceneManager           = new SceneManager();

            mSoundManager = new SoundManager();

            mGraphics.PreferredBackBufferHeight = 1668 / 2;
            mGraphics.PreferredBackBufferWidth = 2224 / 2;
            mGraphics.IsFullScreen =  false;
            this.Window.Title = "Transmission";
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            mBatch = new SpriteBatch(GraphicsDevice);


            mSceneManager.Push(new FlashScreenScene());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float seconds = 0.001f * gameTime.ElapsedGameTime.Milliseconds;
            mSceneManager.Update(seconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            float seconds = 0.001f * gameTime.ElapsedGameTime.Milliseconds;
            mSceneManager.Draw(seconds);
            base.Draw(gameTime);
        }
    }
}
