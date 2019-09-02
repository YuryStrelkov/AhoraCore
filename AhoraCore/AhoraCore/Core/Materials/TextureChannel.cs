using AhoraCore.Core.Buffers.UniformsBuffer;

namespace AhoraCore.Core.Materials.AbstractMaterial
{
    public class TextureChannel : ATextureChannel<string>
    {
        private string UVtile;
        private string UVshift;
        private string Multipy;

        public TextureChannel(int ChannelOffset, string T_ID, ref UniformsBuffer<string> TextureChannelData) : base(ChannelOffset, T_ID, ref TextureChannelData)
        {
            UVtile=   "channel["+ ChannelOffset + "].tileUV";
            UVshift = "channel[" + ChannelOffset + "].offsetUV";
            Multipy = "channel[" + ChannelOffset + "].multRGBA";

            SetMultiply(1,1,1,0);
            SetOffeset(0, 0);
            SetTile(1, 1);
           
        }

        public TextureChannel(int ChannelOffset, string T_ID, string ShaderAccsess, ref UniformsBuffer<string> TextureChannelData) : base(ChannelOffset, T_ID, ref TextureChannelData)
        {

            UVtile  = ShaderAccsess + "channel[" + ChannelOffset + "].tileUV";
            UVshift = ShaderAccsess + "channel[" + ChannelOffset + "].offsetUV";
            Multipy = ShaderAccsess + "channel[" + ChannelOffset + "].multRGBA";

            SetMultiply(1, 1, 1, 0);
            SetOffeset(0, 0);
            SetTile(1, 1);
            
        }

        // TextureChannels => 0  1  2  3  4  5  6  7    
        // TextureChannels => Tx Ty Ox Oy R  G  B  A    
        public override void SetMultiply(float R_mull, float G_mull, float B_mull, float A_mull)
        {
            TextureChannelData.Bind();
            TextureChannelData.UpdateBufferIteam(Multipy, ChannelOffset*NUMBER_OF_PARAMETRS + 4, new float[] { R_mull, G_mull, B_mull, A_mull });//  ("Channel[" + (int)channel + "].multRGBA", );
        }

        public override void SetOffeset(float x, float y)
        {
            TextureChannelData.Bind();
            TextureChannelData.UpdateBufferIteam(UVshift, ChannelOffset * NUMBER_OF_PARAMETRS + 2, new float[] { x, y });//  ("Channel[" + (int)channel + "].multRGBA", );
        }

        public override void SetTile(float x, float y)
        {
            TextureChannelData.Bind();
            TextureChannelData.UpdateBufferIteam(UVtile, ChannelOffset * NUMBER_OF_PARAMETRS, new float[] { x, y });//  ("Channel[" + (int)channel + "].multRGBA", );
        }
    }
}
