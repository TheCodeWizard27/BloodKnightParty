using KantanEngine.Core;
using KantanEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public class KanContextBuilder
    {

        private KanEngineContext _instance;

        public KanContextBuilder()
        {
            _instance = new KanEngineContext();
        }

        public KanContextBuilder SetGraphicsEngine(KanGraphicsEngine graphicsEngine)
        {
            _instance.Graphics = graphicsEngine;
            return this;
        }

        public KanEngineContext Build() => _instance;

    }
}
