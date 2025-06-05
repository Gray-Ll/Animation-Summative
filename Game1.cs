using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Animation_Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        Texture2D russiaFlag;

        Texture2D Intercept;
        Rectangle IntRect;
        Rectangle IntRect2;
        Rectangle IntRect3;
        Vector2 intSpeed;

        Texture2D missile;
        Rectangle missileR;
        Texture2D explosion;
        Rectangle explosionR;
        Vector2 missileS;

        Texture2D back1;
        Texture2D back2;

        SpriteFont LaunchText;
        SpriteFont MisText;
        float sec;

        float rotationAngle;
        Vector2 origin;
        Vector2 position;


        Texture2D milIntro;
        Rectangle mil;

        SoundEffect explode;
        SoundEffect fly;

        enum Screen
        {
            Intro,
            Military,
            Third
        }
        Screen screen;
        MouseState mouseState;

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

            sec = 0;

            IntRect = new Rectangle(300, 200, 75, 450);
            IntRect2 = new Rectangle(-50, 600, 75, 450);
            IntRect3 = new Rectangle(-50, 600, 75, 450);
            intSpeed = new Vector2(0, 0);

            explosionR = new Rectangle(-50, -50, 10, 10);

            // TODO: Add your initialization logic here
            screen = Screen.Intro;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            milIntro = Content.Load<Texture2D>("milIntro");
            Intercept = Content.Load<Texture2D>("Intercept");
            origin = new Vector2(Intercept.Width / 2f, Intercept.Height / 2f);
            position = new Vector2(Intercept.Width / 2f, Intercept.Height / 2f);
            back1 = Content.Load<Texture2D>("back1");
            back2 = Content.Load<Texture2D>("back2");
            LaunchText = Content.Load<SpriteFont>("LaunchText");
            MisText = Content.Load<SpriteFont>("MisText");
            missile = Content.Load<Texture2D>("missile");
            explosion = Content.Load<Texture2D>("explosion");

            explode = Content.Load<SoundEffect>("explode");
            fly = Content.Load<SoundEffect>("fly");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();
            if (screen == Screen.Intro)
            {
                IntRect.X += (int)intSpeed.X;
                IntRect.Y += (int)intSpeed.Y;

                IntRect2.X += (int)intSpeed.X;
                IntRect2.Y += (int)intSpeed.Y;

                IntRect3.X += (int)intSpeed.X;
                IntRect3.Y += (int)intSpeed.Y;

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    intSpeed = new Vector2(0, -7);

                    fly.Play();

                }
                if (IntRect.Bottom < -300)
                {
                    screen = Screen.Military;
                    IntRect2 = new Rectangle(-50, 600, 75, 400);
                    intSpeed = new Vector2(7, -7);
                    fly.Play();
                    sec = 0;
                }
            }
            else if (screen == Screen.Military)
            {

                IntRect.X += (int)intSpeed.X;
                IntRect.Y += (int)intSpeed.Y;

                IntRect2.X += (int)intSpeed.X;
                IntRect2.Y += (int)intSpeed.Y;

                IntRect3.X += (int)intSpeed.X;
                IntRect3.Y += (int)intSpeed.Y;

                if (IntRect2.Bottom < -30)
                {
                    screen = Screen.Third;
                    fly.Play();
                    IntRect3 = new Rectangle(-50, 200, 45, 350);
                    intSpeed = new Vector2(7, 0);
                    missileR = new Rectangle(520, 200, 50, 220);
                    missileS = new Vector2(0, -2);
                    sec = 0;
                }
            }
            else
            {

                IntRect.X += (int)intSpeed.X;
                IntRect.Y += (int)intSpeed.Y;

                IntRect2.X += (int)intSpeed.X;
                IntRect2.Y += (int)intSpeed.Y;

                IntRect3.X += (int)intSpeed.X;
                IntRect3.Y += (int)intSpeed.Y;

                missileR.X += (int)missileS.X;
                missileR.Y += (int)missileS.Y;

                if (IntRect3.Intersects(missileR))
                {
                    explosionR = new Rectangle(350, 10, 400, 400);
                    missileR = new Rectangle(0, 0, 0, 0);
                    IntRect3 = new Rectangle(0, 0, 0, 0);
                    explode.Play();
                }
            }

            // TODO: Add your update logic here

            mouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.RoyalBlue);

            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(milIntro, new Rectangle(0, 0, 800, 500), Color.White);
                //_spriteBatch.Draw(unacIntercept, new Rectangle(300, 200, 75, 345), Color.White);
                _spriteBatch.Draw(Intercept, IntRect, Color.White);

                _spriteBatch.DrawString(MisText, ("Russian Missile Inbound"), new Vector2(50, 50), Color.White);
                _spriteBatch.DrawString(MisText, ("Prepared Interceptor"), new Vector2(55, 120), Color.White);

                _spriteBatch.DrawString(LaunchText, ("PRESS TO"), new Vector2(400, 370), Color.White);
                _spriteBatch.DrawString(LaunchText, ("LAUNCH"), new Vector2(430, 435), Color.White);
            }
            else if (screen == Screen.Military)
            {

                //_spriteBatch.Draw(milIntro, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.Draw(back1, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.Draw(Intercept, IntRect2, null, Color.White, MathHelper.PiOver4, origin, SpriteEffects.None, 0f);

            }
            else if (screen == Screen.Third)
            {

                //_spriteBatch.Draw(milIntro, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.Draw(back2, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.Draw(Intercept, IntRect3, null, Color.White, MathHelper.PiOver2, origin, SpriteEffects.None, 0f);
                _spriteBatch.Draw(missile, missileR, Color.White);
                _spriteBatch.Draw(explosion, explosionR, Color.White);
            }

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}