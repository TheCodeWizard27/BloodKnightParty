using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public interface IKanGameConfiguration
    {

        void Configure(KanGameEngine engine, KanEngineContextBuilder builder);

        KanGameController Startup(KanEngineContext context);

    }
}
