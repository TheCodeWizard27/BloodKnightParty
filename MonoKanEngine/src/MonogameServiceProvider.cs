using KantanEngine.Core;
using Microsoft.Xna.Framework;
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

        public void AddService<T>(T service) where T : class => _game.Services.AddService(service);

        public T GetService<T>() where T : class => _game.Services.GetService<T>();

    }
}
