using System;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

namespace AhoraCore.Core.Buffers.IBuffres
{
   
   public abstract class EditableBuffer<T,D> : ABuffer where D: EditableBuffer<T, D> where T : struct
    {
        public int IteamByteSize = Marshal.SizeOf(typeof(T));

        /// <summary>
        /// Очищает буфер
        /// </summary>
        public void ClearBuffer()
        {
            BindBuffer();
            GL.BufferData(BufferType, Capacity * IteamByteSize, (IntPtr)0, BufferUsageHint.DynamicDraw);
            Console.WriteLine(GL.GetError().ToString());
        }


        public abstract  void EnhanceBuffer(int enhancedCapacity);

        /// <summary>
        /// Дополняет значения буфера новыми значениями слева, принадлежащими buffer
        /// </summary>
        /// <param name="buffer">буфер для слияния</param>
        public void MergeBuffers(EditableBuffer<T, D> buffer)
        {
            if (buffer.Fillnes > Capacity - Fillnes)
            {
                EnhanceBuffer(buffer.Fillnes + Fillnes);
            }
            GL.BindBuffer(BufferTarget.CopyReadBuffer, buffer.ID);
            GL.BindBuffer(BufferTarget.CopyWriteBuffer, ID);
            GL.CopyBufferSubData(BufferTarget.CopyReadBuffer, BufferTarget.CopyWriteBuffer, (IntPtr)0, (IntPtr)(Fillnes * IteamByteSize), IteamByteSize);
            GL.BindBuffer(BufferType, 0);
            Fillnes = Capacity;
        }
        /// <summary>
        /// Копирует данные буфера
        /// </summary>
        /// <param name="target">Буфер куда производится копирование</param>
        /// <param name="source_offset">начальный индекс элемента </param>
        /// <param name="source_length">количество элементов</param>
        /// <param name="target_offset">смещение в  целевом массиве</param>
        public void CopyBufferData(EditableBuffer<T, D> target, int source_offset, int source_length, int target_offset)
        {
            if (source_length == 0)
            {
                return;
            }
            GL.BindBuffer(BufferTarget.CopyReadBuffer, ID);
            GL.BindBuffer(BufferTarget.CopyWriteBuffer, target.ID);
            GL.CopyBufferSubData(BufferTarget.CopyReadBuffer, BufferTarget.CopyWriteBuffer,
                                (IntPtr)(source_offset * IteamByteSize),
                                (IntPtr)(target_offset * IteamByteSize), (IntPtr)(IteamByteSize * source_length));
            target.Fillnes = target_offset + source_length;
            GL.BindBuffer(BufferTarget.CopyReadBuffer, 0);
            GL.BindBuffer(BufferTarget.CopyWriteBuffer, 0);
            //DebugBuffers.displayBufferDataVBO(target);
            //Console.WriteLine("-----------VBO--------------");
            //DebugBuffers.displayBufferDataVBO(target);
            //Console.WriteLine("-----------VBO--------------");
        }
        /// <summary>
        /// Загружает данные в буфер 
        /// </summary>
        /// <param name="data">массив данных</param>
        public void LoadBufferData(T[] data)
        {
            if (data.Length > Capacity - Fillnes)
            {
                EnhanceBuffer(Capacity + data.Length);

                BindBuffer();

                GL.BufferSubData(BufferType, (IntPtr)(Fillnes * IteamByteSize), data.Length * IteamByteSize, data);

                Fillnes += data.Length;
            }
            else
            {

                GL.BufferSubData(BufferType, (IntPtr)(Fillnes * IteamByteSize), data.Length * IteamByteSize, data);

                Fillnes += data.Length;
            }

        }
        /// <summary>
        /// Загружает данные в буфер начиная с определённой позиции
        /// </summary>
        /// <param name="data">массив данных</param>
        /// <param name="start">позиция смещения </param>
        public void LoadBufferSubdata(T[] data, int startIdx)
        {
            if (data.Length > Capacity - Fillnes)
            {
                EnhanceBuffer(Capacity + data.Length);

                BindBuffer();

                GL.BufferSubData(BufferType, (IntPtr)(startIdx * IteamByteSize), data.Length * IteamByteSize, data);

                Fillnes += data.Length;
            }
            else
            {
                GL.BufferSubData(BufferType, (IntPtr)(startIdx * IteamByteSize), data.Length * IteamByteSize, data);
                Fillnes += data.Length;
            }
        }

        public override void BindBuffer()
        {
            GL.BindBuffer(BufferType, ID);
        }

        public override void CreateBuffer()
        {
            ID = ID == -1 ? GL.GenBuffer() : ID;
        }

        /// <summary>
        /// Создаёт буфер обозначенной ёмкости
        /// </summary>
        /// <param name="capacity">ёмкость</param>
        public override void CreateBuffer(int capacity)
        {
            CreateBuffer();
            BindBuffer();
            Capacity = capacity;
            GL.BufferData(BufferType, capacity * IteamByteSize, (IntPtr)0, BufferUsageHint.DynamicDraw);
            Console.WriteLine(" Buffer ID = " + ID + " as "+ BufferType.ToString() + " creation status " + GL.GetError().ToString());
        }

        public override void DeleteBuffer()
        {
            GL.DeleteBuffer(ID);
        }

        public override void UnbindBuffer()
        {
            GL.BindBuffer(BufferType, 0); ;
        }
    }
}
