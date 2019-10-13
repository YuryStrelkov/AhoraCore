using AhoraCore.Core.CES;
using AhoraCore.Core.Context;
using AhoraCore.Core.Rendering.RenderPasses;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Rendering
{
    public class ForwardRenderer : RederPipeline
    {
        public ForwardRenderer():base()
        {
            Passes.Add("GeometryPass", new GeometryRenderPass(this, MainContext.ScreenWidth, MainContext.ScreenHeight));
        }

        public override void AfterRender()
        {

        }

        public override void BeforeRender()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.ClearColor(0.5f, 0.5f, 0.5f, 1);

            GL.Enable(EnableCap.DepthTest);
        }
        public override void Render()
        {
            BeforeRender();

           GameEntityStorrage.Entities.Render(GameEntityStorrage.Entities.RootID);

            AfterRender();
        }
    }
}
