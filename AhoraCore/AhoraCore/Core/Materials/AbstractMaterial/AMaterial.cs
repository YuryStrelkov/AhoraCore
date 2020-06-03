using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Shaders;
using System.Collections.Generic;
using AhoraCore.Core.Buffers.UniformsBuffer;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Buffers.DataStorraging;
using Newtonsoft.Json;

namespace AhoraCore.Core.Materials.AbstractMaterial
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
    /// <summary>
    /// TODO придумать общий интерфейс, который использоваь в качестве способа хранения материала 
    /// </summary>
    public abstract  class AMaterial: ABindableObject<AShader>
    {
        ////        public static Dictionary<TextureChannels, string> ChannelsShaderNames = new Dictionary<TextureChannels, string>();
       public string MaterialName { get; set; }

        private int channelOffset = 0;

        protected static float[] DefColor = new float[] { 1f, 0.0f, 0.0f, 1f };

        protected static float[] BlackColor = new float[] { 0.5f, 0.5f, 0.5f, 1f };

        protected static float[] DefUV = new float[] { 1, 1};

        protected static float[] DefMult = new float[] { 0.5f, 0.5f, 0.5f, 0.5f };

        protected static float[] DefOffs = new float[] { 0, 0 };

        protected UniformsBuffer<string> materialUniformBuffer;

      //  protected Dictionary< string, TextureChannel> textures;

        protected Dictionary<string, string> textures;//<textureID,textureInShderName>

        // protected Dictionary<TextureChannels, string> texturesChannelNames;
        /// 
        /// 
        /// 
        public Dictionary<string, string> Textures
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
            materialUniformBuffer.Bind();

            bindTarget.SetUniformBlock("MaterialData",materialUniformBuffer);
           /// materialUniformBuffer.LinkBufferToShder(bindTarget, "MaterialData");
            int i = 0;

            foreach (string key in textures.Keys)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + i);
                TextureStorrage.Textures.GetItem(textures[key]).Bind();
                bindTarget.SetUniformi(key, i);
                i++;
            }
        }

        public override void Create()
        {
            textures = new Dictionary<string, string>();
            materialUniformBuffer = new UniformsBuffer<string>();
            materialUniformBuffer.Buff_binding_Point = (int)UniformBindingsLocations.MaterialData;
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

        public void Texture2ChannelAssign(string TextureID, string TextureNameInShader)
        {
            if (TextureStorrage.Textures.Iteams.ContainsKey(TextureID))
            {
                textures.Add(TextureNameInShader, TextureID);
                channelOffset++;
            }
            else
            {
                textures.Add( TextureNameInShader, "DefaultTexture");
                channelOffset++;
            }

           
        }

        public bool HasChannel(string ChannelID)
        {
            return textures.ContainsKey(ChannelID);
        }

        public abstract void ReadMaterial(JsonTextReader reader);

        public abstract void InitMaterial();

        public AMaterial()
        {
            Create();
            InitMaterial();
            MaterialName = "";
         }
    }
}
