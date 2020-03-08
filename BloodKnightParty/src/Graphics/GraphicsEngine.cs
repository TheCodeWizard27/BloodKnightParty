using KantanEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodKnightParty.Core
{
    public class GraphicsEngine : KanGraphicsEngine
    {

        #region Fields

        private readonly List<KanSprite> _sprites = new List<KanSprite>();
        private Game _game;

        #endregion

        public GraphicsEngine(Game game)
        {
            _game = game;
        }

        #region Public Methods


        public override void LoadTexture(string name, Stream stream)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods



        #endregion

    }
}
