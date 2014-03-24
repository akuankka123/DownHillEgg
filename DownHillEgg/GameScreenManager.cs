/*
    GameScreenManager.cs
    Copyright © Markku Rahikainen 2011.
	www.Stickmansoft.com
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace XnaGame
{
    public class GameScreenManager : GameComponent, IGameScreenManager
    {
        private int drawOrder;
        private int baseDrawOrder = 20;
        public event EventHandler OnScreenChange;
        private Stack<GameScreen> screenStack = new Stack<GameScreen>();
       
        public GameScreenManager(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IGameScreenManager), this);
            drawOrder = baseDrawOrder;
        }

        public GameScreen TopScreen
        {
            get
            {
                return (screenStack.Peek());
            }
        }

        public bool ContainsScreen(GameScreen Screen)
        {
            return (screenStack.Contains(Screen));
        }

        public void PopScreen()
        {
            RemoveScreen();
            drawOrder -= 1;

            // Inform everyone we just changed screens.
            if (OnScreenChange != null)
            {
                OnScreenChange(this, null);
            }
        }

        private void RemoveScreen()
        {
            GameScreen ScreenToPopOut = (GameScreen)screenStack.Peek();

            // Unregister the event for this screen.
            OnScreenChange -= ScreenToPopOut.ScreenChanged;

            // Remove the screen from game components.
            Game.Components.Remove(ScreenToPopOut.Screen);

            screenStack.Pop();
        }

        public void PushScreen(GameScreen NewScreen)
        {
            drawOrder += 1;
            NewScreen.DrawOrder = drawOrder;
            
            AddScreen(NewScreen);

            // Inform everyone we just changed screens.
            if (OnScreenChange != null)
            {
                OnScreenChange(this, null);
            }
        }

        private void AddScreen(GameScreen Screen)
        {
            screenStack.Push(Screen);

            Game.Components.Add(Screen);

            // Register the event for this screen.
            OnScreenChange += Screen.ScreenChanged;
        }

        public void ChangeScreen(GameScreen NewScreen)
        {
            // We are changing screens, so pop everything off.
            // If we want to just modify screens, we should
            // use PushScreen and PopScreen.
            while (screenStack.Count > 0)
            {
                RemoveScreen();
            }

            // Changing screen, reset our draw order.
            NewScreen.DrawOrder = drawOrder = baseDrawOrder;
            AddScreen(NewScreen);

            // Inform everyone we just changed screens.
            if (OnScreenChange != null)
            {
                OnScreenChange(this, null);
            }
        }
    }
}
