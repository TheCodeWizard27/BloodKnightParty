using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.IO
{
    public class GamepadEventArgs
    {

        public int GamePadId { get; private set; }

        public GamepadEventArgs(int id) => GamePadId = id;

    }

    public class GamepadConnectionEventArgs : GamepadEventArgs
    {
        public GamepadConnectionEventArgs(int id) : base(id) { }
    }

    public enum Button_State
    {
        IsDown,
        IsUp,
        IsPressed,
        IsReleased
    }

    public class GamepadButtonEventArgs : GamepadEventArgs
    {

        public Button_State State { get; private set; }
        public GamePadButton Button { get; private set; }

        public GamepadButtonEventArgs(int id, Button_State state, GamePadButton button) : base(id) 
        {
            Button = button;
            State = state;
        }

        // Quick Methods to simplify checking Button States.

        public bool IsDown(GamePadButton button) => Button == button && State == Button_State.IsDown;
        public bool IsUp(GamePadButton button) => Button == button && State == Button_State.IsUp;
        public bool IsPressed(GamePadButton button) => Button == button && State == Button_State.IsPressed;
        public bool IsReleased(GamePadButton button) => Button == button && State == Button_State.IsReleased;

    }

}
