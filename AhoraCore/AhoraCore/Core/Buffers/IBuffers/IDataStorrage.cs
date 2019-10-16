namespace AhoraCore.Core.Buffers.IBuffers
{
    public interface IDataStorrage<T>
    {
        T LastKey { get; }

        void RemoveItem(T geometryID);

        void ClearStorrage();

        void DeleteStorrage();

    }
}
