using AhoraCore.Core.CES;
using AhoraCore.Core.Context;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Input;
using AhoraCore.Core.Rendering;
using OpenTK;
using System;



namespace AhoraProject.Ahora.Core.Display
{
    public class DisplayDevice: GameWindow
    {

        private RederPipeline renderer;

         public DisplayDevice(int w, int h):base(w,h)
        {
            if (MainContext.GetRenderMethod() == RenderMethods.Deffered)
            {
                renderer = new DefferedRenderer();
            }
            else
            {
                renderer = new ForwardRenderer();
            }
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
            GameEntityStorrage.Entities.Update(GameEntityStorrage.Entities.RootID);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            
            base.OnRenderFrame(e);
            renderer.Render();  
            SwapBuffers();
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
 
        }

    }
}
