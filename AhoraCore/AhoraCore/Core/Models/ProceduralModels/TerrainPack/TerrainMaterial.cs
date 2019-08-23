using AhoraCore.Core.Materials;
using AhoraCore.Core.Materials.AbstractMaterial;
using System;

namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
  public   class TerrainMaterial:AMaterial
    {
        public static int MAX_TEXTURE_CHANNELS_NUMBER = (int)TextureChannels.TerrainChannelsCount;

        /*public void SetGroundDiffuse(string TextID)
        {
            textures.Add(TextureChannels.GroundDiffuse, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.GroundDiffuse, "groundDiffuse");
        }

        public void SetGroundNormal(string TextID)
        {
            textures.Add(TextureChannels.GroundNormal, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.GroundNormal, "groundNormal");
        }

        public void SetGroundDisplacemnt(string TextID)
        {
            textures.Add(TextureChannels.GroundDisplacement, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.GroundDisplacement, "groundDisplacement");
        }

        public void SetGrassDiffuse(string TextID)
        {
            textures.Add(TextureChannels.GrassDiffuse, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.GrassDiffuse, "grassDiffuse");
        }

        public void SetGrassNormal(string TextID)
        {
            textures.Add(TextureChannels.GrassNormal, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.GrassNormal, "grassNormal");
        }

        public void SetGrassDisplacemnt(string TextID)
        {
            textures.Add(TextureChannels.GrassDisplacement, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.GrassDisplacement, "grassDisplacement");
        }

        public void SetRockDiffuse(string TextID)
        {
            textures.Add(TextureChannels.RockDiffuse, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.RockDiffuse, "rockDiffuse");
        }

        public void SetRockNormal(string TextID)
        {
            textures.Add(TextureChannels.RockNormal, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.RockNormal, "rockNormal");
        }

        public void SetRockDisplacemnt(string TextID)
        {
            textures.Add(TextureChannels.RockDisplacement, new TextureChannel(0, TextID, ref materialUniformBuffer));
            texturesChannelNames.Add(TextureChannels.RockDisplacement, "rockDisplacement");
        }*/

        public override int ReadMaterial(int startLine, ref string[] lines)
        {
             float hor_scale = 1, ver_scale = 1;

            if (!lines[startLine + 1].Equals("{\r"))
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
                        AssignTexture2Channel(tokens[1], TextureChannels.GrassDiffuse);
                        Textures[TextureChannels.GrassDiffuse].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.GrassDiffuse].SetMultiply(1, 1, 1, ver_scale);
                        break;

                    case "grassNormal":
                        AssignTexture2Channel(tokens[1], TextureChannels.GrassNormal);
                        Textures[TextureChannels.GrassNormal].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.GrassNormal].SetMultiply(1, 1, 1, ver_scale);
                        break;
                    case "grassDisplacemnt":
                        AssignTexture2Channel(tokens[1], TextureChannels.GrassDisplacement);
                        Textures[TextureChannels.GrassDisplacement].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.GrassDisplacement].SetMultiply(1, 1, 1, ver_scale);
                        break;


                    case "rockDiffuse":
                        AssignTexture2Channel(tokens[1], TextureChannels.RockDiffuse);
                        Textures[TextureChannels.RockDiffuse].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.RockDiffuse].SetMultiply(1, 1, 1, ver_scale);
                        break;

                    case "rockNormal":
                        AssignTexture2Channel(tokens[1], TextureChannels.RockNormal);
                        Textures[TextureChannels.RockNormal].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.RockNormal].SetMultiply(1, 1, 1, ver_scale);
                        break;
                    case "rockDisplacemnt":
                        AssignTexture2Channel(tokens[1], TextureChannels.RockDisplacement);
                        Textures[TextureChannels.RockDisplacement].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.RockDisplacement].SetMultiply(1, 1, 1, ver_scale);
                        break;



                    case "groundDiffuse":
                        AssignTexture2Channel(tokens[1], TextureChannels.GroundDiffuse);
                        Textures[TextureChannels.GroundDiffuse].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.GroundDiffuse].SetMultiply(1, 1, 1, ver_scale);
                        break;

                    case "groundNormal":
                        AssignTexture2Channel(tokens[1], TextureChannels.GroundNormal);
                        Textures[TextureChannels.GroundNormal].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.GroundNormal].SetMultiply(1, 1, 1, ver_scale);
                        break;
                    case "groundDisplacemnt":
                        AssignTexture2Channel(tokens[1], TextureChannels.GroundDisplacement);
                        Textures[TextureChannels.GroundDisplacement].SetTile(hor_scale, hor_scale);
                        Textures[TextureChannels.GroundDisplacement].SetMultiply(1, 1, 1, ver_scale);
                        break;

                }
                i++;
            }

            while (!lines[i + 1].Equals("}"));

            return i + 2;

        }
 
        public override void InitMaterial()
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

            materialUniformBuffer.Create(1);///Создаёт один размеченный выше буфер для материала 


            materialUniformBuffer.UpdateBufferIteam("albedoColor", DefColor);
            materialUniformBuffer.UpdateBufferIteam("ambientColor", DefColor);
            materialUniformBuffer.UpdateBufferIteam("reflectionColor", BlackColor);

            for (int j = 0; j < (int)TextureChannels.TerrainChannelsCount; j++)
            {
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].tileUV", DefUV);
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].offsetUV", DefOffs);
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].multRGBA", DefMult);
            }
        }
 
    }
}
