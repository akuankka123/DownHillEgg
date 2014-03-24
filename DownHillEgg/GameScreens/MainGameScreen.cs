/*
    GameMainScreen.cs
    Copyright © Markku Rahikainen 2011.
	www.Stickmansoft.com
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

/*
    This class is optional because each individual screen could simply
    inherit from GameScreen class. Using this class we don't need to
    implement the same code for many of our games screens.
*/

namespace XnaGame
{
    public partial class MainGameScreen : GameScreen
    {
        protected XnaGame XGame;
        protected ContentManager Content;
        // Button speed bump.
        static protected Boolean buttonSpeedBump;
        Thread buttonSpeedBumpThread = null;

        public MainGameScreen(Game game)
            : base(game)
        {
            Content = game.Content;
            XGame = (XnaGame)game;
        }

        // Button speed bump.

        public Boolean ButtonSpeedBump
        {
            get
            {
                return buttonSpeedBump;
            }
            set
            {
                buttonSpeedBump = value;
            }
        }

        public void StartButtonSpeedBump()
        {
            Thread.Sleep(500);
            ButtonSpeedBump = true;
            buttonSpeedBumpThread = new Thread(new ThreadStart(ButtonSpeedBumpThread));
            buttonSpeedBumpThread.Start();
        }

        protected void ButtonSpeedBumpThread()
        {
            Thread.Sleep(500);

            ButtonSpeedBump = false;
        }
    }
}
