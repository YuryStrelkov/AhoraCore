using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Buffers.SpecificBuffers
{
    public class IndecesBuffer: EditableBuffer<int, IndecesBuffer>
    {
        public IndecesBuffer() : base()
        {
            BufferType = BufferTarget.ElementArrayBuffer;
        }

        /// <summary>
        /// Увеличивает ёмкость буфера до enhancedCapacity
        /// </summary>
        /// <param name="enhancedCapacity"> увеличенная ёмкость</param>
        public override void EnhanceBuffer(int enhancedCapacity)
        {
            if (enhancedCapacity < Capacity)
            {
                return;
            }
            IndecesBuffer tmp = new IndecesBuffer();

            tmp.CreateBuffer(enhancedCapacity);

            CopyBufferData(tmp, 0, Fillnes, 0);

            DeleteBuffer();

            ID = tmp.ID;

            Capacity = enhancedCapacity;
        }

    }
}
