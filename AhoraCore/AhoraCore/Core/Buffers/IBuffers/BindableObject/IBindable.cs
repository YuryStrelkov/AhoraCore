namespace AhoraCore.Core.Buffers.IBuffers.BindableObject
{
    public interface IBindable
    {
        void Bind();
        
        void Create();

        void Delete();

        void Unbind();

        void Clear();
    }
}
