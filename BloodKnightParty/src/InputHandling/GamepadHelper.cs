using KantanEngine.IO;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodKnightParty.src.InputHandling
{
    public class GamepadHelper
    {

        public GamePadState State { get; private set; }
        public Dictionary<GamePadButton, bool> ButtonMap { get; private set; } = new Dictionary<GamePadButton, bool>();

        public void UpdateFromState(GamePadState state)
        {
            ButtonMap.Clear();

            // TODO find out a way to make this a bit more clean.
            ButtonMap.Add(GamePadButton.A, state.Buttons.A == ButtonState.Pressed);
            ButtonMap.Add(GamePadButton.X, state.Buttons.X == ButtonState.Pressed);
            ButtonMap.Add(GamePadButton.Y, state.Buttons.Y == ButtonState.Pressed);
            ButtonMap.Add(GamePadButton.B, state.Buttons.B == ButtonState.Pressed);
            ButtonMap.Add(GamePadButton.LB, state.Buttons.LeftStick == ButtonState.Pressed);
            ButtonMap.Add(GamePadButton.A, state.Buttons.A == ButtonState.Pressed);
            ButtonMap.Add(GamePadButton.A, state.Buttons.A == ButtonState.Pressed);
            ButtonMap.Add(GamePadButton.A, state.Buttons.A == ButtonState.Pressed);
        }

    }
}
