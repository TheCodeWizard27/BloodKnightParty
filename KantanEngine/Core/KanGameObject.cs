using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public abstract class KanGameObject
    {

        protected KanEngineContext Context { get; set; }

        public KanGameObject(KanEngineContext context, bool attachOnCreate = true)
        {
            Context = context;

            if (attachOnCreate) Attach();
        }

        public void Attach()
        {
            Context.ObjectTracker.Attach(this);
            Attached();
        }
        public void Detach()
        {
            Context.ObjectTracker.Detach(this);
            Detached();
        }

        public abstract void Update();
        protected abstract void Attached();
        protected abstract void Detached();

    }
}
