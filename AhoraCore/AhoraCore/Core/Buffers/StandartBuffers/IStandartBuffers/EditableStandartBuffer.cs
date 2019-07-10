using System;

namespace AhoraCore.Core.Buffers.StandartBuffers.IStandartBuffers
{
    public class EditableStandartBuffer<T, D> : AStandartBuffer<T> where D : EditableStandartBuffer<T, D> where T : struct
{
            public void Put(T data)
            {

                if (Fillnes < Capacity)
                {
                    BufferData[Fillnes] = data;
                }
                else
                {
                    EnhanceBuffer((int)(Capacity + 0.2f * Capacity));
                    BufferData[Fillnes] = data;
                }

                Fillnes += 1;

            }

            public void PutDirect(int i,T data)
            {

                if (i < Capacity)
                {
                  ///  Fillnes += BufferData[i]==0?1:0;
                    BufferData[i] = data;
                }
                else
                {
                    EnhanceBuffer(i+1);
                    BufferData[i] = data;
                }
                
            }

            public T Pop(int n)
            {
                return BufferData[n];
            }
   
            /// <summary>
            /// Очищает буфер
            /// </summary>
            /// 

            public void ClearBuffer()
            {
              Array.Clear(BufferData,0,Capacity);
            }
            /// <summary>
            /// Увеличивает ёмкость буфера до enhancedCapacity
            /// </summary>
            /// <param name="enhancedCapacity"> увеличенная ёмкость</param>
            public void EnhanceBuffer(int enhancedCapacity)
            {
                if (enhancedCapacity < Capacity)
                {
                    return;
                }
                EditableStandartBuffer<T, D> tmp = new EditableStandartBuffer<T, D>(enhancedCapacity);
            
                CopyBufferData(tmp, 0, Capacity, 0);

                BufferData = tmp.BufferData;
            
                Capacity = enhancedCapacity;
            }
            /// <summary>
            /// Дополняет значения буфера новыми значениями слева, принадлежащими buffer
            /// </summary>
            /// <param name="buffer">буфер для слияния</param>
            public void MergeBuffers(EditableStandartBuffer<T, D> buffer)
            {
                if (buffer.Fillnes > Capacity - Fillnes)
                {
                    EnhanceBuffer(buffer.Fillnes + Fillnes);
                }
                 Array.Copy(buffer.BufferData,0, BufferData, Capacity, buffer.Capacity);

                 Fillnes = Capacity;
            }
            /// <summary>
            /// Копирует данные буфера в буфер target
            /// </summary>
            /// <param name="target">Буфер куда производится копирование</param>
            /// <param name="source_offset">начальный индекс элемента </param>
            /// <param name="source_length">количество элементов</param>
            /// <param name="target_offset">смещение в  целевом массиве</param>
            public void CopyBufferData(EditableStandartBuffer<T, D> target, int source_offset, int source_length, int target_offset)
            {
                if (source_length == 0)
                {
                    return;
                }
            if (target.Capacity- target.Fillnes < Fillnes)
            {
                target.EnhanceBuffer(target.Fillnes + Fillnes);

                Array.Copy(BufferData, source_offset, target.BufferData, target_offset, source_length);
            }
            else
            {
                Array.Copy(BufferData, source_offset, target.BufferData, target_offset, source_length);
            }

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

                    Array.Copy(data, 0, BufferData, Fillnes, data.Length);

                     Fillnes += data.Length;
                }
                else
                {
                    Array.Copy(data, 0, BufferData, Fillnes, data.Length);

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

                    Array.Copy(data, 0, BufferData, startIdx, data.Length);

                    Fillnes += data.Length;
                }
                else
                {
                    Array.Copy(data, 0, BufferData, startIdx, data.Length);

                    Fillnes += data.Length;
                }
            }

            public override void CreateBuffer()
            {
              BufferData = new T[10000];
              Capacity = 10000;
            }

            /// <summary>
            /// Создаёт буфер обозначенной ёмкости
            /// </summary>
            /// <param name="capacity">ёмкость</param>
            public override void CreateBuffer(int capacity)
            {
                Capacity = capacity;
                BufferData = new T[capacity];
            }

            public override void DeleteBuffer()
            {
               BufferData = null;
            }

            public EditableStandartBuffer():base()
            {
               CreateBuffer();
            }

            public EditableStandartBuffer(int cap) : base()
            {
                CreateBuffer(cap);
            }

    }
    }
