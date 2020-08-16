using KantanEngine.Debugging;
using KantanEngine.IO;
using Microsoft.Xna.Framework.Input;
using MonoKanEngine.src.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BloodKnightParty.src.Core
{

    #region Keyboard
    public class Keyboard
    {

        private KeyboardHandler _keyboardHandler;

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

        internal void InitKeyboardHandlers()
        {
            _keyboardHandler = new KeyboardHandler();

            _keyboardHandler.OnKeyDown += (key) => OnKeyDown?.Invoke(key);
            _keyboardHandler.OnKeyUp += (key) => OnKeyUp?.Invoke(key);
            _keyboardHandler.OnKeyPressed += (key) => OnKeyPressed?.Invoke(key);
            _keyboardHandler.OnKeyClicked += (key) => OnKeyClicked?.Invoke(key);
            _keyboardHandler.OnKeyReleased += (key) => OnKeyReleased?.Invoke(key);
        }

        internal void PollKeyboard()
        {
            try
            {
                _keyboardHandler.UpdateKeyboardState(Microsoft.Xna.Framework.Input.Keyboard.GetState());
            }
            catch
            {
                Log.Default.WriteLine("KeyboardHandler Caught Exception");
            }
        }
    }
    #endregion

    #region GamePads
    public class GamePads
    {

        #region Events

        public delegate void OnButtonDownHandler(GamePadHandler handler, Buttons button, float value);
        public event OnButtonDownHandler OnButtonDown;

        public delegate void OnButtonUpHandler(GamePadHandler handler, Buttons button, float value);
        public event OnButtonUpHandler OnButtonUp;

        public delegate void OnButtonPressedHandler(GamePadHandler handler, Buttons button, float value);
        public event OnButtonDownHandler OnButtonPressed;

        public delegate void OnButtonClickedHandler(GamePadHandler handler, Buttons button, float value);
        public event OnButtonDownHandler OnButtonClicked;

        public delegate void OnButtonReleasedHandler(GamePadHandler handler, Buttons button, float value);
        public event OnButtonDownHandler OnButtonReleased;

        public delegate void OnConnectionHandler(GamePadHandler handler);
        public event OnConnectionHandler OnConnection;

        public delegate void OnDisconnectedHandler(GamePadHandler handler);
        public event OnDisconnectedHandler OnDisconnected;

        #endregion

        public int MaxGamePads { get; private set; } = 4;

        protected GamePadHandler[] _gamePadHandlers;

        public GamePadHandler GetGamePad(int id) => _gamePadHandlers[id];

        internal void PollGamepads()
        {
            try
            {
                for (var i = 0; i < MaxGamePads; i++)
                {
                    _gamePadHandlers[i].UpdateGamePadState(GamePad.GetState(i));
                }
            }
            catch
            {
                Log.Default.WriteLine("GamepadHandler Caught Exception");
            }

        }

        internal void InitGamePadHandlers()
        {
            _gamePadHandlers = new GamePadHandler[MaxGamePads];
            for (int i = 0; i < MaxGamePads; i++)
            {
                var handle = _gamePadHandlers[i] = new GamePadHandler(i);

                // Redirect Events.
                handle.OnConnection += () => OnConnection?.Invoke(handle);
                handle.OnDisconnect += () => OnDisconnected?.Invoke(handle);

                handle.OnButtonDown += (button, value) => OnButtonDown?.Invoke(handle, button, value);
                handle.OnButtonUp += (button, value) => OnButtonUp?.Invoke(handle, button, value);
                handle.OnButtonPressed += (button, value) => OnButtonPressed?.Invoke(handle, button, value);
                handle.OnButtonClicked += (button, value) => OnButtonClicked?.Invoke(handle, button, value);
                handle.OnButtonReleased += (button, value) => OnButtonReleased?.Invoke(handle, button, value);
            }
        }

    }
    #endregion

    public class InputHandler
    {

        protected CancellationTokenSource _cancelToken;
        
        public int Interval { get; private set; } = 1000 / 60;
        public bool GamePadPollingEnabled { get; set; } = true;

        public GamePads GamePads { get; private set; }
        public Keyboard Keyboard { get; private set; }

        public InputHandler()
        {
            GamePads = new GamePads();
            Keyboard = new Keyboard();
        }


        #region Public Methods

        public void StartListening()
        {
            _cancelToken = new CancellationTokenSource();
            var tmpToken = _cancelToken.Token;

            Task.Factory.StartNew(() =>
            {
                GamePads.InitGamePadHandlers();
                Keyboard.InitKeyboardHandlers();

                do
                {
                    if(GamePadPollingEnabled) GamePads.PollGamepads();
                    Keyboard.PollKeyboard();
                    Thread.Sleep(Interval);
                } while (!tmpToken.IsCancellationRequested);
            }, tmpToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            Log.Default.WriteLine("InputHandler Attached");
        }

        public void StopListening() => _cancelToken.Cancel();

        #endregion

    }
}
