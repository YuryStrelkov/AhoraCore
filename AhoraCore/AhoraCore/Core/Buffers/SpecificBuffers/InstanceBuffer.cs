using System;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace AhoraCore.Core.Buffers.SpecificBuffers
{
    public sealed class InstanceBuffer : EditableBuffer<float>,IAttribyteable
    {

        public int AttribStartIdx { get;protected set; }
        
        public InstanceBuffer(int attrOfset) : base()
        {
            BindingTarget = BufferTarget.ArrayBuffer;
            AttribStartIdx = attrOfset;
        }
        
        public void DisableAttribytes()
        {
            GL.DisableVertexAttribArray(AttribStartIdx);
            GL.DisableVertexAttribArray(AttribStartIdx + 1);
            GL.DisableVertexAttribArray(AttribStartIdx + 2);
            GL.DisableVertexAttribArray(AttribStartIdx + 3);
        }

        public void EnableAttribytes()
        {
            GL.EnableVertexAttribArray(AttribStartIdx);
            GL.EnableVertexAttribArray(AttribStartIdx + 1);
            GL.EnableVertexAttribArray(AttribStartIdx + 2);
            GL.EnableVertexAttribArray(AttribStartIdx + 3);
        }

        /// <summary>
        /// Размечает 16 идущих друг за другом чисел float 
        /// </summary>

        public void MarkBufferAttributePointer(int attrType, int attribID, int sampleSize, int from)
        {
            GL.EnableVertexAttribArray(attribID);
            GL.VertexAttribPointer(attribID, sampleSize, (VertexAttribPointerType)attrType, false, sizeof(float) * 16, from);
            GL.VertexAttribDivisor(attribID, 1);
        }

        public void MarkBufferAttributePointers(int VericesAttribytesMap)
        {
            int offset = 0;

            for (int i = 0; i < 4; i++)
            {
                /*GL.EnableVertexAttribArray(AttribStartIdx + i);
                GL.VertexAttribPointer(AttribStartIdx + i, 4, VertexAttribPointerType.Float, false, sizeof(float) * 16, offset * 4);
                GL.VertexAttribDivisor(AttribStartIdx + i, 1);*/
                MarkBufferAttributePointer((int)VertexAttribPointerType.Float, AttribStartIdx + i, 4 , offset * 4);
                offset += 4;
            }
        }
    }
}
