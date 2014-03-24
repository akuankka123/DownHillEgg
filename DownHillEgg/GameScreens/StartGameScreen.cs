
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
    public sealed class StartGameScreen : MainGameScreen, IStartGameScreen
    {
        // Font
        SpriteFont screenFont;
        // Menu map
        Texture2D map;

        public StartGameScreen(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IStartGameScreen), this);
        }

        protected override void LoadContent()
        {
            screenFont = Content.Load<SpriteFont>("Fonts\\MenuFont");
            map = Content.Load<Texture2D>("Graphics\\StartGameScreen");

            
        }

        public override void Update(GameTime gameTime)
        {
            if (XGame.xPressed)
            {
                XGame.Exit();
            }

            if (XGame.sPressed)
            {
                // Push play game screen onto the stack.
                ScreenManager.PushScreen(XGame.PlayGameScreen.Screen);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {



            GraphicsDevice.Clear(Color.LightSkyBlue);

            Int32 clientBoundsWidth = GraphicsDevice.Viewport.Width;
            Int32 clientBoundsHeight = GraphicsDevice.Viewport.Height;
            Vector2 textSize = screenFont.MeasureString("Start Game Screen");
            Vector2 position = new Vector2(clientBoundsWidth / 2 - textSize.X / 2,
                                           clientBoundsHeight / 2 - textSize.Y / 2);

            Int32 mapHeight = 180; //XGame.Window.ClientBounds.Width - 20;
            Int32 mapWidth = 232; //(map.Width / map.Height) * mapHeight;
            Int32 xPos = 0;//(XGame.Window.ClientBounds.Height / 2) - mapWidth / 2 + 40;
            Int32 yPos = 0;//10;
            XGame.SpriteBatch.Draw(map, new Rectangle(xPos, yPos, mapWidth, mapHeight), Color.White);

            /*
            XGame.SpriteBatch.DrawString(screenFont, "Start Game Screen", position, Color.Black);
            XGame.SpriteBatch.DrawString(screenFont, "Start Game Screen",
                                         new Vector2(position.X - 3, position.Y - 3), Color.White);*/

            base.Draw(gameTime);
        }

    }
}
