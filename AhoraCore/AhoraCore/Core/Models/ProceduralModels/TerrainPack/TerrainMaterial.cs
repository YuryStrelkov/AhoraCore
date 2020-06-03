using AhoraCore.Core.Materials.AbstractMaterial;
using Newtonsoft.Json;

namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
    sealed public class TerrainMaterial:AMaterial
    {
        public static int MAX_TEXTURE_CHANNELS_NUMBER = (int)TextureChannels.TerrainChannelsCount;

        public int SubMaterialsCount { get; private set; }

        public TerrainMaterial():base()
        {
            SubMaterialsCount = 0;
        }

        public override void ReadMaterial(JsonTextReader reader)
        {
            float hor_scale = 1;

            float ver_scale = 1;

            while (reader.TokenType != JsonToken.EndObject)
            {
                if (reader.Value == null)
                {
                    reader.Read();
                    continue;
                }
                switch (reader.Value.ToString())
                {
                    case "verticalScale":
                        reader.Read();
                        ver_scale = float.Parse(reader.Value.ToString());
                        reader.Read();
                        break;

                    case "horizontalScale":
                        reader.Read();
                        hor_scale = float.Parse(reader.Value.ToString());
                        reader.Read();
                        break;

                    case "diffuseMap":
                        reader.Read();
                        Texture2ChannelAssign(reader.Value.ToString(), "materials[" + SubMaterialsCount + "].diffuseMap");
                         reader.Read();
                        break;

                    case "normalMap":
                        reader.Read();
                        Texture2ChannelAssign(reader.Value.ToString(), "materials[" + SubMaterialsCount + "].normalMap");
                         reader.Read();
                        break;

                    case "specularMap":
                        reader.Read();
                        Texture2ChannelAssign(reader.Value.ToString(), "materials[" + SubMaterialsCount + "].specularMap");
                        reader.Read();
                        break;

                    case "heightMap":
                        reader.Read();
                        Texture2ChannelAssign(reader.Value.ToString(), "materials[" + SubMaterialsCount + "].heightMap");
                        reader.Read();
                        break;

                    case "reflectGlossMap":
                        reader.Read();
                        Texture2ChannelAssign(reader.Value.ToString(), "materials[" + SubMaterialsCount + "].reflectGlossMap");
                        reader.Read();
                        break;

                    case "transparencyMap":
                        reader.Read();
                        Texture2ChannelAssign(reader.Value.ToString(), "materials[" + SubMaterialsCount + "].transparencyMap");
                        reader.Read();
                        break;
                }

            }
            reader.Read();

            InitSubMat();

            materialUniformBuffer.Bind();

            materialUniformBuffer.UpdateBufferIteam("settings[" + SubMaterialsCount + "].scaling", new float[2] { hor_scale, ver_scale });

            SubMaterialsCount += 1;
      }

        private void InitSubMat()
        {
            materialUniformBuffer.addBufferItem("settings[" + SubMaterialsCount + "].scaling", 4);
            materialUniformBuffer.addBufferItem("settings[" + SubMaterialsCount + "].albedoColor", 4);
            materialUniformBuffer.addBufferItem("settings[" + SubMaterialsCount + "].ambientColor", 4);
            materialUniformBuffer.addBufferItem("settings[" + SubMaterialsCount + "].reflectionColor", 4);

            materialUniformBuffer.Create(1);///Создаёт один размеченный выше буфер для материала 

            materialUniformBuffer.Bind();

            materialUniformBuffer.UpdateBufferIteam("settings[" + SubMaterialsCount + "].scaling", new float[2] { 1, 1 });
            materialUniformBuffer.UpdateBufferIteam("settings[" + SubMaterialsCount + "].albedoColor", DefColor);
            materialUniformBuffer.UpdateBufferIteam("settings[" + SubMaterialsCount + "].ambientColor", DefColor);
            materialUniformBuffer.UpdateBufferIteam("settings[" + SubMaterialsCount + "].reflectionColor", BlackColor);

        ///   Console.WriteLine("_________________");
             //  DebugBuffers.displayBufferData(materialUniformBuffer);
        }

        public override void InitMaterial()
        {

        }


    }
}
