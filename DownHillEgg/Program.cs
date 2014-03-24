/*
    Programs.cs
    Copyright © Stickmansoft 2012.
	www.Stickmansoft.com
*/
using System;

namespace XnaGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (XnaGame game = new XnaGame())
            {
                game.Run();
            }
        }
    }
#endif
}

