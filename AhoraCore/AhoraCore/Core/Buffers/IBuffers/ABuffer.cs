using AhoraCore.Core.Buffers.IBuffers;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Buffers.IBuffres
{
    public abstract class ABuffer : ABindableObject<BufferTarget>
    {
        int bufferCapasity=0;//сколько можно добавить

        int bufferFillness=0; //насколько буфер заполнен

        bool binded = false;

        public bool IsBinded
        {
            get { return binded; }

            protected set { binded = value; }
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
  
        public abstract void Create(int cap);
      
    }
}
