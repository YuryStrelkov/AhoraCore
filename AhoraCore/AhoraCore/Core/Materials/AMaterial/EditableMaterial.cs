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

        protected static float[] DefColor = new float[] { 0.5f, 0.5f, 0.5f, 0.5f };

        protected static float[] BlackColor = new float[] { 0.5f, 0.5f, 0.5f, 0.5f };


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

        public void SetDiffuse(Texture t)
        {
            textures.Add(TextureChannels.Diffuse, new TextureChannel(TextureChannel.NUMBER_OF_PARAMETRS * 0, t,ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Diffuse, "Diffuse");
        }

        public void SetNormals(Texture t)
        {
            textures.Add(TextureChannels.Normal,  new TextureChannel(TextureChannel.NUMBER_OF_PARAMETRS * 1, t, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Normal, "Normal");
        }

        public void SetSpecular(Texture t)
        {
            textures.Add(TextureChannels.Specular, new TextureChannel(TextureChannel.NUMBER_OF_PARAMETRS * 2, t, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Specular, "Specular");
        }

        public void SetHeight(Texture t)
        {
            textures.Add(TextureChannels.Height, new TextureChannel(TextureChannel.NUMBER_OF_PARAMETRS * 3, t, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Height, "Height");
        }

        public void SetReflectGloss(Texture t)
        {
            textures.Add(TextureChannels.ReflectGloss, new TextureChannel(TextureChannel.NUMBER_OF_PARAMETRS * 4, t, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.ReflectGloss, "ReflectGloss");
        }

        public void SetTransparency(Texture t)
        {
            textures.Add(TextureChannels.Transparency, new TextureChannel(TextureChannel.NUMBER_OF_PARAMETRS * 5, t, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Transparency, "Transparency");
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
