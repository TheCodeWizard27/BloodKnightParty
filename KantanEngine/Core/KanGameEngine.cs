using System;

namespace KantanEngine.Core
{
    public class KanGameEngine
    {

        #region Properties

        public KanGameController CurrentController { get; private set; }
        public KanEngineContext Context { get; private set; }

        #endregion

        public KanGameEngine(IKanGameConfiguration configuration)
        {
            Configure(configuration);
        }

        #region Public Methods

        public void ChangeController(KanGameController controller)
        {
            Context.ClearLocal();
            CurrentController?.Unload();
            CurrentController = controller;
            controller.Initialize();
        }

        public void Update(TimeSpan delta)
        {
            Context.TimeDelta = delta;
            CurrentController?.Update();
        }

        public void Draw(TimeSpan delta)
        {
            Context.TimeDelta = delta;
            CurrentController?.Draw();
        }

        #endregion

        #region Private Methods

        private void Configure(IKanGameConfiguration configuration)
        {
            var contextBuilder = new KanEngineContextBuilder();
            contextBuilder.SetControllerSwitchAction(ChangeController);
            configuration?.Configure(this, contextBuilder);
            Context = contextBuilder.Build();
        }

        #endregion

    }
}
