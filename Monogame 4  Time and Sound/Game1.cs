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
        Texture2D collisionTexture;       
        Rectangle collisionRect;
        Rectangle collisionRect2;
        Texture2D cutTexture;
        Rectangle cutRect;
        private SpriteFont timeFont;
        float seconds;
        float startTime;
        MouseState mouseState;
        SoundEffect explode;
        bool bombExploded;
        bool cut;
        bool done;
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
            cut = false;
            done = false;
            
            bombRect = new Rectangle(50, 50, 700, 400);
            expolsionRect = new Rectangle(-100, -10, 1000, 500);  
            collisionRect = new Rectangle(253, 136, 10, 10);
            collisionRect2 = new Rectangle(740, 188, 10, 50);
            cutRect = new Rectangle(740, 198, 10, 20);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bombTexture = Content.Load<Texture2D>("bomb");
            timeFont = Content.Load<SpriteFont>("Time");
            explode = Content.Load<SoundEffect>("explosion");
            expolsionTexture = Content.Load<Texture2D>("explosionPic");
            collisionTexture = Content.Load<Texture2D>("rectangle");
            cutTexture = Content.Load<Texture2D>("rectangle");
            soundEffectInstance = explode.CreateInstance();
            soundEffectInstance.IsLooped = false;
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!done)
            {
                seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
                if (seconds >= 15)
                {
                    if (!bombExploded)
                    {
                        bombExploded = true;
                        soundEffectInstance.Play();

                    }
                    else if (bombExploded && soundEffectInstance.State == SoundState.Stopped)
                    {
                        Exit();
                    }

                }
                else if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (collisionRect.Contains(mouseState.X, mouseState.Y))
                    {
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                }

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (collisionRect2.Contains(mouseState.X, mouseState.Y))
                    {
                        cut = true;
                        done = true;
                    }
                }
            }
            

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MidnightBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(collisionTexture, collisionRect2, Color.MidnightBlue);
            _spriteBatch.Draw(collisionTexture, collisionRect, Color.White);

            if (bombExploded)
            {
                _spriteBatch.Draw(expolsionTexture, expolsionRect, Color.White);
            }
            else
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                _spriteBatch.DrawString(timeFont, (15 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);
            }
            if (cut)
            {
                _spriteBatch.Draw(cutTexture, cutRect, Color.MidnightBlue);
            }
                      
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}