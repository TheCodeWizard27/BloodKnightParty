using BloodKnightParty.src.Core;
using KantanEngine.Debugging;
using MonoKanEngine.src;
using System;

namespace BloodKnightParty
{
    /// <summary>
    /// The main class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Default.OnLogEvent += (msg) => Console.WriteLine($"[{DateTime.Now:mm:ss:fff}] {msg}");

            using (var game = new MonogameEngine(new GameConfiguration()))
                game.Run();            
        }

    }
}
