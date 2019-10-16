
using System.Runtime.InteropServices;

namespace AhoraCore.Core.Buffers.StandartBuffers.IStandartBuffers
{
    public abstract class AStandartBuffer<T>
    {
        int bufferCapasity = 0; // сколько можно добавить

        int bufferFillness = 0; // насколько буфер заполнен

        int itemByteSize = 4;

        public string BufferID;

        public DataCell[] BufferData{ get; protected set;}

        public struct DataCell
        {
            private bool isFilled;

            private T data;

            public bool IsFilled()
            {
                return isFilled;
            }

            public T getValue()
            {
                return data;
            }

            public void setVal(T d)
            {
                data = d;

                isFilled = true;
            }

            public void EraseVal(T d)
            {
                isFilled = false;
            }
        }

        public int IteamByteSize
        {
            get
            {
                return itemByteSize;
            }
            protected set
            {
                itemByteSize = value;
            }
        }

        public int Capacity
        {
            get
            {
                return bufferCapasity;
            }
            protected set
            {
                bufferCapasity = value;
            }
        }

        public int Fillnes
        {
            get
            {
                return bufferFillness;
            }
            protected set
            {
                bufferFillness = value;
            }
        }

        public abstract void CreateBuffer();

        public abstract void CreateBuffer(int cap);

        public abstract void DeleteBuffer();

        public T[] ToArray()
        {
            T[] array = new T[Fillnes];

            for (int i=0; i < Fillnes; i++ )
            {
                array[i] = BufferData[i].getValue();
            }

            return array;
        }

        public AStandartBuffer()
        {
            BufferID = "";
            itemByteSize = Marshal.SizeOf(typeof(T));
        }
    }
}
