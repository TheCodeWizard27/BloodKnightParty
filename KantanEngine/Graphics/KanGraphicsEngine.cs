using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Graphics
{
    public abstract class KanGraphicsEngine
    {

        #region Fields

        private readonly List<KanSprite> _sprites = new List<KanSprite>();

        #endregion

        #region properties

        public Dictionary<string, KanTexture> BufferedTextures { get; private set; } = new Dictionary<string, KanTexture>();

        #endregion

        #region Public Methods

        public abstract void LoadTexture(string name, Stream stream);

        public void RegisterSprite(KanSprite sprite)
        {
            _sprites.Add(sprite);
        }

        public void UnregisterSprite(KanSprite sprite)
        {
            _sprites.Remove(sprite);
        }

        #endregion

        #region Private Methods



        #endregion

    }
}
