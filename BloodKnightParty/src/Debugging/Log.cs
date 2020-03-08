using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodKnightParty.src.Debugging
{
    public class Log
    {

        public static Log Default = new Log();

        private Action<string> _outputAction;

        private Log() { }

        public void ConfigureOutput(Action<string> outputAction)
        {
            _outputAction = outputAction;
        }

        public void WriteLine(string message)
        {
            _outputAction?.Invoke(message);
        }

    }
}
