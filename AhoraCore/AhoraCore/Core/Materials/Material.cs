using System;
using AhoraCore.Core.Materials.AbstractMaterial;
using Newtonsoft.Json;

namespace AhoraCore.Core.Materials
{
   
    public class Material:AMaterial
    {

        public static int MAX_TEXTURE_CHANNELS_NUMBER = 8;

        /*  public void SetDiffuse(string TextID)
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
          }*/

        public override void ReadMaterial(JsonTextReader reader)
        {
            float hor_scale = 1;

            float ver_scale = 1;

            switch (reader.Value.ToString())
            {
                case "verticalScale":
                    reader.Read();
                    ver_scale = float.Parse(reader.Value.ToString());
                    break;

                case "horizontalScale":
                    reader.Read();
                    hor_scale = float.Parse(reader.Value.ToString());
                    break;
                case "diffuseMap":
                    reader.Read();
                    Texture2ChannelAssign(reader.Value.ToString(), "diffuseMap");
                    break;

                case "normalMap":
                    reader.Read();
                    Texture2ChannelAssign(reader.Value.ToString(), "normalMap");
                    break;

                case "specularMap":
                    reader.Read();
                    Texture2ChannelAssign(reader.Value.ToString(), "specularMap");
                    break;

                case "heightMap":
                    reader.Read();
                    Texture2ChannelAssign(reader.Value.ToString(), "heightMap");
                    break;

                case "reflectGlossMap":
                    reader.Read();
                    Texture2ChannelAssign(reader.Value.ToString(), "reflectGlossMap");
                    break;

                case "transparencyMap":
                    reader.Read();
                    Texture2ChannelAssign(reader.Value.ToString(), "transparencyMap");
                    break;

            }

     ///       materialUniformBuffer.UpdateBufferIteam("scaling", new float[2] { hor_scale, ver_scale });

        }

        public override int ReadMaterial(int startline, ref string[] lines)
        {
            return - 1;
        }

        public override void InitMaterial()
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
            materialUniformBuffer.UpdateBufferIteam("ambientColor", DefColor);
            materialUniformBuffer.UpdateBufferIteam("reflectionColor", BlackColor);

            /// TextureChannelData.UpdateBufferIteam("Channel[" + ChannelOffset + "].multRGBA", ChannelOffset * 8 + 4, new float[] { R_mull, G_mull, B_mull, A_mull });//  ("Channel[" + (int)channel + "].multRGBA", );
            materialUniformBuffer.UpdateBufferIteam("reflectivity", 0);
            materialUniformBuffer.UpdateBufferIteam("metallness", 0);
            materialUniformBuffer.UpdateBufferIteam("roughness", 0);
            materialUniformBuffer.UpdateBufferIteam("transparency", 0);

            for (int j = 0; j < (int)TextureChannels.ChannelsCount; j++)
            {
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].tileUV", DefUV);
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].offsetUV", DefOffs);
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].multRGBA", DefMult);
            }
        }



        public Material() : base()
        {
        }
    }
}
