using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.CES;
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
         public DisplayDevice(int w, int h):base(w,h)
        {
 
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GeometryStorageManager.Data.BeforeRender();
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
            GameEntityStorrage.Entities.Update(GameEntityStorrage.Entities.RootID);
            GameEntityStorrage.Entities.Render(GameEntityStorrage.Entities.RootID);
            this.SwapBuffers();
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
 
        }

    }
}
