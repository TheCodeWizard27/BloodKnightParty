using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public class KanGameController
    {

        public KanGameEngine Engine { get; private set; }

        public KanGameController(KanGameEngine engine)
        {
            Engine = engine;
        }

        #region Public Methods

        public void Initialize()
        {

        }

        public void Update(TimeSpan delta, KanEngineContext context)
        {

        }

        public void Draw(TimeSpan delta, KanEngineContext context)
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
