using AhoraCore.Core.Context;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Materials;
using AhoraCore.Core.Shaders;
using OpenTK.Graphics.OpenGL;
namespace AhoraCore.Core.Rendering.RenderPasses
{
    public class SSAOPass : RenderPass
    {
        Texture noizeTex = TextureLoader.getNoizeTexture(10,10);

        public SSAOPass(RederPipeline parent, int w, int h):base(parent, w, h)
        {
            SetParent(parent);
        }

       public override void Create()
        {
            AddTextureAttachment("SSAOBuffer");

            EnableBuffering("SSAOBuffer");

            MarkBuffer( new string[] { "ssaoRaduis", "ssaoBias", "randVectors" }, new int[] { 1, 1, 4*32 });

            ConfirmBuffer();

            RenderPassShder = new SSAOShader();

            UniformBuffer.UpdateBufferIteam("ssaoRaduis", 1);

            UniformBuffer.UpdateBufferIteam("ssaoBias", 4.0f);

            UniformBuffer.UpdateBufferIteam("randVectors", SSAOKernel.Kernel);
        }

        public override void DisableSettings()
        {
        }

        public override void EnableSettings()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
        }

        public override void Input()
        {
        }

        public override void Render()
        {
            RenderPassShder.Bind();

            RenderPassShder.SetUniformBlock(BufferName, UniformBuffer);

            Cameras.CameraInstance.Get().Bind(RenderPassShder);

            Enable(FramebufferTarget.Framebuffer);

            EnableSettings();

            RenderPassShder.SetUniformi("screenWidth",MainContext.ScreenWidth);
            RenderPassShder.SetUniformi("screenHeight",MainContext.ScreenHeight);

            parent.getRenderPassBuffer("GeometryPass").UseColAttachAsTex(0, "PositionBuffer", "gPosition", RenderPassShder);
            parent.getRenderPassBuffer("GeometryPass").UseColAttachAsTex(1, "NormalBuffer", "gNormal", RenderPassShder);
            GL.ActiveTexture(TextureUnit.Texture0 + 2);
            GL.BindTexture(TextureTarget.Texture2D, noizeTex.ID);
            RenderPassShder.SetUniformi("noizeMap", 2);

            GeometryStorageManager.Data.RenderIteam("Canvas");

            Disable();
        }

        public override void Render(AShader shader)
        {
            shader.Bind();

            shader.UpdateUniforms();//SetUniformBlock(BufferName, UniformBuffer);

            Enable(FramebufferTarget.Framebuffer);

            EnableSettings();

            GeometryStorageManager.Data.RenderIteam("Canvas");

            Disable();
        }

        public override void Update()
        {
        }
    }
}
