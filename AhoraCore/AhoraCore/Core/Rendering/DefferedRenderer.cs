using AhoraCore.Core.Context;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace AhoraCore.Core.Rendering
{
    public class DefferedRenderer : RederPipeline
    {
        Dictionary<string, RenderPass> Passes;

        public DefferedRenderer()
        {
            Passes = new Dictionary<string, RenderPass>();
            Passes.Add("GeometryPass", new GeometryRenderPass(this,MainContext.ScreenWidth, MainContext.ScreenHeight));
        }

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

            foreach (string key in Passes.Keys)
            {
                Passes[key].DrawPass();
            }
            AfterRender();
        }
    }
}
