/*
    GameScreen.cs
    Copyright © Markku Rahikainen 2011.
	www.Stickmansoft.com
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaGame
{
    public abstract partial class GameScreen : DrawableGameComponent, IGameScreen
    {
        protected Rectangle TitleSafeArea;
        protected IGameScreenManager ScreenManager;
        
        public GameScreen(Game game)
            : base(game)
        {
            ScreenManager = (IGameScreenManager)game.Services.GetService(typeof(IGameScreenManager));
        }

        public GameScreen Screen
        {
            get
            {
                return (this);
            }
        }

        protected override void LoadContent()
        {
            TitleSafeArea = GraphicsDevice.Viewport.TitleSafeArea;
        }
        
        internal protected virtual void ScreenChanged(object sender, EventArgs e)
        {
            if (ScreenManager.TopScreen == this.Screen)
            {
                Visible = Enabled = true;
            }
            else
            {
                Visible = Enabled = false;
            }
        }
    }
}