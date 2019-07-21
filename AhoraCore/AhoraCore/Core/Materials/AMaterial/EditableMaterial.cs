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
        ChannelsCount = 6
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

        public void SetDiffuse(string TextID)
        {
            
            textures.Add(TextureChannels.Diffuse, new TextureChannel( 0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Diffuse, "diffuseMap");
        }

        public void SetNormals(string TextID)
        {
            textures.Add(TextureChannels.Normal,  new TextureChannel( 1, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Normal, "normalMap");
        }

        public void SetSpecular(string TextID)
        {
            textures.Add(TextureChannels.Specular, new TextureChannel( 2, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Specular, "specularMap");
        }

        public void SetHeight(string TextID)
        {
            textures.Add(TextureChannels.Height, new TextureChannel( 3, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Height, "heightMap");
        }

        public void SetReflectGloss(string TextID)
        {
            textures.Add(TextureChannels.ReflectGloss, new TextureChannel( 4, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.ReflectGloss, "reflectGlossMap");
        }

        public void SetTransparency(string TextID)
        {
            textures.Add(TextureChannels.Transparency, new TextureChannel( 5, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Transparency, "transparencyMap");
        }

        public bool HasDiffuse
        {
            get { return textures.ContainsKey(TextureChannels.Diffuse); }
        }

        public bool HasNormals
        {
            get { return textures.ContainsKey(TextureChannels.Normal); }
        }

        public bool HasSpecular
        {
            get { return textures.ContainsKey(TextureChannels.Normal); }
        }

        public bool HasHeight
        {
            get { return textures.ContainsKey(TextureChannels.Height); }
        }

        public bool HasReflectGloss
        {
            get { return textures.ContainsKey(TextureChannels.ReflectGloss); }
        }

        public bool HasTransparency
        {
            get { return textures.ContainsKey(TextureChannels.Transparency); }
        }

        public override void Bind()
        {
        }

        public override void Bind(AShader bindTarget)
        {
            materialUniformBuffer.LinkBufferToShder(bindTarget, "MaterialData");
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
