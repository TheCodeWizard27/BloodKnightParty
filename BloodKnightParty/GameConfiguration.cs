using BloodKnightParty.src.Core;
using KantanEngine.Core;
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
            
        }

        public KanGameController Startup(KanEngineContext context)
        {
            return new TestController(context);
        }
    }
}
