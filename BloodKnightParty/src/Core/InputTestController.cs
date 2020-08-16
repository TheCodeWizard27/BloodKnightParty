using KantanEngine.Core;
using KantanEngine.Debugging;
using Microsoft.Xna.Framework.Input;
using MonoKanEngine.src.Input;
using System.Collections.Generic;
using System.Linq;

namespace BloodKnightParty.src.Core
{
    public class InputTestController : KanGameController
    {

        //private HashSet<Buttons> _bufferedInput = new HashSet<Buttons>();
        private HashSet<Keys> _bufferedInput = new HashSet<Keys>();

        public InputTestController(KanEngineContext context): base(context)
        {

        }

        public override void Draw()
        {
            //throw new System.NotImplementedException();
        }

        public override void Initialize()
        {

            Context.RunService<InputHandler>(input =>
            {
                input.Keyboard.OnKeyDown += KeyDown;
                input.Keyboard.OnKeyUp += KeyUp;

                //input.GamePads.OnConnection += Connected;
                //input.GamePads.OnDisconnected += Disconnected;
                //input.GamePads.OnButtonDown += ButtonDown;
                //input.GamePads.OnButtonUp += ButtonUp;
            });
        }

        public override void Unload()
        {

            Context.RunService<InputHandler>(input =>
            {
                input.Keyboard.OnKeyDown -= KeyDown;
                input.Keyboard.OnKeyUp -= KeyUp;

                //input.GamePads.OnConnection -= Connected;
                //input.GamePads.OnDisconnected -= Disconnected;
                //input.GamePads.OnButtonDown -= ButtonDown;
                //input.GamePads.OnButtonUp -= ButtonUp;
            });
        }

        public override void Update()
        {
            var test = "[";
            foreach(var button in _bufferedInput.ToList())
            {
                test += $"{button.ToString()},";
            }

            Log.Default.WriteLine($"{test}]");

        }

        public void KeyDown(Keys key) => _bufferedInput.Add(key);
        public void KeyUp(Keys key) => _bufferedInput.Remove(key);

        //public void ButtonDown(GamePadHandler gamepad, Buttons button, float value) => _bufferedInput.Add(button);
        //public void ButtonUp(GamePadHandler gamepad, Buttons button, float value) => _bufferedInput.Remove(button);

        private void Connected(GamePadHandler handler)
        {
            Log.Default.WriteLine("Connection Made");
        }

        private void Disconnected(GamePadHandler handler)
        {
            Log.Default.WriteLine("Disconnected");
        }
    }
}
