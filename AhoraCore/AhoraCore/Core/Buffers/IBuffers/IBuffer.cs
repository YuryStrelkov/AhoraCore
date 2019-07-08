namespace AhoraCore.Core.Buffers.IBuffres
{
    public interface IBuffer
    {
        void BindBuffer();

        void UnbindBuffer();

        void CreateBuffer();

        int ID { get;}

        int Capacity { get; }

        int Fillnes { get; }

        void DeleteBuffer();
    }
}
