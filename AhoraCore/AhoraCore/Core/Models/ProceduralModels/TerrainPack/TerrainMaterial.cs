using AhoraCore.Core.Materials.AbstractMaterial;
using System;

namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
  public   class TerrainMaterial:AMaterial
    {
        public static int MAX_TEXTURE_CHANNELS_NUMBER = (int)TextureChannels.TerrainChannelsCount;
        
        private void readSubMat(int submatN, ref int startLine, ref string[] lines)
        {
            ///int i = startLine;
            string[] tokens;
            float hor_scale = 1;
            float ver_scale = 1;
            TextureChannels ID;
            do
            {
                tokens = lines[startLine].Split(' ');

                switch (tokens[0])
                {
                    case "verticalScale": hor_scale = float.Parse(tokens[1]); break;

                    case "horizontalScale": ver_scale = float.Parse(tokens[1]); break;

                    case "diffuseMap":
                        ID = (TextureChannels)((int)TextureChannels.Diffuse + submatN * (int)TextureChannels.ChannelsCount);
                        AssignTexture2Channel("materials["+ submatN + "].diffuseMap", "settings[" + submatN + "].channel[", ID);
                        Textures[ID].SetMultiply(hor_scale, ver_scale, 1, 1);
                    break;

                    case "normalMap":
                        ID = (TextureChannels)((int)TextureChannels.Normal + submatN * (int)TextureChannels.ChannelsCount);
                        AssignTexture2Channel("materials[" + submatN + "].normalMap", "settings[" + submatN + "].channel[", ID);
                        Textures[ID].SetMultiply(hor_scale, ver_scale, 1, 1);
                    break;

                    case "specularMap":
                        ID = (TextureChannels)((int)TextureChannels.Specular + submatN * (int)TextureChannels.ChannelsCount);
                        AssignTexture2Channel("materials[" + submatN + "].specularMap", "settings[" + submatN + "].channel[", ID);
                        Textures[ID].SetMultiply(hor_scale, ver_scale, 1, 1);
                    break;

                    case "heightMap":
                        ID = (TextureChannels)((int)TextureChannels.Height + submatN * (int)TextureChannels.ChannelsCount);
                        AssignTexture2Channel("materials[" + submatN + "].heightMap", "settings[" + submatN + "].channel[", ID);
                        Textures[ID].SetMultiply(hor_scale, ver_scale, 1, 1);
                    break;

                    case "reflectGlossMap":
                        ID = (TextureChannels)((int)TextureChannels.ReflectGloss + submatN * (int)TextureChannels.ChannelsCount);
                        AssignTexture2Channel("materials[" + submatN + "].reflectGlossMap", "settings[" + submatN + "].channel[", ID);
                        Textures[ID].SetMultiply(hor_scale, ver_scale, 1, 1);
                    break;

                    case "transparencyMap":
                        ID = (TextureChannels)((int)TextureChannels.Transparency + submatN * (int)TextureChannels.ChannelsCount);
                        AssignTexture2Channel("materials[" + submatN + "].transparencyMap", "settings[" + submatN + "].channel[", ID);
                        Textures[ID].SetMultiply(hor_scale, ver_scale, 1, 1);
                   break;

                }
                startLine++;

            } while(!lines[startLine].Equals("}"));

            startLine +=1;
        }


        public override int ReadMaterial(int startLine, ref string[] lines)
        {
            if (!lines[startLine + 1].Equals("{\r"))
            {
                Console.WriteLine("Wrong material definition");
                return startLine + 1;
            }
            
            int i = startLine + 2;

            string[] tokens = lines[i].Split(' ');

            if (!tokens[0].Equals("count"))
            {
                Console.WriteLine("Wrong material definition");
                return i + 1;
            }

            int subMaterialsNumber = int.Parse(tokens[1]);

            InitMaterial(subMaterialsNumber);

            i+=1;

            int submatIDX = 0;
            do
            {
                tokens = lines[i].Split(' ');

                if (tokens[0].Equals("{\r"))
                {
                    readSubMat(submatIDX, ref i, ref lines);
                    submatIDX += 1;
                }
                else
                {
                    i += 1;
                }

            } while (!lines[i].Equals("}"));

            return i + 1;

        }

        public void InitMaterial(int MAX_MATERIALS)
        {
            for (int i = 0; i < MAX_MATERIALS; i++)
            {
                materialUniformBuffer.addBufferItem("settings[" + i + "].albedoColor", 4);
                materialUniformBuffer.addBufferItem("settings[" + i + "].ambientColor", 4);
                materialUniformBuffer.addBufferItem("settings[" + i + "].reflectionColor", 4);

                for (int j = 0; j < (int)TextureChannels.TerrainChannelsCount; j++)
                {
                    materialUniformBuffer.addBufferItem("settings[" + i + "].channel[" + j + "].tileUV", 2);
                    materialUniformBuffer.addBufferItem("settings[" + i + "].channel[" + j + "].offsetUV", 2);
                    materialUniformBuffer.addBufferItem("settings[" + i + "].channel[" + j + "].multRGBA", 4);
                }
            }

            materialUniformBuffer.Create(1);///Создаёт один размеченный выше буфер для материала 

            for (int i = 0; i < MAX_MATERIALS; i++)
            {
                materialUniformBuffer.UpdateBufferIteam("settings[" + i + "].albedoColor", DefColor);
                materialUniformBuffer.UpdateBufferIteam("settings[" + i + "].ambientColor", DefColor);
                materialUniformBuffer.UpdateBufferIteam("settings[" + i + "].reflectionColor", BlackColor);

                for (int j = 0; j < (int)TextureChannels.TerrainChannelsCount; j++)
                {
                    materialUniformBuffer.UpdateBufferIteam("settings[" + i + "].channel[" + j + "].tileUV", DefUV);
                    materialUniformBuffer.UpdateBufferIteam("settings[" + i + "].channel[" + j + "].offsetUV", DefOffs);
                    materialUniformBuffer.UpdateBufferIteam("settings[" + i + "].channel[" + j + "].multRGBA", DefMult);
                }
            }
        }


        public override void InitMaterial()
        {
            //for (int i = 0; i < 3; i++)
            //{
            //    materialUniformBuffer.addBufferItem("settings[" + i + "].albedoColor", 4);
            //    materialUniformBuffer.addBufferItem("settings[" + i + "].ambientColor", 4);
            //    materialUniformBuffer.addBufferItem("settings[" + i + "].reflectionColor", 4);

            //    for (int j = 0; j < (int)TextureChannels.TerrainChannelsCount; j++)
            //    {
            //        materialUniformBuffer.addBufferItem("settings[" + i + "].channel[" + j + "].tileUV", 2);
            //        materialUniformBuffer.addBufferItem("settings[" + i + "].channel[" + j + "].offsetUV", 2);
            //        materialUniformBuffer.addBufferItem("settings[" + i + "].channel[" + j + "].multRGBA", 4);
            //    }
            //}

            //materialUniformBuffer.Create(1);///Создаёт один размеченный выше буфер для материала 

            //for (int i = 0; i < 3; i++)
            //{
            //    materialUniformBuffer.UpdateBufferIteam("settings[" + i + "].albedoColor", DefColor);
            //    materialUniformBuffer.UpdateBufferIteam("settings[" + i + "].ambientColor", DefColor);
            //    materialUniformBuffer.UpdateBufferIteam("settings[" + i + "].reflectionColor", BlackColor);

            //    for (int j = 0; j < (int)TextureChannels.TerrainChannelsCount; j++)
            //    {
            //        materialUniformBuffer.UpdateBufferIteam("settings[" + i + "].channel[" + j + "].tileUV", DefUV);
            //        materialUniformBuffer.UpdateBufferIteam("settings[" + i + "].channel[" + j + "].offsetUV", DefOffs);
            //        materialUniformBuffer.UpdateBufferIteam("settings[" + i + "].channel[" + j + "].multRGBA", DefMult);
            //    }
            //}
        }
 
    }
}
