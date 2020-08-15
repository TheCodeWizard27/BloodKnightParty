using KantanEngine.Debugging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public class KanEngineContext
    {

        internal IKanServiceProvider _serviceProvider;
        internal Action<KanGameController> _switchController;
        internal ServiceContainer _local = new ServiceContainer();

        #region Properties

        public TimeSpan TimeDelta { get; internal set; }

        #endregion

        internal KanEngineContext()
        {
        }

        #region Public Methods

        public void ClearLocal()
        {
            _local = new ServiceContainer();
        }
        public void AddLocal<T>(T local) => _local.AddService(typeof(T), local);

        public T GetLocal<T>() => (T)_local.GetService(typeof(T));

        public T GetService<T>() where T : class => _serviceProvider.GetService<T>();
        public void RunService<T>(Action<T> action) where T : class => action.Invoke(GetService<T>());
        public void TryRunService<T>(Action<T> action) where T : class
        {
            try { RunService<T>(action); } catch { }
        }

        public void SwitchController(KanGameController controller) => _switchController?.Invoke(controller);

        #endregion

    }
}
