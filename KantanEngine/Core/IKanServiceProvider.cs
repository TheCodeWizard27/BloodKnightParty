using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public interface IKanServiceProvider
    {

        T GetService<T>() where T : class;
        void AddService<T>(T service) where T : class;

    }
}
