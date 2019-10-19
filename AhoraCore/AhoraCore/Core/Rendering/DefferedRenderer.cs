using AhoraCore.Core.Cameras;
using AhoraCore.Core.Context;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Rendering.RenderPasses;
using AhoraCore.Core.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Rendering
{
    public class DefferedRenderer : RederPipeline
    {
        DefferedFinalStepShdr displayShader;

        public DefferedRenderer():base()
        {
            displayShader = new DefferedFinalStepShdr();

            Passes.Add("GeometryPass", new GeometryRenderPass(this,MainContext.ScreenWidth, MainContext.ScreenHeight));

            Passes.Add("SSAOPass", new SSAOPass(this, MainContext.ScreenWidth, MainContext.ScreenHeight));

       //     Passes.Add("SSLR", new GeometryRenderPass(this, MainContext.ScreenWidth, MainContext.ScreenHeight));
        }

        public override void AfterRender()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            displayShader.Bind();

            displayShader.SetUniform("position", Vector3.One);
            
            displayShader.SetUniform("scale", Vector3.One);

            CameraInstance.Get().Bind(displayShader);

            getRenderPassBuffer("GeometryPass").UseColAttachAsTex(0, "PositionBuffer", "Positions", displayShader);
            getRenderPassBuffer("GeometryPass").UseColAttachAsTex(1, "NormalBuffer", "Normals", displayShader);
            getRenderPassBuffer("GeometryPass").UseColAttachAsTex(2, "ColorBuffer", "Colors", displayShader);
            getRenderPassBuffer("GeometryPass").UseColAttachAsTex(3, "FresnelBuffer", "Fresnels", displayShader);
            getRenderPassBuffer("SSAOPass").UseColAttachAsTex(4, "SSAOBuffer", "SSAO", displayShader);

            GeometryStorageManager.Data.RenderIteam("Canvas");
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

                 foreach (string key in Passes.Keys)
               {
                   Passes[key].Render();
               }

               AfterRender(); 
        }
    }
}
