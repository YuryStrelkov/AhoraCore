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
