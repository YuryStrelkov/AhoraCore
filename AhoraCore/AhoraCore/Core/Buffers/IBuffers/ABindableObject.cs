using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.Buffers.IBuffers
{
    public abstract class ABindableObject
    {
        int objID = -1;
        
        public int ID
        {
            get
            {
                return objID;
            }
            protected set
            {
                objID = value;
            }
        }

        public abstract void Bind();

        public abstract void Create();

        public abstract void Delete();

        public abstract void Unbind();
    }
}
