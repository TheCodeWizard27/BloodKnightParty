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
    public class InputHandler
    {

        protected CancellationTokenSource _cancelToken;
        
        protected GamePadHandler[] _gamePadHandlers;

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

        public delegate void OnConnectionHandler (GamePadHandler handler);
        public event OnConnectionHandler OnConnection;

        public delegate void OnDisconnectedHandler(GamePadHandler handler);
        public event OnDisconnectedHandler OnDisconnected;

        #endregion

        public int MaxGamePads { get; private set; } = 4;
        public int Interval { get; private set; } = 1000 / 60;
        public bool GamePadPollingEnabled { get; set; } = true;

        public InputHandler()
        {
            _gamePadHandlers = new GamePadHandler[MaxGamePads];
        }


        #region Public Methods

        public void StartListening()
        {
            _cancelToken = new CancellationTokenSource();
            var tmpToken = _cancelToken.Token;

            Task.Factory.StartNew(() =>
            {
                InitGamePadHandlers();
                do
                {
                    if(GamePadPollingEnabled) PollGamepads();

                    Thread.Sleep(Interval);
                } while (!tmpToken.IsCancellationRequested);
            }, tmpToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            Log.Default.WriteLine("InputHandler Attached");
        }

        public void StopListening() => _cancelToken.Cancel();

        public GamePadHandler GetGamePad(int id) => _gamePadHandlers[id];

        #endregion

        #region Private Methods

        private void InitGamePadHandlers()
        {
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

        private void PollGamepads()
        {
            try
            {
                for (var i = 0; i < MaxGamePads; i++)
                {
                    _gamePadHandlers[i].UpdateGamePadState(GamePad.GetState(i));
                }
            } catch
            {
                Log.Default.WriteLine("GamepadHandler Caught Exception");
            }

        }

        #endregion

    }
}
