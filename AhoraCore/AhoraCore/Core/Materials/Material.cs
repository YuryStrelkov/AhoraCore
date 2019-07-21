using AhoraCore.Core.Materials.AMaterial;

namespace AhoraCore.Core.Materials
{
   
    public class Material:EditableMaterial
    {
        public static int MAX_TEXTURE_CHANNELS_NUMBER = 8;

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
            ///  materialUniformBuffer.packBuffer();


            materialUniformBuffer.Create(1);///Создаёт один размеченный выше буфер для материала 


            materialUniformBuffer.UpdateBufferIteam("albedoColor", DefColor);
            materialUniformBuffer.UpdateBufferIteam("ambientColor",DefColor);
            materialUniformBuffer.UpdateBufferIteam("reflectionColor", BlackColor);

           /// TextureChannelData.UpdateBufferIteam("Channel[" + ChannelOffset + "].multRGBA", ChannelOffset * 8 + 4, new float[] { R_mull, G_mull, B_mull, A_mull });//  ("Channel[" + (int)channel + "].multRGBA", );
            materialUniformBuffer.UpdateBufferIteam("reflectivity", 0);
            materialUniformBuffer.UpdateBufferIteam("metallness", 0);
            materialUniformBuffer.UpdateBufferIteam("roughness", 0);
            materialUniformBuffer.UpdateBufferIteam("transparency", 0);

            for (int j = 0; j < textures.Count; j++)
            {
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].tileUV",   DefUV);
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].offsetUV", DefOffs);
                materialUniformBuffer.UpdateBufferIteam("channel[" + j + "].multRGBA", DefMult);
            }
        }
    }
}
