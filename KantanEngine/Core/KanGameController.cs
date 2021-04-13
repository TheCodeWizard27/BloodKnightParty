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

        public abstract void OnInitialize();

        public void Update()
        {
            Context.ObjectTracker.UpdateTracked();
        }
        public abstract void OnUpdate();

        public abstract void OnDraw();

        public void Unload()
        {
            Context.ObjectTracker.Clear();
        }
        public abstract void OnUnload();

        #endregion

    }
}
