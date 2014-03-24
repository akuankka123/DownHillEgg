
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace XnaGame
{
    /*
    Here are defined all the empty interfaces that all inherit from IGameScreen.
    This allows the game screen objects to all be accessed the same way and 
    allows them to be registered as a game services. A game service and 
    therefore game screen too must have a unique interface. Whenever the game
    needs a new game screen, the game need to add new interface. */
    public interface IStartGameScreen : IGameScreen { }
    public interface IPlayGameScreen : IGameScreen { }
    public interface IGameOverScreen : IGameScreen { }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class XnaGame : Microsoft.Xna.Framework.Game
    {
        //audio
        SoundEffect wavSoundEffect;

        // HW
        GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch;
        // Game screen management
        GameScreenManager screenManager;
        // Game screens
        public IStartGameScreen StartGameScreen;
        public IPlayGameScreen PlayGameScreen;
        public IGameOverScreen GameOverScreen;
        // Touch
        Vector2 touchPosition = Vector2.Zero;
        Rectangle rectA = Rectangle.Empty;
        Rectangle rectB = Rectangle.Empty;
        Rectangle rectS = Rectangle.Empty;
        Rectangle rectX = Rectangle.Empty;
        Rectangle munaposition = Rectangle.Empty;
        public Int32 buttonSize = 80;
        Texture2D buttonA;
        Texture2D buttonB;
        Texture2D buttonS;
        Texture2D buttonX;
        Texture2D munaground;
        public Boolean aPressed = false;
        public Boolean bPressed = false;
        public Boolean sPressed = false;
        public Boolean xPressed = false;

        public XnaGame()
        {

            graphics = new GraphicsDeviceManager(this);

            graphics.SupportedOrientations = DisplayOrientation.Portrait;
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
                
                //DisplayOrientation.LandscapeLeft |
                //                             DisplayOrientation.LandscapeRight;
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Game screen manager
            screenManager = new GameScreenManager(this);
            Components.Add(screenManager);

            // Game screens
            StartGameScreen = new StartGameScreen(this);
            PlayGameScreen = new PlayGameScreen(this);
            GameOverScreen = new GameOverScreen(this);

            // First game screen.
            screenManager.ChangeScreen(StartGameScreen.Screen);


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
            wavSoundEffect = Content.Load<SoundEffect>("Audio\\DSAASD");
            wavSoundEffect.Play();

            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            munaground = Content.Load<Texture2D>("Graphics\\vammajotakii");

            // Touch
            buttonA = Content.Load<Texture2D>("Graphics\\ButtonA");
            buttonB = Content.Load<Texture2D>("Graphics\\ButtonB");
            buttonS = Content.Load<Texture2D>("Graphics\\ButtonS");
            buttonX = Content.Load<Texture2D>("Graphics\\ButtonX");
            rectA = new Rectangle(50, 670, buttonSize, buttonSize);
            rectB = new Rectangle(200, 670, buttonSize, buttonSize);
            rectS = new Rectangle(340, 670, buttonSize, buttonSize);
            munaposition = new Rectangle(0, 0, 480, 800);
            rectX = new Rectangle(Window.ClientBounds.Height - 10, 3, buttonSize, buttonSize);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
#if WINDOWS || XBOX
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }
#endif
            // Allows the game to exit.
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                this.Exit();
            }

            // Touch
            HandleTouchScreenInputs();

            base.Update(gameTime);
        }

        private void HandleTouchScreenInputs()
        {
            TouchCollection touchCollection = TouchPanel.GetState();

            foreach (TouchLocation touchLoc in touchCollection)
            {
                if ((touchLoc.State == TouchLocationState.Pressed) /*|| (touchLoc.State == TouchLocationState.Moved)*/)
                {
                    touchPosition = touchLoc.Position;
                }
            }

            // Button A
            if (CheckIntersection(rectA, touchPosition))
            {
                aPressed = true;
            }
            // Button B
            else if (CheckIntersection(rectB, touchPosition))
            {
                bPressed = true;
            }
            // Button S
            else if (CheckIntersection(rectS, touchPosition))
            {
                sPressed = true;
            }
            // Buton X
            else if (CheckIntersection(rectX, touchPosition))
            {
                xPressed = true;
            }
            else
            {
                aPressed = false;
                bPressed = false;
                sPressed = false;
                xPressed = false;
            }

            touchPosition = Vector2.Zero;
        }

        private Boolean CheckIntersection(Rectangle Rect, Vector2 Pt)
        {
            if (Rect.Intersects(new Rectangle((int)Pt.X - 1, (int)Pt.Y - 1, 2, 2)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();

            SpriteBatch.Draw(munaground, munaposition, Color.White);
            // Touch
            SpriteBatch.Draw(buttonA, rectA, Color.White);
            SpriteBatch.Draw(buttonB, rectB, Color.White);
            SpriteBatch.Draw(buttonS, rectS, Color.White);
            SpriteBatch.Draw(buttonX, rectX, Color.White);

            base.Draw(gameTime);



            SpriteBatch.End();
        }
    }
}
