using AhoraCore.Core.Buffers.IBuffers;
using OpenTK.Graphics.OpenGL;
 

namespace AhoraCore.Core.Buffers.IBuffres
{
 
    public abstract class ABuffer : ABindableObject<BufferTarget> 
    {
        protected object mutex;

        int bufferCapasity; //сколько можно добавить

        int bufferFillness; //насколько буфер заполнен
        
        public int IteamByteSize { get; protected set; }
  
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
  
        public abstract void Create(int cap);

        public ABuffer() : base()
        {
            mutex = new object();
            bufferCapasity = 0;
            bufferFillness = 0;
            IteamByteSize = 0;
        }


    }
}
