using AhoraCore.Core.Materials.AMaterial;

namespace AhoraCore.Core.Materials
{
   
    public class Material:EditableMaterial
    {

        public static int MAX_TEXTURE_CHANNELS_NUMBER = 8;

        public void SetDiffuse(string TextID)
        {

            textures.Add(TextureChannels.Diffuse, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Diffuse, "diffuseMap");
        }

        public void SetNormals(string TextID)
        {
            textures.Add(TextureChannels.Normal, new TextureChannel(1, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Normal, "normalMap");
        }

        public void SetSpecular(string TextID)
        {
            textures.Add(TextureChannels.Specular, new TextureChannel(2, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Specular, "specularMap");
        }

        public void SetHeight(string TextID)
        {
            textures.Add(TextureChannels.Height, new TextureChannel(3, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Height, "heightMap");
        }

        public void SetReflectGloss(string TextID)
        {
            textures.Add(TextureChannels.ReflectGloss, new TextureChannel(4, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.ReflectGloss, "reflectGlossMap");
        }

        public void SetTransparency(string TextID)
        {
            textures.Add(TextureChannels.Transparency, new TextureChannel(5, TextID, ref materialUniformBuffer));
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


        public Material() : base()
        {
            materialUniformBuffer.addBufferItem("albedoColor", 4);
            materialUniformBuffer.addBufferItem("ambientColor", 4);
            materialUniformBuffer.addBufferItem("reflectionColor", 4);

            materialUniformBuffer.addBufferItem("reflectivity", 1);
            materialUniformBuffer.addBufferItem("metallness", 1);
            materialUniformBuffer.addBufferItem("roughness", 1);
            materialUniformBuffer.addBufferItem("transparency", 1);

            for (int j = 0; j < (int)TextureChannels.ChannelsCount; j++)
            {
                materialUniformBuffer.addBufferItem("channel[" + j + "].tileUV", 2);
                materialUniformBuffer.addBufferItem("channel[" + j + "].offsetUV", 2);
                materialUniformBuffer.addBufferItem("channel[" + j + "].multRGBA", 4);
            }
            
            materialUniformBuffer.Create(1);///Создаёт один размеченный выше буфер для материала 


            materialUniformBuffer.UpdateBufferIteam("albedoColor", DefColor);
            materialUniformBuffer.UpdateBufferIteam("ambientColor",DefColor);
            materialUniformBuffer.UpdateBufferIteam("reflectionColor", BlackColor);

           /// TextureChannelData.UpdateBufferIteam("Channel[" + ChannelOffset + "].multRGBA", ChannelOffset * 8 + 4, new float[] { R_mull, G_mull, B_mull, A_mull });//  ("Channel[" + (int)channel + "].multRGBA", );
            materialUniformBuffer.UpdateBufferIteam("reflectivity", 0);
            materialUniformBuffer.UpdateBufferIteam("metallness", 0);
            materialUniformBuffer.UpdateBufferIteam("roughness", 0);
            materialUniformBuffer.UpdateBufferIteam("transparency", 0);

            for (int j = 0; j < (int)TextureChannels.ChannelsCount; j++)
            {
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].tileUV",   DefUV);
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].offsetUV", DefOffs);
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].multRGBA", DefMult);
            }
        }
    }
}
