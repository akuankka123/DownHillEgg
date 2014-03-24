/*
    GameOverScreen.cs
    Copyright © Markku Rahikainen 2011.
	www.Stickmansoft.com
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XnaGame
{
    public sealed class GameOverScreen : MainGameScreen, IGameOverScreen
    {
        // Font
        SpriteFont screenFont;
        // Menu map
        Texture2D map;

        public GameOverScreen(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IGameOverScreen), this);
        }

        protected override void LoadContent()
        {
            screenFont = Content.Load<SpriteFont>("Fonts\\MenuFont");
            map = Content.Load<Texture2D>("Graphics\\GameOverScreen");
        }

        internal protected override void ScreenChanged(object sender, EventArgs e)
        {
            base.ScreenChanged(sender, e);

            if (ScreenManager.TopScreen != this.Screen)
            {
                Visible = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (XGame.bPressed)
            {
                // Clear the stack and pop start game screen on the stack.
                ScreenManager.ChangeScreen(XGame.StartGameScreen.Screen);
            }

            if (XGame.xPressed)
            {
                XGame.Exit();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSkyBlue);

            int clientBoundsWidth = GraphicsDevice.Viewport.Width;
            int clientBoundsHeight = GraphicsDevice.Viewport.Height;
            Vector2 textSize = screenFont.MeasureString("Game Over Screen");
            Vector2 position = new Vector2(clientBoundsWidth / 2 - textSize.X / 2,
                                           clientBoundsHeight / 2 - textSize.Y / 2);

            Int32 mapHeight = 180; //XGame.Window.ClientBounds.Width - 20;
            Int32 mapWidth = 232; //(map.Width / map.Height) * mapHeight;
            Int32 xPos = 0;//(XGame.Window.ClientBounds.Height / 2) - mapWidth / 2 + 40;
            Int32 yPos = 0;//10;
            XGame.SpriteBatch.Draw(map, new Rectangle(xPos, yPos, mapWidth, mapHeight), Color.White);

            /*
            XGame.SpriteBatch.DrawString(screenFont, "Game Over Screen", position, Color.Black);
            XGame.SpriteBatch.DrawString(screenFont, "Game Over Screen",
                                         new Vector2(position.X - 3, position.Y - 3), Color.White);*/

            base.Draw(gameTime);
        }
    }
}
