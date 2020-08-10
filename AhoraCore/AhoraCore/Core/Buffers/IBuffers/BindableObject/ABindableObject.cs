using AhoraCore.Core.Buffers.IBuffers.BindableObject;
using System;

namespace AhoraCore.Core.Buffers.IBuffers
{
    public abstract class ABindableObject<T>:IBindable,IEquatable<ABindableObject<T>>
    {
        T bindTarget;

        int objID;

        public T BindingTarget
        {
            get
            {
                return bindTarget;
            }
             set
            {
                bindTarget = value;
            }
        }
                
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

        public bool Equals(ABindableObject<T> binable)
        {
            return binable.ID == ID;
        }

        public ABindableObject()
        {
            objID = -1;

            BindingTarget = default(T);
        }

        public abstract void Bind(T bindTarget);

        public abstract void Bind();

        public abstract void Create();

        public abstract void Delete();

        public abstract void Unbind();

        public abstract void Clear();
    }
}
