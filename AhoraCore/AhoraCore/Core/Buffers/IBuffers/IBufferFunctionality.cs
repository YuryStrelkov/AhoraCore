namespace AhoraCore.Core.Buffers.IBuffres
{
   
   public interface IBufferFunctionality<T,D> where D:ABuffer
    {
        /// <summary>
        /// Создаёт буфер обозначенной ёмкости
        /// </summary>
        /// <param name="capacity">ёмкость</param>
        void CreateBuffer(int capacity);
        /// <summary>
        /// Очищает буфер
        /// </summary>
          void ClearBuffer();
        /// <summary>
        /// Увеличивает ёмкость буфера до enhancedCapacity
        /// </summary>
        /// <param name="enhancedCapacity"> увеличенная ёмкость</param>
        void EnhanceBuffer(int enhancedCapacity);
        /// <summary>
        /// Дополняет значения буфера новыми значениями слева, принадлежащими buffer
        /// </summary>
        /// <param name="buffer">буфер для слияния</param>
          void MergeBuffers(D buffer);
        /// <summary>
        /// Копирует данные буфера
        /// </summary>
        /// <param name="target">Буфер куда производится копирование</param>
        /// <param name="source_offset">начальный индекс элемента </param>
        /// <param name="source_length">количество элементов</param>
        /// <param name="target_offset">смещение в  целевом массиве</param>
        void CopyBufferData(D target, int source_offset, int source_length, int target_offset);
        /// <summary>
        /// Загружает данные в буфер 
        /// </summary>
        /// <param name="data">массив данных</param>
        void LoadBufferData(T[] data);
        /// <summary>
        /// Загружает данные в буфер начиная с определённой позиции
        /// </summary>
        /// <param name="data">массив данных</param>
        /// <param name="start">позиция смещения </param>
        void LoadBufferSubdata(T[] data,int start);
        
    }
}
