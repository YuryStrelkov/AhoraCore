using System;
using AhoraCore.Core.Buffers.FrameBuffers;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.IBuffers.BindableObject;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Rendering
{
    public abstract class RenderPass: IBindable
    {
        private RederPipeline parent;

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
        public RenderPass(RederPipeline parent, int w, int h)
        {
            this.parent = parent;

            PassBuffer = new FrameBuffer(w,h);

            Create();
        }

        public void AddTextureAttacment(string texName)
        {
            PassBuffer.addColorAttachment(texName, PixelInternalFormat.Rgba16f, PixelFormat.Rgb, PixelType.UnsignedByte);
        }

        public abstract void EnableSettings();

        public abstract void DisableSettings();

        public abstract void DrawPass();
     /*   {
            Bind();
            EnableCap();
            Drawinig smthng
            DisableCap();
        }*/

        public void Bind()
        {
            PassBuffer.Bind();
        }

        public abstract void Create();


        public void Delete()
        {
            PassBuffer.Delete();
        }

        public void Unbind()
        {
            PassBuffer.Unbind();
        }

        public void Clear()
        {
            PassBuffer.Clear();
        }
    }
}
