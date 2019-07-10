using AhoraCore.Core.Buffers.StandartBuffers.IStandartBuffers;


namespace AhoraCore.Core.Buffers.StandartBuffers
{
   public  class FloatBuffer: EditableStandartBuffer<float, FloatBuffer>
    {
        public FloatBuffer():base(10000)
        {

        }
        public FloatBuffer(int cap) : base(cap)
        {

        }
    }
}
