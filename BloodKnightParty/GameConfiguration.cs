using BloodKnightParty.src.Core;
using KantanEngine.Core;
using KantanEngine.IO;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodKnightParty
{
    public class GameConfiguration : IKanGameConfiguration
    {
        public void Configure(KanGameEngine engine, KanEngineContextBuilder builder)
        {
            var inputHandler = new InputHandler();
            inputHandler.StartListening();
            builder.Services.AddService(inputHandler);
            //builder.Services.AddService(new TestContentLoader(builder.Services));
            //builder.Services.AddService(new ContentManager(builder.Services));
            //builder.Services.AddService(new Loader());
        }

        public KanGameController Startup(KanEngineContext context)
        {
            return new GraphicsTestController(context);
        }
    }
}
