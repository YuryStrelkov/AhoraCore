using AhoraCore.Core.Buffers.FrameBuffers;
using AhoraCore.Core.Buffers.IBuffers.BindableObject;
using AhoraCore.Core.CES;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Shaders;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Rendering
{
    public abstract class RenderPass : AComponent<RederPipeline>
    {
        //   private RederPipeline parent;
        protected AShader RenderPassShder;

        private FrameBuffer PassBuffer;
        /// <summary>
        /// Render Pass based on framebuffer with only depth component
        /// </summary>
        /// <param name="parent"></param>
        public RenderPass(RederPipeline parent)
        {
            this.parent = parent;

            PassBuffer = new FrameBuffer();

            Create();
        }

        /// <summary>
        /// Render Pass based on framebuffer (w , h) with only depth component
        /// </summary>
        /// <param name="parent"></param>
        public RenderPass(RederPipeline parent, int w, int h):base()
        {
            this.parent = parent;

            PassBuffer = new FrameBuffer(w,h);

            Create();
        }

        public void UseColAttachAsTex(int targetChannel,string channelName, AShader shder)
        {
            PassBuffer.ActivateBufferLayerAsTexture(targetChannel, channelName, shder);
        }

        public void UseColAttachAsTex(int targetChannel, string channelName, string shaderChannelName, AShader shder)
        {
            PassBuffer.ActivateBufferLayerAsTexture(targetChannel, channelName, shaderChannelName, shder);
        }

        public void AddTextureAttachment(string texName)
        {
            PassBuffer.addColorAttachment(texName, PixelInternalFormat.Rgba16f, PixelFormat.Rgb, PixelType.UnsignedByte);
        }

        public abstract void EnableSettings();

        public abstract void DisableSettings();

        public override void Enable()
        {
            PassBuffer.Bind();
        }

        public void Enable(FramebufferTarget bindingTarget)
        {
            PassBuffer.Bind(bindingTarget);
        }


        public abstract void Create();


        public override void Delete()
        {
            PassBuffer.Delete();
        }

        public override void Disable()
        {
            PassBuffer.Unbind();
        }

        public override void Clear()
        {
            PassBuffer.Clear();
        }
    }
}
