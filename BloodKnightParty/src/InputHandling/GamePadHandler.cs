using BloodKnightParty.src.InputHandling;
using KantanEngine.Debugging;
using KantanEngine.IO;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BloodKnightParty.src.Core
{
    public class GamepadHandler
    {

        private CancellationTokenSource _cancelToken;
        private JoystickState[] _prevGamePadStates;

        #region Events

        public delegate void OnConnectionHandler (GamepadConnectionEventArgs args);
        public event OnConnectionHandler OnConnection;

        public delegate void OnDisconnectedHandler(GamepadConnectionEventArgs args);
        public event OnDisconnectedHandler OnDisconnected;

        public delegate void OnGamepadInputHandler(GamepadButtonEventArgs args);
        public event OnGamepadInputHandler OnInput;

        #endregion

        public int MaxGamePads { get; private set; } = 8;
        public int Interval { get; private set; } = 1000 / 60;

        public GamepadHandler()
        {
            _prevGamePadStates = new JoystickState[MaxGamePads];
        }

        public void StartListening()
        {
            _cancelToken = new CancellationTokenSource();
            var tmpToken = _cancelToken.Token;

            Task.Factory.StartNew(() =>
            {
                do
                {
                    PollGamepads();
                    Thread.Sleep(Interval);
                } while (!tmpToken.IsCancellationRequested);
            }, tmpToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void StopListening() => _cancelToken.Cancel();

        #region Private Methods

        private void PollGamepads()
        {
            var connectedString = "Connected Controllers : { ";
            
            // Savety net because sometimes GetState may throw an exception.
            // For performance reasons outside of for loop.
            /*
            try
            {
                for (var i = 0; i < MaxGamePads; i++)
                {
                    var tmpGamePad = Joystick.GetState(i);
                    var wasConnected = _prevGamePadStates[i] == null ? false : _prevGamePadStates[i].IsConnected;

                    if (tmpGamePad.IsConnected)
                    {
                        connectedString += $"{i} ";
                        if (!wasConnected)
                        {
                            OnConnection?.Invoke(new GamepadConnectionEventArgs(i));
                            Log.Default.WriteLine(JsonConvert.SerializeObject(tmpGamePad)); 
                            Log.Default.WriteLine(JsonConvert.SerializeObject(Joystick.GetCapabilities(i)));
                            Log.Default.WriteLine(JsonConvert.SerializeObject(GamePad.GetCapabilities(i)));
                        }
                        else
                        {
                            PollInputEvents(i, tmpGamePad);
                        }
                    }
                    else if (wasConnected)
                    {
                        OnDisconnected?.Invoke(new GamepadConnectionEventArgs(i));
                    }

                    _prevGamePadStates[i] = tmpGamePad;
                }
            } catch
            {
                // Ignore for now
            }
            //Log.Default.WriteLine(connectedString + "}");
            */
        }

        private void PollInputEvents(int id, JoystickState currentState)
        {
            
        }

        #endregion

    }
}
