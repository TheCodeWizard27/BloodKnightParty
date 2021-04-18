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

        //private HashSet<Buttons> _bufferedInput = new HashSet<Buttons>();
        private HashSet<Keys> _bufferedInput = new HashSet<Keys>();

        private VertexPositionTexture[] _floorVerts;
        private BasicEffect _effect;
        private Vector3 _cameraPos = new Vector3(0, 40, 20);
        private float _rotation = 0;

        private float currY = 0;
        private float MaxY = 500;

        private Model _model;

        public GraphicsTestController(KanEngineContext context) : base(context)
        {
        }

        public override void OnDraw()
        {
            Context.RunService<GraphicsDeviceManager>(gdm =>
            {
                gdm.GraphicsDevice.Clear(Color.LightBlue);

                _cameraPos.Y = currY;

                var cameraPosition = _cameraPos;
                var cameraLookAtVector = _cameraPos + Vector3.Down;// Matrix.CreateRotationY(1.4f).Forward;
                var cameraUpVector = Vector3.UnitZ;

                _effect.View = Matrix.CreateLookAt(
                    cameraPosition, cameraLookAtVector, cameraUpVector);

                float aspectRatio =
                    gdm.PreferredBackBufferWidth / (float)gdm.PreferredBackBufferHeight;
                float fieldOfView = MathHelper.PiOver4;
                float nearClipPlane = 0.01f;
                float farClipPlane = 100000;

                _effect.TextureEnabled = true;
                _effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
                
                gdm.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
                //gdm.GraphicsDevice.SamplerStates[0].MinFilter = TextureFilter.Anisotropic;
                //gdm.GraphicsDevice.SamplerStates[0].MagFilter = TextureFilter.Anisotropic;
                //gdm.GraphicsDevice.SamplerStates[0].MipFilter = TextureFilter.Linear;
                //gdm.GraphicsDevice.SamplerStates[0].MaxAnisotropy = 16;


                foreach (var pass in _effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    foreach(var mesh in _model.Meshes)
                    {
                        foreach(BasicEffect effect in mesh.Effects)
                        {
                            effect.View = _effect.View;
                            effect.Projection = _effect.Projection;
                            effect.Alpha = 1;
                        }

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

            _model = loader.Load<Model>("test.xnb");

            var gdm = Context.GetService<GraphicsDeviceManager>();

            _floorVerts = new VertexPositionTexture[6];

            _floorVerts[0].Position = new Vector3(-20, -20, 0);
            _floorVerts[1].Position = new Vector3(-20, 20, 0);
            _floorVerts[2].Position = new Vector3(20, -20, 0);

            _floorVerts[3].Position = _floorVerts[1].Position;
            _floorVerts[4].Position = new Vector3(20, 20, 0);
            _floorVerts[5].Position = _floorVerts[2].Position;

            _effect = new BasicEffect(gdm.GraphicsDevice);
            
            _effect.FogEnabled = false;

            Context.RunService<InputHandler>(input =>
            {
                input.Keyboard.OnKeyDown += Keyboard_OnKeyDown;
                input.Keyboard.OnKeyUp += Keyboard_OnKeyUp;
            });
        }

        private void Keyboard_OnKeyUp(Keys key) => _bufferedInput.Add(key);

        private void Keyboard_OnKeyDown(Keys key) => _bufferedInput.Remove(key);

        public override void OnUnload()
        {
        }

        public override void OnUpdate()
        {
            _bufferedInput.ToList().ForEach(key =>
            {
                switch (key)
                {
                    case Keys.W:
                        _cameraPos.X += 1;
                        break;
                    case Keys.S:
                        _cameraPos.X -= 1;
                        break;
                    case Keys.A:
                        currY += 1;
                        break;
                    case Keys.D:
                        currY -= 1;
                        break;
                    case Keys.Space:
                        _cameraPos.Z += 1;
                        break;
                    case Keys.LeftShift:
                        _cameraPos.Z -= 1;
                        break;
                }
            });

            Log.Default.Write($"{{ x: {_cameraPos.X} y:{_cameraPos.Y} z:{_cameraPos.Z} }}");
        }
    }
}
