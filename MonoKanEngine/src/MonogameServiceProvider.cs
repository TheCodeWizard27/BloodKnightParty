using KantanEngine.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoKanEngine.src
{
    public class MonogameServiceProvider : IKanServiceProvider
    {
        private Game _game;

        public MonogameServiceProvider(Game game)
        {
            _game = game;
        }

        public void AddService<T, TProvided>(TProvided service) where TProvided : class
        {
            _game.Services.AddService(typeof(T), service);
        }
        public void AddService<T>(T provider) where T : class
        {
            _game.Services.AddService(typeof(T), provider);
        }
        public void AddService<T>(Type type)
        {
            var instance = ActivatorUtilities.CreateInstance(_game.Services, type);
            _game.Services.AddService(typeof(T), instance);
        }
        public void AddService(Type type)
        {
            var instance = ActivatorUtilities.CreateInstance(_game.Services, type);
            _game.Services.AddService(type, instance);
        }

        public void AddService(Type type, object provider) => _game.Services.AddService(type, provider);

        public T GetService<T>() where T : class => _game.Services.GetService<T>();

        public object GetService(Type serviceType) => _game.Services.GetService(serviceType);

        public void RemoveService(Type type) => _game.Services.RemoveService(type);
    }
}