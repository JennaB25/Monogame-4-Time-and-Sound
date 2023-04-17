using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_4__Time_and_Sound
{
    public class Game1 : Game
    {
        Texture2D bombTexture;
        Rectangle bombRect;
        Texture2D expolsionTexture;
        Rectangle expolsionRect;
        private SpriteFont timeFont;
        float seconds;
        float startTime;
        MouseState mouseState;
        SoundEffect explode;
        bool bombExploded;
        SoundEffectInstance soundEffectInstance;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            this.Window.Title = "Bomb Game";
            bombExploded = false;

            bombRect = new Rectangle(50, 50, 700, 400);
            expolsionRect = new Rectangle(-100, -10, 1000, 500);
            startTime = 15;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bombTexture = Content.Load<Texture2D>("bomb");
            timeFont = Content.Load<SpriteFont>("Time");
            explode = Content.Load<SoundEffect>("explosion");
            expolsionTexture = Content.Load<Texture2D>("explosionPic");
            soundEffectInstance = explode.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            seconds = startTime - (float)gameTime.TotalGameTime.TotalSeconds;
            if (seconds <= 0)
            {                        
                soundEffectInstance.Play();
                while (soundEffectInstance.State == SoundState.Playing)
                {
                    bombExploded = true;
                }
                Exit();
            }
            else if (mouseState.LeftButton == ButtonState.Pressed)
            {
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }          

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MidnightBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, bombRect, Color.White);           
            _spriteBatch.DrawString(timeFont, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);
            if (bombExploded == true)
            {
                _spriteBatch.Draw(expolsionTexture, expolsionRect, Color.White);
            }        
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}