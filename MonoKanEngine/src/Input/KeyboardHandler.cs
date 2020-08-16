using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoKanEngine.src.Input
{
    public class KeyboardHandler
    {

        protected KeyboardState _bufferedKeyboardState;
        protected KeyboardState _currentKeyboardState;

        #region Events

        public delegate void OnKeyDownHandler(Keys key);
        public event OnKeyDownHandler OnKeyDown;

        public delegate void OnKeyUpHandler(Keys key);
        public event OnKeyUpHandler OnKeyUp;

        public delegate void OnKeyPressedHandler(Keys key);
        public event OnKeyDownHandler OnKeyPressed;

        public delegate void OnKeyClickedHandler(Keys key);
        public event OnKeyDownHandler OnKeyClicked;

        public delegate void OnKeyReleasedHandler(Keys key);
        public event OnKeyDownHandler OnKeyReleased;

        #endregion

        public bool IsKeyDown(Keys key)
            => _currentKeyboardState.IsKeyDown(key);
        public bool IsKeyUp(Keys key)
            => _currentKeyboardState.IsKeyUp(key);
        public bool IsKeyPressed(Keys key)
            => _bufferedKeyboardState.IsKeyDown(key) && _currentKeyboardState.IsKeyDown(key);
        public bool IsKeyClicked(Keys key)
            => _bufferedKeyboardState.IsKeyUp(key) && _currentKeyboardState.IsKeyDown(key);
        public bool IsKeyReleased(Keys key)
            => _bufferedKeyboardState.IsKeyDown(key) && _currentKeyboardState.IsKeyUp(key);


        public void UpdateKeyboardState(KeyboardState keyboardState)
        {
            _bufferedKeyboardState = _currentKeyboardState;
            _currentKeyboardState = keyboardState;
            InvokeEvents();
        }

        private void InvokeEvents()
        {
            foreach (var key in Enum.GetValues(typeof(Keys)).Cast<Keys>())
            {
                if (IsKeyDown(key))
                {
                    OnKeyDown?.Invoke(key);
                    if (IsKeyPressed(key)) OnKeyPressed?.Invoke(key);
                    if (IsKeyClicked(key)) OnKeyClicked?.Invoke(key);
                }
                else
                {
                    OnKeyUp?.Invoke(key);
                    if (IsKeyReleased(key)) OnKeyReleased?.Invoke(key);
                }
            }
        }

    }
}
