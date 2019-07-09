using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Materials;
using AhoraCore.Core.Shaders;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AhoraCore.Core.Buffers.FrameBuffers
{

    public struct  LayerData
    {
        public int ID { get; set; }

        public PixelInternalFormat InteranalPixFormat { get; set; }

        public PixelFormat PixFormat { get; set; }

        public PixelType PixType { get; set; }

        public FramebufferAttachment LayerAttachment { get; set; }

        public LayerData(int ID_, PixelType PixT,PixelFormat PixF, PixelInternalFormat interanalPixF, FramebufferAttachment atch)
        {
            ID = ID_;
            InteranalPixFormat = interanalPixF;
            PixFormat = PixF;
            PixType = PixT;
            LayerAttachment = atch;
        }
    }

    public class FrameBuffer:ABindableObject<FramebufferTarget>
    {
        bool isMultisample = false;

        bool hasDepthBuffer = false;

        int multisamples = 0;
           
        int Width;

        int  Height;

        int DepthBufferID;

        private Dictionary<string, LayerData> bufferLayers;

        private List<DrawBuffersEnum> attachments;

        public override void Bind()
        {
            BindingTarget = FramebufferTarget.Framebuffer;
            GL.BindFramebuffer(BindingTarget, ID);
        }
     
        public override void Create()
        {
            ID = GL.GenFramebuffer();
            Bind(FramebufferTarget.DrawFramebuffer);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
            Unbind();

            if (hasDepthBuffer)
            {
                Bind();
                    GL.GenRenderbuffers(1, out DepthBufferID);
                    GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, DepthBufferID);
                    GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent, Width, Height);
                    GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, DepthBufferID);
                Unbind();
            }
           

        }

        public override void Delete()
        {
            if (ID != -1)
            {
                GL.DeleteFramebuffer(ID);
                if (bufferLayers != null)
                {
                    foreach (string k in bufferLayers.Keys)
                    {
                        GL.DeleteTexture(bufferLayers[k].ID);
                    }
                }
            }
       }

        public override void Unbind()
        {
            GL.BindFramebuffer(BindingTarget, 0);
        }

        public override void Bind(FramebufferTarget bindTarget)
        {
            BindingTarget = bindTarget;
            GL.BindFramebuffer(bindTarget, ID);
        }

        public int GetLayerID(string layerName)
        {
            if (bufferLayers.ContainsKey(layerName))
            {
                return bufferLayers[layerName].ID;
            }
            else
            {
                return -1;///bufferLayers[layerName];
            }
        }

        public void addColorAttachment(  string AttchName,
                                          PixelInternalFormat internalPixelFormat,
                                          PixelFormat pixelFormat,
                                          PixelType pixelType)
        {
            Bind(FramebufferTarget.DrawFramebuffer);

            GenerateTextureAttachment(AttchName,internalPixelFormat, pixelFormat, pixelType);
            attachments.Add((DrawBuffersEnum)(FramebufferAttachment.ColorAttachment0 + attachments.Count));
            GL.DrawBuffers(bufferLayers.Count, attachments.ToArray());
        }


        private void GenerateTextureAttachment(string layerName,
                                               PixelInternalFormat internalPixelFormat,
                                               PixelFormat pixelFormat,
                                               PixelType pixelType)
        {
            int id;

            GL.GenTextures(1, out id);

            LayerData Layer = new LayerData(id, pixelType, pixelFormat, internalPixelFormat,FramebufferAttachment.ColorAttachment0 + bufferLayers.Count);

            GL.BindTexture(TextureTarget.Texture2D, Layer.ID);

            GL.TexImage2D(TextureTarget.Texture2D, 0, internalPixelFormat, Width, Height, 0, pixelFormat, pixelType, IntPtr.Zero);

            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, ref TextureLoader.NEAREST);

            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, ref TextureLoader.NEAREST);

            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, Layer.LayerAttachment, TextureTarget.Texture2D, Layer.ID, 0);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            bufferLayers.Add(layerName, Layer);

        }

        public FrameBuffer(int width, int height, bool msaa, bool hasDepthBuffer)
        {
            Width = width;

            Height = height;
            //attachments = new List<DrawBuffersEnum>();
            bufferLayers = new Dictionary<string, LayerData>();

            attachments = new List<DrawBuffersEnum>();

            this.hasDepthBuffer = hasDepthBuffer;

            Create();

            if (DebugBuffers.DisplayFrameBufferErrors("emptyBuffer", GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer))
                != FramebufferErrorCode.FramebufferComplete)
            {
                throw new Exception("frameBuffer creation faild");
            }
            else
            {
                Unbind();
            }
        }

        public void ActivateBufferLayerAsTexture(int channelTarget, string layerNameInShader, AShader shader)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + channelTarget);
            ///такое обращение к словорю черевато, обязаельно надо быть уверенным, что запрашиваешь имя, которое точно там есть
            GL.BindTexture(TextureTarget.Texture2D, bufferLayers[layerNameInShader].ID);
            shader.SetUniformi(layerNameInShader, channelTarget);
        }


        public void activateBufferLayerAsTexture(int channelTarget, string layerName, string nameInShader, AShader shader)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + channelTarget);
            ///такое обращение к словорю черевато, обязаельно надо быть уверенным, что запрашиваешь имя, которое точно там есть
            GL.BindTexture(TextureTarget.Texture2D, bufferLayers[layerName].ID);
            shader.SetUniformi(nameInShader, channelTarget);
        }
    }
}
