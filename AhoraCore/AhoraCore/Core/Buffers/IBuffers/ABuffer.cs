using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Buffers.IBuffres
{
    public abstract class ABuffer : IBuffer
    {
        int bufferID=-1;

        int bufferCapasity=0;//сколько можно добавить

        int bufferFillness=0; //насколько буфер заполнен

        public BufferTarget bufferType;

        public BufferTarget BufferType
        {
            get
            {
                return bufferType;
            }
            protected set
            {
            bufferType = value;
            }
        }


        public int ID
        {
            get
            {
                return bufferID;
            }
            protected set
            {
                bufferID = value;
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

        public abstract void BindBuffer();

        public abstract void CreateBuffer();

        public abstract void CreateBuffer(int cap);

        public abstract void DeleteBuffer();

        public abstract void UnbindBuffer();

     
    }
}
