using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Buffers.SpecificBuffers
{
    public  class VerticesBuffer : EditableBuffer<float>
    {
        /// <summary>
        /// Список атрибутов вершин буфера
        /// </summary>
        /// 
        public VerticesBuffer() : base()
        {
            BindingTarget = BufferTarget.ArrayBuffer;
            Create();
        }

    }
}
