using System;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

namespace AhoraCore.Core.Buffers.IBuffres
{
   
   public class EditableBuffer<T,D> : ABuffer where D: EditableBuffer<T, D> where T : struct
    {
        public int IteamByteSize = Marshal.SizeOf(typeof(T));

        /// <summary>
        /// Очищает буфер
        /// </summary>
        public override void Clear()
        {
            Bind();
            GL.BufferData(BindingTarget, Capacity * IteamByteSize, (IntPtr)0, BufferUsageHint.DynamicDraw);
            Console.WriteLine(GL.GetError().ToString());
        }
        /// <summary>
        /// Дополняет значения буфера новыми значениями слева, принадлежащими buffer
        /// </summary>
        /// <param name="buffer">буфер для слияния</param>
        public void MergeBuffers(EditableBuffer<T, D> buffer)
        {
            if (buffer.Fillnes > Capacity - Fillnes)
            {
                throw new Exception("Unnable to load data to buffer:not enought of space");
            }
            GL.BindBuffer(BufferTarget.CopyReadBuffer, buffer.ID);
            GL.BindBuffer(BufferTarget.CopyWriteBuffer, ID);
            GL.CopyBufferSubData(BufferTarget.CopyReadBuffer, BufferTarget.CopyWriteBuffer, (IntPtr)0, (IntPtr)(Fillnes * IteamByteSize), IteamByteSize);
            GL.BindBuffer(BindingTarget, 0);
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
                throw new Exception("Unnable to load data to buffer:not enought of space");
            }
 
                GL.BufferSubData(BindingTarget, (IntPtr)(Fillnes * IteamByteSize), data.Length * IteamByteSize, data);

                Fillnes += data.Length;
           
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
                throw new Exception("Unnable to load data to buffer:not enought of space");
            }
                GL.BufferSubData(BindingTarget, (IntPtr)(startIdx * IteamByteSize), data.Length * IteamByteSize, data);
                Fillnes += data.Length;
        }
        /// <summary>
        /// включает буфер
        /// </summary>
        public override void Bind()
        {
            GL.BindBuffer(BindingTarget, ID);
        }
        /// <summary>
        /// Создаёт буфер
        /// </summary>
        public override void Create()
        {
            ID = ID == -1 ? GL.GenBuffer() : ID;
        }
        /// <summary>
        /// Создаёт буфер обозначенной ёмкости
        /// </summary>
        /// <param name="capacity">ёмкость</param>
        public override void Create(int capacity)
        {
            Create();
            Bind();
            Capacity = capacity;
            GL.BufferData(BindingTarget, capacity * IteamByteSize, (IntPtr)0, BufferUsageHint.DynamicDraw);
        //    Console.WriteLine(" Buffer ID = " + ID + " as "+ BufferType.ToString() + " creation status " + GL.GetError().ToString());
        }
        /// <summary>
        /// Удаляет буфер
        /// </summary>
        public override void Delete()
        {
            GL.DeleteBuffer(ID);
            ID = -1;
        }
        /// <summary>
        /// Отключает буфер 
        /// </summary>
        public override void Unbind()
        {
            GL.BindBuffer(BindingTarget, 0);  
        }
        /// <summary>
        /// Включает буфер в режиме, соответствующему bindTarget
        /// </summary>
        /// <param name="bindTarget"></param>
        public override void Bind(BufferTarget bindTarget)
        {
            GL.BindBuffer(bindTarget, ID);
        }
    }
}
