using KantanEngine.Core;
using KantanEngine.Debugging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoKanEngine.src;
using MonoKanEngine.src.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodKnightParty.src.Core
{
    public class GraphicsTestController : KanGameController
    {
        private HashSet<Keys> _bufferedInput = new HashSet<Keys>();

        private Effect _effect;

        private Vector3 _cameraPos = new Vector3(0, 40, 20);
        private Vector3 _cameraRot = new Vector3(0, 0, 0);
        private double _maxLookUp = 89 * Math.PI / 180;
        private double _maxLookDown = -89 * Math.PI / 180;

        private int _speed = 10;

        private float _cameraSensitivity = .5f;

        private Vector2 _middleOfScreen;

        private Model _model;

        public GraphicsTestController(KanEngineContext context) : base(context)
        {
        }

        public override void OnDraw()
        {
            Context.RunService<GraphicsDeviceManager>(gdm =>
            {
                gdm.GraphicsDevice.Clear(Color.Black);

                _cameraRot.Y = (float) Math.Min(Math.Max(_cameraRot.Y, _maxLookDown), _maxLookUp);

                var cameraPosition = _cameraPos;
                var cameraRotation = Matrix.CreateFromYawPitchRoll(_cameraRot.X, _cameraRot.Y, 0);
                var cameraLookAtVector = _cameraPos + Vector3.Transform( Vector3.Forward, cameraRotation);
                var cameraUpVector = Vector3.Up;

                _effect.Parameters["World"].SetValue(Matrix.CreateTranslation(0, 0, 0));
                _effect.Parameters["View"].SetValue(Matrix.CreateLookAt(
                    cameraPosition, cameraLookAtVector, cameraUpVector));

                float aspectRatio =
                    gdm.PreferredBackBufferWidth / (float)gdm.PreferredBackBufferHeight;
                float fieldOfView = MathHelper.PiOver4;
                float nearClipPlane = 1f;
                float farClipPlane = 100000;

                //_effect.TextureEnabled = true;
                _effect.Parameters["Projection"].SetValue(Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView, aspectRatio, nearClipPlane, farClipPlane));
                
                gdm.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
                //gdm.GraphicsDevice.SamplerStates[0].MinFilter = TextureFilter.Anisotropic;
                //gdm.GraphicsDevice.SamplerStates[0].MagFilter = TextureFilter.Anisotropic;
                //gdm.GraphicsDevice.SamplerStates[0].MipFilter = TextureFilter.Linear;
                //gdm.GraphicsDevice.SamplerStates[0].MaxAnisotropy = 16;

                
                if (Context.GetService<Game>().IsActive)
                {
                    var movement = _middleOfScreen - (Mouse.GetState().Position.ToVector2());

                    var delta = Context.TimeDelta.Milliseconds / 16;
                    //Log.Default.Write($"Miliseconds {Context.TimeDelta.Milliseconds}");

                    _cameraRot.X += (movement.X * _cameraSensitivity *delta) * ((float)Math.PI / 180);
                    _cameraRot.Y += (movement.Y * _cameraSensitivity *delta) * ((float)Math.PI / 180);

                    //Log.Default.Write($"CameraRot X {_cameraRot.X} , Y {_cameraRot.Y}");

                    Mouse.SetPosition((int)_middleOfScreen.X, (int)_middleOfScreen.Y);
                }

                foreach (var pass in _effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    foreach (var mesh in _model.Meshes)
                    {
                        /*
                        foreach(var part in mesh.MeshParts)
                        {
                            
                        }
                        */
                        /*
                        foreach(BasicEffect effect in mesh.Effects)
                        {
                            effect.EnableDefaultLighting();
                            effect.View = _effect.View;
                            effect.Projection = _effect.Projection;
                            effect.Alpha = 1;
                        }
                        */

                        mesh.Draw();
                    }
                }
            });
        }

        public override void OnInitialize()
        {
            var loader = Context.GetService<KanContentManager>();
            loader.Loader.AddPackageToQueue("Packages/test.kco");
            loader.Loader.LoadAsync().Wait();

            _model = loader.Load<Model>("Map");

            var gdm = Context.GetService<GraphicsDeviceManager>();
            _effect = loader.Load<Effect>("Shaders/test");
            //_effect = new BasicEffect(gdm.GraphicsDevice);
            //_effect.EnableDefaultLighting();
            //_effect.TextureEnabled = true;
            //_effect.LightingEnabled = true;
            //_effect.DirectionalLight0.Enabled = true;
            //_effect.DirectionalLight0.DiffuseColor = Color.Red.ToVector3();
            //_effect.DirectionalLight0.Direction = Vector3.Normalize(new Vector3(0, -1.5f, -1));
            //_effect.DirectionalLight0.SpecularColor = Color.White.ToVector3();
            //_effect.FogEnabled = false;

            foreach(var model in _model.Meshes)
            {
                foreach(var part in model.MeshParts)
                {
                    part.Effect = _effect;
                }
            }

            //gdm.GraphicsDevice.Viewport = new Viewport(0, 0, 165, 124);

            var viewPort = Context.GetService<Game>().GraphicsDevice.Viewport;
            _middleOfScreen = new Vector2(viewPort.Width/2, viewPort.Height/2);

            Context.RunService<InputHandler>(input =>
            {
                input.Keyboard.OnKeyDown += Keyboard_OnKeyDown;
                input.Keyboard.OnKeyUp += Keyboard_OnKeyUp;
            });
        }

        private void Keyboard_OnKeyUp(Keys key) => _bufferedInput.Add(key);

        private void Keyboard_OnKeyDown(Keys key) => _bufferedInput.Remove(key);

        public override void OnUpdate()
        {
            var cameraRotation = Matrix.CreateFromYawPitchRoll(_cameraRot.X, 0, 0);
            var direction = new Vector3();

            _bufferedInput.ToList().ForEach(key =>
            {

                switch (key)
                {
                    case Keys.Up:
                        _speed += 1;
                        break;
                    case Keys.Down:
                        _speed -= 1;
                        break;

                    case Keys.W:
                        direction.Z += _speed;
                        break;
                    case Keys.S:
                        direction.Z -= _speed;
                        break;
                    case Keys.A:
                        direction.X += _speed;
                        break;
                    case Keys.D:
                        direction.X -= _speed;
                        break;
                    case Keys.Space:
                        direction.Y -= _speed;
                        break;
                    case Keys.LeftShift:
                        direction.Y += _speed;
                        break;
                }
            });

            var moveTo = Vector3.Transform(direction, cameraRotation);
            _cameraPos += moveTo;
        }

        public override void OnUnload()
        {
            
        }
    }
}
