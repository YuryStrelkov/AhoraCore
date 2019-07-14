namespace AhoraCore.Core.Buffers.IBuffers
{
    public interface IDataStorrage<T>
    {
        void RemoveItem(T geometryID);

        void ClearStorrage();

        void DeleteStorrage();

    }
}
