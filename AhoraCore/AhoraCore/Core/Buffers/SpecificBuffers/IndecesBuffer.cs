using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;
using System;



namespace AhoraCore.Core.Buffers.SpecificBuffers
{
    public class IndecesBuffer:ABuffer, IBufferFunctionality<int, IndecesBuffer>
    {
        public override void BindBuffer()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
        }

        public override void CreateBuffer()
        {
            ID = ID == -1 ? GL.GenBuffer() : ID;
        }

        public void CreateBuffer(int capacity)
        {
            CreateBuffer();
            BindBuffer();
            Capacity = capacity;
            GL.BufferData(BufferTarget.ElementArrayBuffer, capacity * sizeof(int), (IntPtr)0, BufferUsageHint.DynamicDraw);
            Console.WriteLine("Indeces buffer ID = " + ID + " creation status " + GL.GetError().ToString());
            // UnbindBuffer();
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
            IndecesBuffer tmp = new IndecesBuffer();

            tmp.CreateBuffer(enhancedCapacity);

            CopyBufferData(tmp, 0, Fillnes, 0);

            DeleteBuffer();

            ID = tmp.ID;

            Capacity = enhancedCapacity;
        }

        public override void UnbindBuffer()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void MergeBuffers(IndecesBuffer buffer)
        {
            if (buffer.Fillnes > Capacity - Fillnes)
            {
                EnhanceBuffer(buffer.Fillnes + Fillnes);
            }
            GL.BindBuffer(BufferTarget.CopyReadBuffer, buffer.ID);
            GL.BindBuffer(BufferTarget.CopyWriteBuffer, ID);
            GL.CopyBufferSubData(BufferTarget.CopyReadBuffer, BufferTarget.CopyWriteBuffer, (IntPtr)0, (IntPtr)(Fillnes * sizeof(int)), sizeof(int));
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            Fillnes = Capacity;
        }

        public void LoadBufferData(int[] data)
        {
            if (data.Length > Capacity - Fillnes)
            {
                EnhanceBuffer(Capacity + data.Length);

                BindBuffer();
                GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)(Fillnes * sizeof(uint)), data.Length * sizeof(uint), data);
                //GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(int), data, BufferUsageHint.StaticDraw);
                Fillnes += data.Length;
            }
            else
            {
                //GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(int), data, BufferUsageHint.StaticDraw);
                GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)(Fillnes * sizeof(uint)), data.Length * sizeof(uint), data);
                Fillnes += data.Length;
            }
            
        }

        public void LoadBufferSubdata(int[] data, int startIdx)
        {
            if (data.Length > Capacity - Fillnes)
            {
                EnhanceBuffer(Capacity + data.Length);

                BindBuffer();

                GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)(startIdx * sizeof(uint)), data.Length * sizeof(uint), data);
                Fillnes += data.Length;
            }
            else
            {
                GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)(startIdx * sizeof(uint)), data.Length * sizeof(uint), data);
              
                Fillnes += data.Length;
            }

             
        }

        public void ClearBuffer()
        {
            BindBuffer();
            GL.BufferData(BufferTarget.ElementArrayBuffer, Capacity * sizeof(int), (IntPtr)0, BufferUsageHint.DynamicDraw);
            Console.WriteLine(GL.GetError().ToString());
        }



        public void CopyBufferData(IndecesBuffer target, int source_offset, int source_length, int target_offset)
        {
            if (source_length == 0)
            {
                return;
            }
            GL.BindBuffer(BufferTarget.CopyReadBuffer, ID);
            GL.BindBuffer(BufferTarget.CopyWriteBuffer, target.ID);
            GL.CopyBufferSubData(BufferTarget.CopyReadBuffer, BufferTarget.CopyWriteBuffer,
                                (IntPtr)(source_offset * sizeof(int)),
                                (IntPtr)(target_offset * sizeof(int)), (IntPtr)(source_length * sizeof(int)));
            target.Fillnes = target_offset + source_length ;
            GL.BindBuffer(BufferTarget.CopyReadBuffer, 0);
            GL.BindBuffer(BufferTarget.CopyWriteBuffer, 0);
            //Console.WriteLine("-----------ITMP--------------");
            //DebugBuffers.displayBufferDataIBO(target);
            //Console.WriteLine("-----------ITMP--------------");
        }

    }
}
