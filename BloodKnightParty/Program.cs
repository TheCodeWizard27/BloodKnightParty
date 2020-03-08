using BloodKnightParty.Core;
using BloodKnightParty.src.Core;
using BloodKnightParty.src.Debugging;
using KantanEngine.Core;
using Microsoft.Xna.Framework;
using System;

namespace BloodKnightParty
{
    /// <summary>
    /// The main class.
    /// </summary>
    public class Program : Game
    {
        private GraphicsDeviceManager _graphics;
        private GameEngine _engine;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Default.ConfigureOutput((msg) => Console.WriteLine($"[{DateTime.Now:hh:mm:ss}] {msg}"));

            using (var program = new Program())
                program.Run();
        }

        public Program()
        {
            var context = new KanContextBuilder()
                .SetGraphicsEngine(new GraphicsEngine(this))
                .Build();

            _graphics = new GraphicsDeviceManager(this);
            _engine = new GameEngine(this, context);
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            _engine.Update(gameTime.ElapsedGameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _engine.Draw(gameTime.ElapsedGameTime);
            base.Draw(gameTime);
        }

    }
}
