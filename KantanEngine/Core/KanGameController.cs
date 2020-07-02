using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public abstract class KanGameController
    {

        protected KanEngineContext Context { get; private set; }

        public KanGameController(KanEngineContext context)
        {
            Context = context;
        }

        #region Public Methods

        public abstract void Initialize();

        public abstract void Update();

        public abstract void Draw();

        public abstract void Unload();

        #endregion

    }
}
