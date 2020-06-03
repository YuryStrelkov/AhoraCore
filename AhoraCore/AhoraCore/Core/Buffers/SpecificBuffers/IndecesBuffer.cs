using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Buffers.SpecificBuffers
{
    public sealed class IndecesBuffer: EditableBuffer<int>
    {
        public IndecesBuffer() : base()
        {
            BindingTarget = BufferTarget.ElementArrayBuffer;
        }
 
 

    }
}
