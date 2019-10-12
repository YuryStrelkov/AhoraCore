using AhoraCore.Core.CES;

using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Rendering
{
    public class ForwardRenderer : RederPipeline
    {
        public void AfterRender()
        {

        }

        public void BeforeRender()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.ClearColor(0.5f, 0.5f, 0.5f, 1);

            GL.Enable(EnableCap.DepthTest);
        }

        public void Render()
        {
            BeforeRender();

            GameEntityStorrage.Entities.Render(GameEntityStorrage.Entities.RootID);

            AfterRender();
        }
    }
}
