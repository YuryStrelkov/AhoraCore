using AhoraCore.Core.Buffers.UniformsBuffer;

namespace AhoraCore.Core.Materials.AbstractMaterial
{
    public class TextureChannel : ATextureChannel<string>
    {
        public TextureChannel(int ChannelOffset, string T_ID, ref UniformsBuffer<string> TextureChannelData) : base(ChannelOffset, T_ID, ref TextureChannelData)
        {
            ShaderAccsessString = "channel[";
            SetMultiply(1,1,1,0);
            SetOffeset(0, 0);
            SetTile(1, 1);
           
        }

        public TextureChannel(int ChannelOffset, string ShaderAccsess, string T_ID, ref UniformsBuffer<string> TextureChannelData) : base(ChannelOffset, T_ID, ref TextureChannelData)
        {
            ShaderAccsessString = ShaderAccsess;
            SetMultiply(1, 1, 1, 0);
            SetOffeset(0, 0);
            SetTile(1, 1);
            
        }

        // TextureChannels => 0  1  2  3  4  5  6  7    
        // TextureChannels => Tx Ty Ox Oy R  G  B  A    
        public override void SetMultiply(float R_mull, float G_mull, float B_mull, float A_mull)
        {
            TextureChannelData.Bind();
            TextureChannelData.UpdateBufferIteam( ShaderAccsessString + ChannelOffset + "].multRGBA", ChannelOffset*NUMBER_OF_PARAMETRS + 4, new float[] { R_mull, G_mull, B_mull, A_mull });//  ("Channel[" + (int)channel + "].multRGBA", );
        }

        public override void SetOffeset(float x, float y)
        {
            TextureChannelData.Bind();
            TextureChannelData.UpdateBufferIteam( ShaderAccsessString + ChannelOffset + "].offsetUV", ChannelOffset * NUMBER_OF_PARAMETRS + 2, new float[] { x, y });//  ("Channel[" + (int)channel + "].multRGBA", );
        }

        public override void SetTile(float x, float y)
        {
            TextureChannelData.Bind();
            TextureChannelData.UpdateBufferIteam( ShaderAccsessString + ChannelOffset + "].tileUV", ChannelOffset * NUMBER_OF_PARAMETRS, new float[] { x, y });//  ("Channel[" + (int)channel + "].multRGBA", );
        }
    }
}
