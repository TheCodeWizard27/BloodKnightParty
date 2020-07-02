using KantanEngine.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public class KanEngineContext
    {

        internal IKanServiceProvider _serviceProvider;
        internal Action<KanGameController> _switchController;
        internal Dictionary<string, object> _localBuffer;

        #region Properties

        public TimeSpan TimeDelta { get; internal set; }

        #endregion

        internal KanEngineContext()
        {
        }

        #region Public Methods

        public void ClearLocal() => _localBuffer.Clear();
        public void SetLocal(string local, object value) => _localBuffer.Add(local, value);
        public bool LocalExists(string local) => _localBuffer.ContainsKey(local);

        public T GetLocal<T>(string local) => (T)_localBuffer[local];
        public T TryGetLocal<T>(string local, T defaultValue = default)
        {
            try { return TryGetLocal<T>(local); } catch { return defaultValue; }
        }

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
