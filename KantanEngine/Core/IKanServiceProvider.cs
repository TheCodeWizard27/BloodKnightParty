using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public interface IKanServiceProvider : IServiceProvider
    {

        void AddService(Type type, object provider);
        void AddService(Type type);
        void AddService<T>(Type type);
        void AddService<T>(T provider) where T : class;
        void AddService<T, TProvided>(TProvided provider) where TProvided : class;
        
        T GetService<T>() where T : class;
        void RemoveService(Type type);

    }
}
