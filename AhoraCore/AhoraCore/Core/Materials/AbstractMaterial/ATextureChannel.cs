using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Buffers.UniformsBuffer;

namespace AhoraCore.Core.Materials.AbstractMaterial
{
    public abstract class ATextureChannel<T>
    {
        public static int NUMBER_OF_PARAMETRS = 8;

        protected int ChannelOffset = 0;

        public string TextureID { get; protected set; }
        
        public string ShaderAccsessString { get; protected set; }

        protected UniformsBuffer<T> TextureChannelData;

        public Texture Texture
        {
            get
            {
                return TextureStorrage.Textures.GetItem(TextureID);
            }
      
        }

        public abstract void SetOffeset(float x, float y);

        public abstract void SetMultiply(float R_mull, float G_mull, float B_mull, float A_mull);

        public abstract void SetTile(float x, float y);




        public ATextureChannel(int ChannelOffset, string tID, ref UniformsBuffer<T> TextureChannelData)
        {
            TextureID = tID;

            this.ChannelOffset = ChannelOffset;

            this.TextureChannelData = TextureChannelData;
        }
    }
}
