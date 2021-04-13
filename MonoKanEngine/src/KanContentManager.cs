using KantanEngine.IO;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoKanEngine.src
{
    public class KanContentManager : ContentManager
    {

        public KantanLoader Loader { get; private set; }

        public KanContentManager(IServiceProvider serviceProvider) : base(serviceProvider, "Packages")
        {
            Loader = new KantanLoader();
        }

        protected override Stream OpenStream(string assetName) => Loader.GetLoadedRessource(assetName);
    }
}
