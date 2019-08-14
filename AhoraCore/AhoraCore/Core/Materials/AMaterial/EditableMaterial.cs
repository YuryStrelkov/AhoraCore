using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Shaders;
using System.Collections.Generic;
using AhoraCore.Core.Buffers.UniformsBuffer;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Materials.AMaterial
{
    public enum TextureChannels
    {
        Diffuse = 0,
        Normal = 1,
        Specular = 2,
        Height = 3,
        ReflectGloss=4,
        Transparency=5,
        ChannelsCount = 6,

        GroundDiffuse = 7,
        GroundNormal = 8,
        GroundDisplacement = 9,
        GroundReflectGloss = 10,

        GrassDiffuse = 11,
        GrassNormal = 12,
        GrassDisplacement = 13,
        GrassReflectGloss = 14,

        RockDiffuse = 15,
        RockNormal = 16,
        RockDisplacement = 17,
        RockReflectGloss = 18,

        TerrainChannelsCount = 12
    }
    public class EditableMaterial: ABindableObject<AShader>
    {

        protected static float[] DefColor = new float[] { 1f, 0.0f, 0.0f, 1f };

        protected static float[] BlackColor = new float[] { 0.5f, 0.5f, 0.5f, 1f };

        protected static float[] DefUV = new float[] { 1, 1};

        protected static float[] DefMult = new float[] { 0.5f, 0.5f, 0.5f, 0.5f };

        protected static float[] DefOffs = new float[] { 0, 0 };

        protected UniformsBuffer<string> materialUniformBuffer;

        protected Dictionary<TextureChannels, TextureChannel> textures;

        protected Dictionary<TextureChannels, string> texturesChannelNames;

        public Dictionary<TextureChannels, TextureChannel> Textures
        {
            get
            {
                return textures;
            }
            protected set
            {
                textures = value;
            }
        }

        public override void Bind()
        {
        }

        public override void Bind(AShader bindTarget)
        {
            materialUniformBuffer.LinkBufferToShder(bindTarget, "MaterialData",1);
            int i = 0;
            foreach (TextureChannels key in textures.Keys)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + i);
                GL.BindTexture(textures[key].Texture.BindingTarget, textures[key].Texture.ID);
                bindTarget.SetUniformi(texturesChannelNames[key], i);
                i++;
            }
        }

        public override void Create()
        {
            textures = new Dictionary<TextureChannels, TextureChannel>();
            texturesChannelNames = new Dictionary<TextureChannels, string>();
            materialUniformBuffer = new UniformsBuffer<string>();
        }

        public override void Clear()
        {
            materialUniformBuffer.Clear();
        }

        public override void Delete()
        {
            materialUniformBuffer.Delete();
        }

        public override void Unbind()
        {
        }
        
        public  EditableMaterial()
        {
            Create();
          ///  materialUniformBuffer = new UniformsBuffer<string>(8 * 6);
        }
    }
}
