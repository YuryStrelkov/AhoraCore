using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;
using System;



namespace AhoraCore.Core.Buffers.SpecificBuffers
{
    public class IndecesBuffer: EditableBuffer<int, IndecesBuffer>
    {
        public IndecesBuffer() : base()
        {
            BufferType = BufferTarget.ElementArrayBuffer;
        }

    }
}
