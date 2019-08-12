using AhoraCore.Core.Materials;
using AhoraCore.Core.Materials.AMaterial;
using System;

namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
    class TerrainMaterial:EditableMaterial
    {
        public static int MAX_TEXTURE_CHANNELS_NUMBER = (int)TextureChannels.TerrainChannelsCount;

        public void SetGroundDiffuse(string TextID)
        {
            textures.Add(TextureChannels.GroundDiffuse, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Diffuse, "groundDiffuse");
        }

        public void SetGroundNormal(string TextID)
        {
            textures.Add(TextureChannels.GroundNormal, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Diffuse, "groundNormal");
        }

        public void SetGroundDisplacemnt(string TextID)
        {
            textures.Add(TextureChannels.GroundDisplacement, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Diffuse, "groundDisplacement");
        }



        public void SetGrassDiffuse(string TextID)
        {
            textures.Add(TextureChannels.GrassDiffuse, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Diffuse, "grassDiffuse");
        }

        public void SetGrassNormal(string TextID)
        {
            textures.Add(TextureChannels.GrassNormal, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Diffuse, "grassNormal");
        }

        public void SetGrassDisplacemnt(string TextID)
        {
            textures.Add(TextureChannels.GrassDisplacement, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Diffuse, "grassDisplacement");
        }


        public void SetRockDiffuse(string TextID)
        {
            textures.Add(TextureChannels.RockDiffuse, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Diffuse, "rockDiffuse");
        }

        public void SetRockNormal(string TextID)
        {
            textures.Add(TextureChannels.RockNormal, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Diffuse, "rockNormal");
        }

        public void SetRockDisplacemnt(string TextID)
        {
            textures.Add(TextureChannels.RockDisplacement, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.Diffuse, "rockDisplacement");
        }



        private int ReadMaterial(int startLine, ref string[] lines)
        {
             float hor_scale = 1, ver_scale = 1;

            if (!lines[startLine + 1].Equals("{"))
            {
                Console.WriteLine("Wrong material definition");
                return startLine + 1;
            }

            int i = startLine + 2;

            do
            {
                string[] tokens = lines[i].Split(' ');

                switch (tokens[0])
                {
                    case "verticalScale": hor_scale = float.Parse(tokens[1]); break;

                    case "horizontalScale": ver_scale = float.Parse(tokens[1]); break;


                    case "grassDiffuse":
                        SetGrassDiffuse(tokens[1]);
                        Textures[TextureChannels.GrassDiffuse].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.GrassDiffuse].SetMultiply(1, 1, 1, ver_scale);
                        break;

                    case "grassNormal":
                        SetGrassNormal(tokens[1]);
                        Textures[TextureChannels.GrassNormal].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.GrassNormal].SetMultiply(1, 1, 1, ver_scale);
                        break;
                    case "grassDisplacemnt":
                        SetGrassDisplacemnt(tokens[1]);
                        Textures[TextureChannels.GrassDisplacement].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.GrassDisplacement].SetMultiply(1, 1, 1, ver_scale);
                        break;


                    case "rockDiffuse":
                        SetGrassDiffuse(tokens[1]);
                        Textures[TextureChannels.RockDiffuse].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.RockDiffuse].SetMultiply(1, 1, 1, ver_scale);
                        break;

                    case "rockNormal":
                        SetGrassNormal(tokens[1]);
                        Textures[TextureChannels.RockNormal].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.RockNormal].SetMultiply(1, 1, 1, ver_scale);
                        break;
                    case "rockDisplacemnt":
                        SetGrassDisplacemnt(tokens[1]);
                        Textures[TextureChannels.RockDisplacement].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.RockDisplacement].SetMultiply(1, 1, 1, ver_scale);
                        break;



                    case "groundDiffuse":
                        SetGrassDiffuse(tokens[1]);
                        Textures[TextureChannels.GroundDiffuse].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.GroundDiffuse].SetMultiply(1, 1, 1, ver_scale);
                        break;

                    case "groundNormal":
                        SetGrassNormal(tokens[1]);
                        Textures[TextureChannels.GroundNormal].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.GroundNormal].SetMultiply(1, 1, 1, ver_scale);
                        break;
                    case "groundDisplacemnt":
                        SetGrassDisplacemnt(tokens[1]);
                        Textures[TextureChannels.GroundDisplacement].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.GroundDisplacement].SetMultiply(1, 1, 1, ver_scale);
                        break;

                }
                i++;
            }

            while (!lines[i + 1].Equals("}"));

            return i + 2;

        }




        public TerrainMaterial() : base()
        {
            
            materialUniformBuffer.addBufferItem("albedoColor", 4);
            materialUniformBuffer.addBufferItem("ambientColor", 4);
            materialUniformBuffer.addBufferItem("reflectionColor", 4);

            //materialUniformBuffer.addBufferItem("reflectivity", 1);
            //materialUniformBuffer.addBufferItem("metallness", 1);
            //materialUniformBuffer.addBufferItem("roughness", 1);
            //materialUniformBuffer.addBufferItem("transparency", 1);

            for (int j = 0; j < (int)TextureChannels.TerrainChannelsCount; j++)
            {
                materialUniformBuffer.addBufferItem("channel[" + j + "].tileUV", 2);
                materialUniformBuffer.addBufferItem("channel[" + j + "].offsetUV", 2);
                materialUniformBuffer.addBufferItem("channel[" + j + "].multRGBA", 4);
            }
            ///  materialUniformBuffer.packBuffer();


            materialUniformBuffer.Create(1);///Создаёт один размеченный выше буфер для материала 


            materialUniformBuffer.UpdateBufferIteam("albedoColor", DefColor);
            materialUniformBuffer.UpdateBufferIteam("ambientColor", DefColor);
            materialUniformBuffer.UpdateBufferIteam("reflectionColor", BlackColor);

            /// TextureChannelData.UpdateBufferIteam("Channel[" + ChannelOffset + "].multRGBA", ChannelOffset * 8 + 4, new float[] { R_mull, G_mull, B_mull, A_mull });//  ("Channel[" + (int)channel + "].multRGBA", );
            //materialUniformBuffer.UpdateBufferIteam("reflectivity", 0);
            //materialUniformBuffer.UpdateBufferIteam("metallness", 0);
            //materialUniformBuffer.UpdateBufferIteam("roughness", 0);
            //materialUniformBuffer.UpdateBufferIteam("transparency", 0);

            for (int j = 0; j < (int)TextureChannels.TerrainChannelsCount; j++)
            {
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].tileUV", DefUV);
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].offsetUV", DefOffs);
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].multRGBA", DefMult);
            }
        }
    }
}
