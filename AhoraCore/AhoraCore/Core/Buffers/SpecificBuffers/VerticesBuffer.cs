using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Buffers.SpecificBuffers
{
    public  class VerticesBuffer : EditableBuffer<float, VerticesBuffer>
    {
        /// <summary>
        /// Список атрибутов вершин буфера
        /// </summary>
        /// 
        public VerticesBuffer() : base()
        {
            BufferType = BufferTarget.ArrayBuffer;
        }

        public override void EnhanceBuffer(int enhancedCapacity)
        {
            if (enhancedCapacity < Capacity)
            {
                return;
            }
            VerticesBuffer tmp = new VerticesBuffer();

            tmp.CreateBuffer(enhancedCapacity);

            CopyBufferData(tmp, 0, Fillnes, 0);

            DeleteBuffer();

            ID = tmp.ID;

            Capacity = enhancedCapacity;
        }
    }
}
