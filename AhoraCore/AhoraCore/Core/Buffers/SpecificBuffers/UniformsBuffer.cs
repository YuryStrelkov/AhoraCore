using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Buffers.UniformsBuffer
{
    public class UniformBuffer : EditableBuffer<float, UniformBuffer>, IAttribyteable
    {
        public UniformBuffer() : base()
        {
            BufferType = BufferTarget.UniformBuffer;
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
