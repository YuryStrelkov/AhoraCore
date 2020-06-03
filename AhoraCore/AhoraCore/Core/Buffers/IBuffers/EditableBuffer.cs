using System;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace AhoraCore.Core.Buffers.IBuffres
{

   
   public class EditableBuffer<T> : ABuffer  where T : struct
    {
        private BufferUsageHint drawMode;

        /// <summary>
        /// Очищает буфер
        /// </summary>

        public override void Clear()
        {
            lock (mutex)
            {
                Bind();
                GL.BufferData(BindingTarget, Capacity * IteamByteSize, (IntPtr)0, drawMode);
                Console.WriteLine(GL.GetError().ToString());
            }
        }
        
        /// <summary>
        /// Дополняет значения буфера новыми значениями слева, принадлежащими buffer
        /// </summary>
        /// <param name="buffer">буфер для слияния</param>
        public void MergeBuffers(EditableBuffer<T> buffer)
        {
            MergeBuffers(buffer, Fillnes);
        }

        public void MergeBuffers(EditableBuffer<T> buffer,int writeOffset)
        {
            MergeBuffers(buffer, 0,  writeOffset);
        }

        public void MergeBuffers(EditableBuffer<T> buffer, int readOffset, int writeOffset)
        {
            MergeBuffers( buffer, readOffset, writeOffset, buffer.Fillnes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MergeBuffers(EditableBuffer<T> buffer, int readOffset, int writeOffset,int elementsToMerge)
        {
            lock (mutex)
            {
                if (Capacity - writeOffset <= elementsToMerge)
                {
                    EnhanceBufferCapcity(Capacity + buffer.Fillnes);
                }
                GL.BindBuffer(BufferTarget.CopyReadBuffer, buffer.ID);
                GL.BindBuffer(BufferTarget.CopyWriteBuffer, ID);
                GL.CopyBufferSubData(BufferTarget.CopyReadBuffer, BufferTarget.CopyWriteBuffer,
                                                                 (IntPtr)(readOffset * IteamByteSize),
                                                                 (IntPtr)(writeOffset * IteamByteSize),
                                                                 elementsToMerge * IteamByteSize);
                GL.BindBuffer(BindingTarget, 0);
                Fillnes = writeOffset + elementsToMerge;
            }
        }
        
        /// <summary>
        /// Копирует данные буфера
        /// </summary>
        /// <param name="target">Буфер куда производится копирование</param>
        /// <param name="source_offset">начальный индекс элемента </param>
        /// <param name="source_length">количество элементов</param>
        /// <param name="target_offset">смещение в  целевом массиве</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyBufferData(EditableBuffer<T> target_Buffer, int write_target_offset, int read_source_offset, int read_source_length)
        {
            if (read_source_length == 0)
            {
                return;
            }

            if (Fillnes> target_Buffer.Capacity)
            {
                target_Buffer.EnhanceBufferCapcity(Fillnes + target_Buffer.Capacity);
            }

            GL.BindBuffer(BufferTarget.CopyReadBuffer, ID);

            GL.BindBuffer(BufferTarget.CopyWriteBuffer, target_Buffer.ID);

            GL.CopyBufferSubData(BufferTarget.CopyReadBuffer, BufferTarget.CopyWriteBuffer,
                                (IntPtr)(write_target_offset * IteamByteSize),
                                (IntPtr)(read_source_offset * IteamByteSize), (IntPtr)(IteamByteSize * read_source_length));

            target_Buffer.Fillnes = read_source_offset + read_source_length;

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LoadBufferData(T[] data)  
        {
            lock (mutex)
            {
                if (data.Length > Capacity - Fillnes)
                {
                    EnhanceBufferCapcity(Capacity + data.Length);
                }
                GL.BufferSubData(BindingTarget, (IntPtr)(Fillnes * IteamByteSize), data.Length * IteamByteSize, data);

                Fillnes += data.Length;
            }
        }
  
        /// <summary>
        /// Загружает данные в буфер начиная с определённой позиции
        /// </summary>
        /// <param name="data">массив данных</param>
        /// <param name="start">позиция смещения </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LoadBufferSubdata(T[] data, int startIdx)
        {
            lock (mutex)
            {
                GL.BufferSubData(BindingTarget, (IntPtr)(startIdx * IteamByteSize), data.Length * IteamByteSize, data);
                Fillnes = startIdx + data.Length > Fillnes ? startIdx + data.Length : Fillnes;
            }
        }
   
        /// <summary>
        /// включает буфер
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Bind()
        {
            lock (mutex)
            {
                GL.BindBuffer(BindingTarget, ID);
               // IsBinded = true;
            }
        }
    
        /// <summary>
        /// Создаёт буфер
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Create()
        {
            lock (mutex)
            {
                IteamByteSize = Marshal.SizeOf(typeof(T));
                ID = ID == -1 ? GL.GenBuffer() : ID;
            }
        }

        /// <summary>
        /// Создаёт буфер обозначенной ёмкости
        /// </summary>
        /// <param name="capacity">ёмкость</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Create(int capacity)
        {
            Create(capacity, BufferUsageHint.StaticDraw);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Create(int capacity, BufferUsageHint drawMode)
        {
            lock (mutex)
            {
                Create();

                Bind();

                Capacity = capacity;

                this.drawMode = drawMode;

                GL.BufferData(BindingTarget, capacity * IteamByteSize, (IntPtr)0, drawMode);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Create(T[] data)
        {
            Create(data, BufferUsageHint.StaticDraw);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Create(T[] data, BufferUsageHint drawMode)
        {
            lock (mutex)
            {
                Create();

                Bind();

                Capacity = data.Length;

                Fillnes = data.Length;

                this.drawMode = drawMode;

                GL.BufferSubData(BindingTarget, (IntPtr)(0), data.Length * IteamByteSize, data);
            }

        }

        /// <summary>
        /// Удаляет буфер
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Delete()
        {
            GL.DeleteBuffer(ID);
            ID = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnhanceBufferCapcity(int newCap)
        {
            if (newCap<=Capacity)
            {
                return;
            }

            EditableBuffer<T> enhanced = new EditableBuffer<T>();

            enhanced.BindingTarget = BindingTarget;

            enhanced.Create(newCap,drawMode);
            
            enhanced.MergeBuffers(this);

            Delete();

            Capacity = newCap;

            ID = enhanced.ID;

            Fillnes = enhanced.Fillnes;

        }

        /// <summary>
        /// Отключает буфер 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Unbind()
        {
            GL.BindBuffer(BindingTarget, 0);  
        }

        /// <summary>
        /// Включает буфер в режиме, соответствующему bindTarget
        /// </summary>
        /// <param name="bindTarget"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Bind(BufferTarget bindTarget)
        {
            lock (mutex)
            {
                GL.BindBuffer(bindTarget, ID);
            }
        }

        public EditableBuffer():base()
        {
            drawMode = BufferUsageHint.StaticDraw;
        }

    }
}
