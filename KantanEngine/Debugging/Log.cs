using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Debugging
{
    public class Log
    {

        public static Log Default = new Log();

        public delegate void OnLogEventHandler(string message);
        public event OnLogEventHandler OnLogEvent;

        private Log() { }

        public void WriteLine(string message)
        {
            OnLogEvent?.Invoke(message);
        }

    }
}
