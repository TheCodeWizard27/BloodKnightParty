using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public class KanEngineContextBuilder
    {

        private KanEngineContext _kanEngineContext;

        public IKanServiceProvider Services { get => _kanEngineContext._serviceProvider; }

        public KanEngineContextBuilder()
        {
            _kanEngineContext = new KanEngineContext();
        }

        public KanEngineContextBuilder SetServiceProvider(IKanServiceProvider serviceProvider)
        {
            _kanEngineContext._serviceProvider = serviceProvider;
            return this;
        }
        public KanEngineContextBuilder SetControllerSwitchAction(Action<KanGameController> controllerSwitch)
        {
            _kanEngineContext._switchController = controllerSwitch;
            return this;
        }

        public KanEngineContext Build()
        {
            if (_kanEngineContext._serviceProvider is null) throw new Exception("No Service Provider defined");

            return _kanEngineContext;
        }

    }
}
