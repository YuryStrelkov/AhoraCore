using AhoraCore.Core.Materials.AMaterial;

namespace AhoraCore.Core.Materials
{
   
    public class Material:EditableMaterial
    {
        public Material() : base()
        {
            materialUniformBuffer.addBufferItem("AlbedoColor", 4);
            materialUniformBuffer.addBufferItem("AmbientColor", 4);
            materialUniformBuffer.addBufferItem("ReflectionColor", 4);

            materialUniformBuffer.addBufferItem("Reflectivity", 1);
            materialUniformBuffer.addBufferItem("Metallness", 1);
            materialUniformBuffer.addBufferItem("Roughness", 1);
            materialUniformBuffer.addBufferItem("Transparency", 1);

            for (int j = 0; j < (int)TextureChannels.ChannelsCount; j++)
            {
                materialUniformBuffer.addBufferItem("Channel[" + j + "].tileUV", 2);
                materialUniformBuffer.addBufferItem("Channel[" + j + "].offsetUV", 2);
                materialUniformBuffer.addBufferItem("Channel[" + j + "].multRGBA", 4);
            }
            ///  materialUniformBuffer.packBuffer();


            materialUniformBuffer.CreateBuffer(1);///Создаёт один размеченный выше буфер для материала 


            materialUniformBuffer.UpdateBufferIteam("AlbedoColor", DefColor);
            materialUniformBuffer.UpdateBufferIteam("AmbientColor",DefColor);
            materialUniformBuffer.UpdateBufferIteam("ReflectionColor", BlackColor);

           /// TextureChannelData.UpdateBufferIteam("Channel[" + ChannelOffset + "].multRGBA", ChannelOffset * 8 + 4, new float[] { R_mull, G_mull, B_mull, A_mull });//  ("Channel[" + (int)channel + "].multRGBA", );
            materialUniformBuffer.UpdateBufferIteam("Reflectivity", 0);
            materialUniformBuffer.UpdateBufferIteam("Metallness", 0);
            materialUniformBuffer.UpdateBufferIteam("Roughness", 0);
            materialUniformBuffer.UpdateBufferIteam("Transparency", 0);

            for (int j = 0; j < textures.Count; j++)
            {
                materialUniformBuffer.UpdateBufferIteam("Channel[" + j + "].tileUV",   DefUV);
                materialUniformBuffer.UpdateBufferIteam("Channel[" + j + "].offsetUV", DefOffs);
                materialUniformBuffer.UpdateBufferIteam("Channel[" + j + "].multRGBA", DefMult);
            }
        }
    }
}
