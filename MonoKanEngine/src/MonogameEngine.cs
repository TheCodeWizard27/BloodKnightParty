using KantanEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoKanEngine.src
{
    public class MonogameEngine : Game, IKanGameConfiguration
    {
        private KanGameEngine _engine;
        private IKanGameConfiguration _configuration;

        public GraphicsDeviceManager GraphicsDeviceManager { get; set; }

        public MonogameEngine(IKanGameConfiguration configuration)
        {
            Initialize();
            _configuration = configuration;
            
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _engine = new KanGameEngine(this);
        }

        protected override void Update(GameTime gameTime) => _engine?.Update(gameTime.ElapsedGameTime);
        protected override void Draw(GameTime gameTime) => _engine?.Draw(gameTime.ElapsedGameTime);

        private new void Initialize()
        {
            IsMouseVisible = false;
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            GraphicsDeviceManager.PreferredBackBufferHeight = 760;
            GraphicsDeviceManager.PreferredBackBufferWidth = 1080;
            
            //GraphicsDeviceManager.ToggleFullScreen();
            Content.RootDirectory = "Packages";
        }

        public void Configure(KanGameEngine engine, KanEngineContextBuilder builder)
        {
            builder.SetServiceProvider(new MonogameServiceProvider(this));
            builder.Services.AddService<Game>(this);
            builder.Services.AddService(GraphicsDeviceManager);
            builder.Services.AddService(new KanContentManager(builder.Services));
            _configuration.Configure(engine, builder); // Forward configuration to final implementation
        }

        public KanGameController Startup(KanEngineContext context)
        {
            return _configuration.Startup(context); // Forward configuration to final implementation
        }

    }
}