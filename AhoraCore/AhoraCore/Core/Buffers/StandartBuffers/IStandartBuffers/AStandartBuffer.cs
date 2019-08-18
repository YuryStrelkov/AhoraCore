
using System.Runtime.InteropServices;

namespace AhoraCore.Core.Buffers.StandartBuffers.IStandartBuffers
{
    public abstract class AStandartBuffer<T>
    {
        int bufferCapasity = 0; // сколько можно добавить

        int bufferFillness = 0; // насколько буфер заполнен

        public string BufferID;

        public T[] BufferData{ get; protected set;}

        public int IteamByteSize
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

        public AStandartBuffer()
        {
            BufferID = "";
            IteamByteSize = Marshal.SizeOf(typeof(T));
        }
    }
}
