using AhoraCore.Core.CES;
using AhoraCore.Core.Shaders;
using OpenTK.Graphics.OpenGL;
namespace AhoraCore.Core.Rendering.RenderPasses
{
    public class GeometryRenderPass:RenderPass
    {
        public GeometryRenderPass(RederPipeline parent, int w, int h):base(parent, w, h)
        {
            SetParent(parent);
        }

        public override void Create()
        {
            AddTextureAttachment("PositionBuffer");
            AddTextureAttachment("NormalBuffer");
            AddTextureAttachment("ColorBuffer");
            AddTextureAttachment("FresnelBuffer");

            EnableBuffering("GeoPassData");

            MarkBuffer(new string[] { "FogColor", "SkyColor", "zFarFog", "zNearFog", "FogA", "FogB" }, new int[] { 4, 4, 1, 1, 1,1 });

            ConfirmBuffer();
        }

        public override void DisableSettings()
        {
        }

        public override void Render()
        {
          /// TODO
            /* while (GameEntityStorrage.Entities.HasElementsInRenderQue())
                {
                    GameEntityStorrage.Entities.Render(GameEntityStorrage.Entities.GetNextRenderID());
                } */

            Enable(FramebufferTarget.Framebuffer);

            EnableSettings();

            GameEntityStorrage.Entities.Render(GameEntityStorrage.Entities.RootID);

            Disable();
        }

        public override void EnableSettings()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
          
        }

        public override void Input()
        {
        
        }

        public override void Render(AShader shader)
        {
            shader.SetUniformBlock(BufferName, UniformBuffer);

            Enable(FramebufferTarget.Framebuffer);

            EnableSettings();

            GameEntityStorrage.Entities.Render(GameEntityStorrage.Entities.RootID);

            Disable();

        }

        public override void Update()
        {

        }
    }
}
