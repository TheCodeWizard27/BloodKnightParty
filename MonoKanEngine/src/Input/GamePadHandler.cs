using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoKanEngine.src.Input
{

    public class GamePadHandler
    {

        public static int X_THRESHOLD = 10;
        public static int Y_THRESHOLD = 10;

        protected GamePadState _bufferedGamePadState;
        protected GamePadState _currentGamePadState;

        public bool IsConnected { get => _currentGamePadState.IsConnected; }

        public int Id { get; private set; }

        #region Events

        public delegate void OnButtonDownHandler(Buttons button, float value);
        public event OnButtonDownHandler OnButtonDown;

        public delegate void OnButtonUpHandler(Buttons button, float value);
        public event OnButtonUpHandler OnButtonUp;

        public delegate void OnButtonPressedHandler(Buttons button, float value);
        public event OnButtonDownHandler OnButtonPressed;

        public delegate void OnButtonClickedHandler(Buttons button, float value);
        public event OnButtonDownHandler OnButtonClicked;

        public delegate void OnButtonReleasedHandler(Buttons button, float value);
        public event OnButtonDownHandler OnButtonReleased;

        public delegate void OnConnectionHandler();
        public event OnConnectionHandler OnConnection;

        public delegate void OnDisconnectHandler();
        public event OnDisconnectHandler OnDisconnect;

        #endregion

        public GamePadHandler(int id)
        {
            Id = id;
        }

        #region Public Methods

        public bool IsButtonDown(Buttons button) 
            => _currentGamePadState.IsButtonDown(button);
        public bool IsButtonUp(Buttons button) 
            => _currentGamePadState.IsButtonUp(button);
        public bool IsButtonPressed(Buttons button) 
            => _bufferedGamePadState.IsButtonDown(button) && _currentGamePadState.IsButtonDown(button);
        public bool IsButtonClicked(Buttons button)
            => _bufferedGamePadState.IsButtonUp(button) && _currentGamePadState.IsButtonDown(button);
        public bool IsButtonReleased(Buttons button)
            => _bufferedGamePadState.IsButtonDown(button) && _currentGamePadState.IsButtonUp(button);

        public Vector2 GetLeftThumbStick() => _currentGamePadState.ThumbSticks.Left;
        public Vector2 GetRightThumbStick() => _currentGamePadState.ThumbSticks.Left;

        public void UpdateGamePadState(GamePadState gamePadState)
        {
            _bufferedGamePadState = _currentGamePadState;
            _currentGamePadState = gamePadState;
            InvokeEvents();
        }

        public void InvokeEvents()
        {
            
            if(!_bufferedGamePadState.IsConnected)
            {
                if(!IsConnected) return;
                OnConnection();
            }else if(!IsConnected)
            {
                OnDisconnect();
                return;
            }

            InvokeButtonEvents();
        }

        #endregion

        private void InvokeButtonEvents()
        {
            foreach (var button in Enum.GetValues(typeof(Buttons)).Cast<Buttons>())
            {
                if (IsButtonDown(button))
                {
                    OnButtonDown?.Invoke(button, 1);
                    if (IsButtonPressed(button)) OnButtonPressed?.Invoke(button, 1);
                    if (IsButtonClicked(button)) OnButtonClicked?.Invoke(button, 1);
                }
                else
                {
                    OnButtonUp?.Invoke(button, 1);
                    if (IsButtonReleased(button)) OnButtonReleased?.Invoke(button, 1);
                }
            }
        }

    }
}
