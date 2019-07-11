using AhoraCore.Core.Materials.AMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AhoraCore.Core.Buffers.UniformsBuffer;

namespace AhoraCore.Core.Materials
{
    public class TextureChannel : ATextureChannel<string>
    {
        public TextureChannel(int ChannelOffset, Texture t, ref UniformsBuffer<string> TextureChannelData) : base(ChannelOffset, t, ref TextureChannelData)
        {
            SetMultiply(1,1,1,0);
            SetOffeset(0, 0);
            SetTile(1, 1);
        }
        // TextureChannels => 0  1  2  3  4  5  6  7    
        // TextureChannels => Tx Ty Ox Oy R  G  B  A    
        public override void SetMultiply(float R_mull, float G_mull, float B_mull, float A_mull)
        {
            TextureChannelData.UpdateBufferIteam("Channel[" + ChannelOffset + "].multRGBA", ChannelOffset*8 + 4, new float[] { R_mull, G_mull, B_mull, A_mull });//  ("Channel[" + (int)channel + "].multRGBA", );
        }

        public override void SetOffeset(float x, float y)
        {
            TextureChannelData.UpdateBufferIteam("Channel[" + ChannelOffset + "].offsetUV", ChannelOffset * 8 + 2, new float[] { x, y });//  ("Channel[" + (int)channel + "].multRGBA", );
        }

        public override void SetTile(float x, float y)
        {
            TextureChannelData.UpdateBufferIteam("Channel[" + ChannelOffset + "].tileUV", ChannelOffset * 8 , new float[] { x, y });//  ("Channel[" + (int)channel + "].multRGBA", );
        }
    }
}
