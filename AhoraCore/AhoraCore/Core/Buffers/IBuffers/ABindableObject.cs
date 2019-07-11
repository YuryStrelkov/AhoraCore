namespace AhoraCore.Core.Buffers.IBuffers
{
    public abstract class ABindableObject<T>:ABindable<T>
    {
        T bindTarget;

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

        public abstract void Bind(T bindTarget);

        public abstract void Create();

        public abstract void Delete();

        public abstract void Unbind();
    }
}
