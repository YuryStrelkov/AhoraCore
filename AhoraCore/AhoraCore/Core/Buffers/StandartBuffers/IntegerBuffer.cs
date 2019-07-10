using AhoraCore.Core.Buffers.StandartBuffers.IStandartBuffers;

namespace AhoraCore.Core.Buffers.StandartBuffers
{
    public class IntegerBuffer : EditableStandartBuffer<float, IntegerBuffer>
    {
        public IntegerBuffer():base(10000)
        {

        }
        public IntegerBuffer(int cap) : base(cap)
        {

        }
    }
}
