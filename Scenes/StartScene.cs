using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;


namespace Transmission.Scenes
{
    
    public class StartScene : IScene
    {
        ButtonList mButtonList = null;
        Texture2D mBackgroundTexture = null;
        Rectangle mBackgroundRectangle;

        Texture2D mForgroundTexture = null;

        Texture2D mTitleTexture = null;
        Rectangle mTitleRectangle;

        SpriteBatch mSpriteBatch = null;

        private SoundEffect startMusic = null;
        private SoundEffectInstance startMusicInstance = null;
        float mSecondsLeft;
        public StartScene() {
            IGame game = Transmission.Instance();

            mSpriteBatch = new SpriteBatch(game.GDM().GraphicsDevice);
            int screenWidth = game.GDM().GraphicsDevice.Viewport.Width;
            int screenHeight = game.GDM().GraphicsDevice.Viewport.Height;

            mBackgroundTexture = game.CM().Load<Texture2D>("background_01");         
            mBackgroundRectangle = new Rectangle(0, 0, screenWidth, screenHeight);


            mForgroundTexture = game.CM().Load<Texture2D>("menu_white");


            mTitleTexture = game.CM().Load<Texture2D>("menu_title");
            mTitleRectangle = new Rectangle((screenWidth / 2) - (644 / 2), (screenHeight / 2) - (128 / 2), 644, 128);

            mButtonList = new ButtonList();
                        
            Texture2D startGameButtonTexture = game.CM().Load<Texture2D>("menu_play_white");
            Texture2D startGameButtonTexturePressed = game.CM().Load<Texture2D>("menu_play_dark");
            
            Rectangle startGameButtonRectangle = 
                new Rectangle(
                    ((int)((screenWidth - startGameButtonTexture.Width) / 2) - (int)(startGameButtonTexture.Width * 0.75f)), 
                    (screenHeight) / 2 + startGameButtonTexture.Height, 
                    startGameButtonTexture.Width, 
                    startGameButtonTexture.Height);
            
            Button startGameButton = new Button(startGameButtonTexture,startGameButtonTexturePressed, startGameButtonRectangle, Color.Red, StartGame);
            startGameButton.Selected = true;
            mButtonList.Add(startGameButton);
            
            Texture2D exitGameButtonTexture = game.CM().Load<Texture2D>("menu_quit_white");
            Texture2D exitGameButtonTexturePressed = game.CM().Load<Texture2D>("menu_quit_dark");
            
            Rectangle exitGameButtonRectangle =
                new Rectangle((screenWidth - exitGameButtonTexture.Width) / 2 + (int)(startGameButtonTexture.Width * 0.75f),
                    (screenHeight) / 2 + exitGameButtonTexture.Width,
                    exitGameButtonTexture.Width, 
                    exitGameButtonTexture.Height);
            Button exitGameButton = new Button(exitGameButtonTexture, exitGameButtonTexturePressed, exitGameButtonRectangle, Color.Red, ExitGame);
            exitGameButton.Selected = false;
            mButtonList.Add(exitGameButton);
            mSecondsLeft = 0.1f;
            startMusic = game.CM().Load<SoundEffect>("Music/Music_start");  // Put the name of your song here instead of "song_title"
            startMusicInstance = game.GetSoundManager().GetLoopableSoundEffectInstance("Music/Music_start");
            startMusicInstance.Play();
            
         

         
        }

        

        public void ExitGame()
        {
            startMusicInstance.Stop();
            IGame game = Transmission.Instance();
            game.Exit();
        }

        public void StartGame()
        {
            startMusicInstance.Stop();
            IGame game = Transmission.Instance();
            game.SM().Pop();
            game.SM().Push(new GameScene());
        }

        public void Update(float pSeconds)
        {

        }
        public void Draw(float pSeconds)
        {
            Transmission.Instance().GDM().GraphicsDevice.Clear(Color.Black);
            mSpriteBatch.Begin();
            Color backColour = Color.White;
            
            mSpriteBatch.Draw(mBackgroundTexture, mBackgroundRectangle, backColour);
            mSpriteBatch.Draw(mForgroundTexture, mBackgroundRectangle, backColour);

            mSpriteBatch.Draw(mTitleTexture, mTitleRectangle, backColour);

            mButtonList.Draw(mSpriteBatch);

            mSpriteBatch.End();
        }
    }
}
