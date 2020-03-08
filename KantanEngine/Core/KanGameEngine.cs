using System;

namespace KantanEngine.Core
{
    public class KanGameEngine
    {

        #region Properties

        public KanGameController CurrentController { get; private set; }
        public KanEngineContext Context { get; private set; }

        #endregion

        public KanGameEngine(KanEngineContext context)
        {
            Context = context;
        }

        #region Public Methods

        public void ChangeController(KanGameController controller)
        {
            CurrentController.Dispose();
            CurrentController = controller;
            controller.Initialize();
        }

        public void Update(TimeSpan delta)
        {
            CurrentController.Update(delta, Context);
        }

        public void Draw(TimeSpan delta)
        {
            CurrentController.Draw(delta, Context);
        }

        #endregion

    }
}
