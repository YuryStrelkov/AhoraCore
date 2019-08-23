using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Shaders;
using System.Collections.Generic;
using AhoraCore.Core.Buffers.UniformsBuffer;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.CES.ICES;

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
    public static class TextureShaderChannels
    {
        public static string Diffuse = "diffuseMap";
        public static string Normal = "normalMap";
        public static string Specular = "specularMap";
        public static string Height = "heightMap";
        public static string ReflectGloss = "reflectGlossMap";
        public static string Transparency = "transparencyMap";
   
        public static string GroundDiffuse = "groundDiffuse";
        public static string GroundNormal = "groundNormal";
        public static string GroundDisplacement = "groundDisplacement";
        public static string GroundReflectGloss = "groundReflectGloss";

        public static string GrassDiffuse = "grassDiffuse";
        public static string GrassNormal = "grassNormal";
        public static string GrassDisplacement = "grassDisplacement";
        public static string GrassReflectGloss = "grassReflectGloss";

        public static string RockDiffuse = "rockDiffuse";
        public static string RockNormal = "rockNormal";
        public static string RockDisplacement = "rockDisplacement";
        public static string RockReflectGloss = "rockReflectGloss";

        private static Dictionary<TextureChannels, string> ChannelShaderNames;

        private static void init()
        {
            ChannelShaderNames = new Dictionary<TextureChannels, string>();
            ChannelShaderNames.Add(TextureChannels.Diffuse, Diffuse);
            ChannelShaderNames.Add(TextureChannels.Normal, Normal);
            ChannelShaderNames.Add(TextureChannels.Specular, Specular);
            ChannelShaderNames.Add(TextureChannels.Height, Height);
            ChannelShaderNames.Add(TextureChannels.ReflectGloss, ReflectGloss);
            ChannelShaderNames.Add(TextureChannels.Transparency, Transparency);
            ChannelShaderNames.Add(TextureChannels.GrassDiffuse, GrassDiffuse);
            ChannelShaderNames.Add(TextureChannels.GrassNormal, GrassNormal);
            ChannelShaderNames.Add(TextureChannels.GrassDisplacement, GrassDisplacement);
            ChannelShaderNames.Add(TextureChannels.GrassReflectGloss, GrassReflectGloss);
            ChannelShaderNames.Add(TextureChannels.GroundDiffuse, GroundDiffuse);
            ChannelShaderNames.Add(TextureChannels.GroundNormal, GroundNormal);
            ChannelShaderNames.Add(TextureChannels.GroundDisplacement, GroundDisplacement);
            ChannelShaderNames.Add(TextureChannels.GroundReflectGloss, GroundReflectGloss);
            ChannelShaderNames.Add(TextureChannels.RockDiffuse, RockDiffuse);
            ChannelShaderNames.Add(TextureChannels.RockNormal, RockNormal);
            ChannelShaderNames.Add(TextureChannels.RockDisplacement, RockDisplacement);
            ChannelShaderNames.Add(TextureChannels.RockReflectGloss, RockReflectGloss);
       
        }

        public static string GetName(TextureChannels ID)
        {
            if (ChannelShaderNames == null)
            {
                init();
            }
            return ChannelShaderNames.ContainsKey(ID) ? ChannelShaderNames[ID]:"";

        }
    }


    /// <summary>
    /// TODO придумать общий интерфейс, который использоваь в качестве способа хранения материала 
    /// </summary>
    public abstract  class AMaterial: ABindableObject<AShader>
    {
        public static Dictionary<TextureChannels, string> ChannelsShaderNames = new Dictionary<TextureChannels, string>();

        private int channelOffset = 0;

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
            materialUniformBuffer.Bind();

            bindTarget.SetUniformBlock("MaterialData",materialUniformBuffer);
           /// materialUniformBuffer.LinkBufferToShder(bindTarget, "MaterialData");
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

        //public void AssignTexture2Channel(Texture t, TextureChannels ChannelID)
        //{
            
        //}

        public void AssignTexture2Channel(string tName, TextureChannels ChannelID)
        {
            textures.Add(ChannelID, new TextureChannel(channelOffset, tName, ref materialUniformBuffer));
            texturesChannelNames.Add(ChannelID, TextureShaderChannels.GetName(ChannelID ));
            channelOffset++;
        }

        public bool HasChannel(TextureChannels ChannelID)
        {
            return textures.ContainsKey(ChannelID);
        }

        public abstract int ReadMaterial(int startline, ref string[]  lines);

        public abstract void InitMaterial();

        public AMaterial()
        {
            Create();
            InitMaterial();
         }
    }
}
