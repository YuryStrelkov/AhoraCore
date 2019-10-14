using AhoraCore.Core.Materials.AbstractMaterial;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AhoraCore.Core.Materials
{

    public class Material:AMaterial
    {
        public static int MAX_TEXTURE_CHANNELS_NUMBER = 8;

        private Dictionary<string, int> ChannelPerID;

        public override void ReadMaterial(JsonTextReader reader)
        {
            float[] tmp = new float[] { 0, 0, 0, 0 };
            float[] tmp2 = new float[] { 0, 0 };
            int i = 0;
            string tmpstr = "diffuseMap";
            while (reader.TokenType != JsonToken.EndObject)
            {
                if (reader.Value == null)
                {
                    reader.Read();
                    continue;
                }
                switch (reader.Value.ToString())
                {
                    case "name":
                        reader.Read();
                        MaterialName = reader.Value.ToString();
                        reader.Read();
                        break;

                    case "diffuseColor":
                        while (reader.TokenType != JsonToken.StartArray)
                        {
                            reader.Read();
                        }
                        reader.Read();
                        while (reader.TokenType != JsonToken.EndArray)
                        {
                            tmp[i] = float.Parse(reader.Value.ToString());
                            reader.Read();
                            i++;
                        }
                        i = 0;
                        materialUniformBuffer.Bind();
                        materialUniformBuffer.UpdateBufferIteam("albedoColor", tmp);
                        reader.Read();
                        break;

                    case "reflectionColor":

                        while (reader.TokenType != JsonToken.StartArray)
                        {
                            reader.Read();
                        }
                        reader.Read();
                        while (reader.TokenType != JsonToken.EndArray)
                        {
                            reader.Read();
                            tmp[i] = float.Parse(reader.Value.ToString());
                            reader.Read();
                            i++;
                        }
                        i = 0;
                        materialUniformBuffer.Bind();
                        materialUniformBuffer.UpdateBufferIteam("reflectionColor", tmp);
                        reader.Read();
                        break;

                    case "textureChannel":

                        while (reader.TokenType != JsonToken.StartArray)
                        {
                            reader.Read();
                        }
                        reader.Read();

                        while (reader.TokenType != JsonToken.EndArray)
                        {
                           
                            if (reader.TokenType != JsonToken.StartObject)
                            {
                                reader.Read();
                                continue;
                            }

                            while (reader.TokenType != JsonToken.EndObject)
                            {
                                if (reader.Value==null)
                                {
                                    reader.Read();
                                    continue;
                                }
                             
                                switch (reader.Value.ToString())
                                {
                                    case "cahnnel":
                                        reader.Read();
                                        tmpstr = reader.Value.ToString();
                                        reader.Read();
                                        break;
                                        
                                    case "texture":
                                        reader.Read();
                                        Texture2ChannelAssign(reader.Value.ToString(), tmpstr);
                                        reader.Read();
                                        break;
                                    case "uvScaling":
                                        reader.Read(); reader.Read();
                                        tmp2[0] = float.Parse(reader.Value.ToString());
                                        reader.Read(); 
                                        tmp2[1] = float.Parse(reader.Value.ToString());
                                        reader.Read();
                                        materialUniformBuffer.Bind();
                                        materialUniformBuffer.UpdateBufferIteam("channel[" + ChannelPerID[tmpstr] + "].tileUV", DefUV);
                                        break;

                                    case "uvShift":
                                        reader.Read(); reader.Read();
                                        tmp2[0] = float.Parse(reader.Value.ToString());
                                        reader.Read(); 
                                        tmp2[1] = float.Parse(reader.Value.ToString());
                                        reader.Read();
                                        materialUniformBuffer.Bind();
                                        materialUniformBuffer.UpdateBufferIteam("channel[" + ChannelPerID[tmpstr] + "].offsetUV", DefUV);
                                        break;
                                }
                            }

                        }




                        if (reader.TokenType != JsonToken.StartObject)
                        {
                            continue;
                        }
                        reader.Read();
                        while (reader.TokenType != JsonToken.EndObject)
                        {
                            tmp[i] = float.Parse(reader.Value.ToString());
                            reader.Read();
                            i++;
                        }
                        break;
                }

            }
            reader.Read();

            materialUniformBuffer.Bind();

            Unbind();   

        }


        public override void InitMaterial()
        {

            ///КОСТЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЛЬ!!!
            ChannelPerID = new Dictionary<string, int>();

            ChannelPerID.Add("diffuseMap",0);
            ChannelPerID.Add("normalMap", 1);
            ChannelPerID.Add("specularMap", 2);
            ChannelPerID.Add("heightMap", 3);
            ///

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
