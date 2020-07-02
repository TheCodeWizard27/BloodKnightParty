using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodKnightParty.src.IO
{
    public class ControllerHandler
    {

        private 

        public int MaxControllers { get; protected set; } = 4;

        public ControllerHandler()
        {
            GamePad.GetState(PlayerIndex.One);
        }

        #region Public Methods

        #endregion

        #region Private Methods

        private void UpdateBufferedStates()
        {
            GamePad.GetState()
        }

        #endregion

    }
}
