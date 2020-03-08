using KantanEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public class KanEngineContext
    {

        private Dictionary<string, object> _buffer = new Dictionary<string, object>();

        #region Properties

        public KanGraphicsEngine Graphics { get; internal set; }

        #endregion

        internal KanEngineContext()
        {

        }

        #region Public Methods

        public bool BufferContains(string key) => _buffer.ContainsKey(key);

        public void AddToBuffer(string key, object value, bool overrideIfExists = false)
        {
            if (overrideIfExists && BufferContains(key))
            {
                SetBufferValue(key, value);
                return;
            }
            _buffer.Add(key, value);
        }

        public void SetBufferValue(string key, object value, bool addIfNExists = false)
        {
            if (addIfNExists && !BufferContains(key))
            {
                SetBufferValue(key, value);
                return;
            }
            _buffer[key] = value;
        }

        public object GetBufferedValue(string key) => _buffer[key];
        public T GetBufferedValue<T>(string key) => (T)GetBufferedValue(key);

        public void ClearBuffer() => _buffer.Clear();

        #endregion

    }
}
