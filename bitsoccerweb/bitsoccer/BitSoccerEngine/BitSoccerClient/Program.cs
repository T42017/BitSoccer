﻿using System;
using GentleWare.CloudBall.Wolkenhondjes1;

namespace BitSoccerClient
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Common.Global.Random = new Random(0);

            using (var game = new BitSoccerClient(new Wolkenhond1(), new TeamTwo.TeamTwo()))
            {
                game.Run();
            }
        }
    }
#endif
}
