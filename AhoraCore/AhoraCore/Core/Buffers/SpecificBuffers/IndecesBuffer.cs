using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Buffers.SpecificBuffers
{
    public class IndecesBuffer: EditableBuffer<int>
    {
        public IndecesBuffer() : base()
        {
            BindingTarget = BufferTarget.ElementArrayBuffer;
        }

        /// <summary>
        /// Увеличивает ёмкость буфера до enhancedCapacity
        /// </summary>
        /// <param name="enhancedCapacity"> увеличенная ёмкость</param>
 

    }
}
