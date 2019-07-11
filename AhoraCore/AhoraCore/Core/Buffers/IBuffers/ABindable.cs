namespace AhoraCore.Core.Buffers.IBuffers
{
    public interface ABindable<T>
    {
        void Bind();

        void Bind(T bindTarget);

        void Create();

        void Delete();

        void Unbind();
    }
}
