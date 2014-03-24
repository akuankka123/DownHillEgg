/*
    GameScreenManagerInterfaces.cs
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
    public interface IGameScreen
    {
        GameScreen Screen
        { 
            get;
        }
    }

    public interface IGameScreenManager
    {
        GameScreen TopScreen
        { 
            get;
        }
        void PopScreen();
        void PushScreen(GameScreen Screen);
        void ChangeScreen(GameScreen NewScreen);
        bool ContainsScreen(GameScreen Screen);
        event EventHandler OnScreenChange;
    }
}
