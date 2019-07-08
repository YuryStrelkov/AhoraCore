using System;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;


namespace AhoraCore.Core.Buffers.SpecificBuffers
{
    public class InstanceBuffer : EditableBuffer<float, InstanceBuffer>, IAttribyteable
    {
        public InstanceBuffer() : base()
        {
            BufferType = BufferTarget.ArrayBuffer;
        }

        public void DisableAttribytes()
        {
        }

        public void EnableAttribytes()
        {
        }

        public void MarkBufferAttributePointer(int attrType, int attribID, int sampleSize, int from)
        {
        }

        public void MarkBufferAttributePointers(int VericesAttribytesMap)
        {
        }
    }
}
