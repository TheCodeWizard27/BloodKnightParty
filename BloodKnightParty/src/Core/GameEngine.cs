using BloodKnightParty.Core;
using KantanEngine.Core;
using Microsoft.Xna.Framework;

namespace BloodKnightParty.src.Core
{
    public class GameEngine : KanGameEngine
    {

        private Game _handle;

        public GameEngine(Game handle, KanEngineContext context) : base(context)
        {
            _handle = handle;

            /*
            Graphics = new GraphicsEngine(_handle);
            
            var loader = new Loader();
            loader.OnLoad = (name, stream) =>
            {
                if (!name.StartsWith("texture.")) return;
                Graphics.LoadTexture(name, stream);
            };

            loader
                .AddPackageToQueue("Ressources/test.kco")
                .Load();
            */
        }


        #region Public Methods



        #endregion

    }
}
