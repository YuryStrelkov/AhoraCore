using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace AhoraCore.Core.Buffers.SpecificBuffers
{
    public  class VerticesBuffer : ABuffer, IBufferFunctionality<float, VerticesBuffer>
    {
        /// <summary>
        /// Список атрибутов вершин буфера
        /// </summary>
        public override void BindBuffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
        }

        public override void CreateBuffer()
        {
            ID = ID==-1? GL.GenBuffer(): ID;
        }

        public void CreateBuffer(int capacity)
        {
            CreateBuffer();
            BindBuffer();
            Capacity = capacity;
            GL.BufferData(BufferTarget.ArrayBuffer, capacity * sizeof(float), (IntPtr)0, BufferUsageHint.DynamicDraw);
            Console.WriteLine("Vertices buffer ID = "+ ID +" creation status " + GL.GetError().ToString());

        }

        public override void DeleteBuffer()
        {
            GL.DeleteBuffer(ID);
        }

        public void EnhanceBuffer(int enhancedCapacity)
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

        public override void UnbindBuffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void MergeBuffers(VerticesBuffer buffer)
        {
            if (buffer.Fillnes > Capacity - Fillnes)
            {
                EnhanceBuffer(buffer.Fillnes + Fillnes);
            }
            GL.BindBuffer(BufferTarget.CopyReadBuffer, buffer.ID);
            GL.BindBuffer(BufferTarget.CopyWriteBuffer, ID);
            GL.CopyBufferSubData(BufferTarget.CopyReadBuffer, BufferTarget.CopyWriteBuffer, (IntPtr)0, (IntPtr)(Fillnes * sizeof(float)), sizeof(float));
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            Fillnes = Capacity;
        }

        public void LoadBufferData(float[] data)
        {
            if (data.Length > Capacity - Fillnes)
            {
                EnhanceBuffer(Capacity + data.Length);

                BindBuffer();

                GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(Fillnes * sizeof(float)), data.Length * sizeof(float), data);

                Fillnes += data.Length;
            }
            else
            {

                GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(Fillnes * sizeof(float)), data.Length * sizeof(float), data);

                Fillnes += data.Length;
            }

        }

        public void LoadBufferSubdata(float[] data, int startIdx)
        {
            if (data.Length > Capacity - Fillnes)
            {
                EnhanceBuffer(Capacity + data.Length);

                BindBuffer();

                GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(startIdx * sizeof(float)), data.Length * sizeof(float), data);
             
                Fillnes += data.Length;
            }
            else
            {
                GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(startIdx * sizeof(float)), data.Length * sizeof(float), data);
                Fillnes += data.Length;
            }

         
        }

        public void ClearBuffer()
        {
            BindBuffer();
            GL.BufferData(BufferTarget.ArrayBuffer, Capacity * sizeof(float), (IntPtr)0, BufferUsageHint.DynamicDraw);
            Console.WriteLine(GL.GetError().ToString());
        }

      

        public void CopyBufferData(VerticesBuffer target, int source_offset, int source_length, int target_offset)
        {
            if (source_length == 0)
            {
                return;
            }
            GL.BindBuffer(BufferTarget.CopyReadBuffer, ID);
            GL.BindBuffer(BufferTarget.CopyWriteBuffer, target.ID);
            GL.CopyBufferSubData(BufferTarget.CopyReadBuffer, BufferTarget.CopyWriteBuffer, 
                                (IntPtr)(source_offset * sizeof(float)),
                                (IntPtr)(target_offset * sizeof(float)), (IntPtr)(sizeof(float)* source_length));
            target.Fillnes = target_offset + source_length ;
            GL.BindBuffer(BufferTarget.CopyReadBuffer, 0);
            GL.BindBuffer(BufferTarget.CopyWriteBuffer, 0);
            //DebugBuffers.displayBufferDataVBO(target);
            //Console.WriteLine("-----------VBO--------------");
            //DebugBuffers.displayBufferDataVBO(target);
            //Console.WriteLine("-----------VBO--------------");
        }
        
    }
}
