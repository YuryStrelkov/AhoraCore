using AhoraCore.Core.Buffers.IBuffres;
using System;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Buffers.UniformsBuffer
{
    public class UniformBuffer : ABuffer, IBufferFunctionality<float, UniformBuffer>
    {
        public override void BindBuffer()
        {
            GL.BindBuffer(BufferTarget.UniformBuffer, ID);
        }
        public override void UnbindBuffer()
        {
            GL.BindBuffer(BufferTarget.UniformBuffer, 0);
        }
        public override void CreateBuffer()
        {
            ID = GL.GenBuffer();
        }

        public override void DeleteBuffer()
        {
            GL.DeleteBuffer(ID);
        }

        public void ClearBuffer()
        {
        }

        public void CopyBufferData(UniformBuffer target, int source_offset, int source_length, int target_offset)
        {
        }

        public void CreateBuffer(int capacity)
        {
        }
        

        public void EnhanceBuffer(int enhancedCapacity)
        {
        }

        public void LoadBufferData(float[] data)
        {
        }

        public void LoadBufferSubdata(float[] data, int start)
        {
        }

        public void MergeBuffers(UniformBuffer buffer)
        {
        }

        
    }
}
