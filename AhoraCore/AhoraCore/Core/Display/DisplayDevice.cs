using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Input;
using AhoraCore.Core.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;



namespace AhoraProject.Ahora.Core.Display
{
    public class DisplayDevice: GameWindow
    {
       // public GeometryStorrageManager Scene { get; private set; }

        public AShader staticShader{ get; private set; }

        public DisplayDevice(int w, int h):base(w,h)
        {
         ///   Scene = new GeometryStorrageManager();
            staticShader = new DefaultShader();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GeometryStorrageManager.Data.BeforeRender();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            KeysInput.UpdateKeysStatment();
            MouseInput.UpdateMouseStatment();
           // CameraInstance.Get().UpdateCamera();
        }

        private void cleanUpFrame()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.ClearColor(0.5f, 0.5f, 0.5f, 1);

            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            cleanUpFrame();
            staticShader.Bind();
            staticShader.UpdateUniforms();
          ///  MaterialStorrage.Materials.GetItem("DefaultMaterial").Bind(staticShader);
            GeometryStorrageManager.Data.Render();
            this.SwapBuffers();
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            //GeometryStorrageManager.Data.PostRender();
            //GeometryStorrageManager.Data.ClearManager();
            staticShader.Delete();
        }

    }
}
