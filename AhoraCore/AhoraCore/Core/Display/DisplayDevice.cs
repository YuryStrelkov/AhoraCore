using AhoraCore.Core.Buffers;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;



namespace AhoraProject.Ahora.Core.Display
{
    public class DisplayDevice: GameWindow
    {
        public GeometryStorrageManager Scene { get; private set; }

        public StaticShader staticShader{ get; private set; }



        public DisplayDevice(int w, int h):base(w,h)
        {
            Scene = new GeometryStorrageManager();
            staticShader = new StaticShader();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Scene.BeforeRender();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        private void cleanUpFrame()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.ClearColor(0.1f, 0.1f, 0.1f, 1);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            cleanUpFrame();

            staticShader.Bind();
            Scene.Render();
            this.SwapBuffers();
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Scene.PostRender();
            Scene.ClearManager();
            staticShader.DeleteShader();
        }

    }
}
