namespace AhoraCore.Core.Buffers.IBuffers
{
    public interface IDataStorrage<T>
    {

        void AddItem(T geometryID, float[] verticesData, int[] indecesDtata);

        void ResolveKeys(T geometryID);

        void RemoveItem(T geometryID);

        void MergeItems(T[] ItemIDs);

        void ClearStorrage();

        void DeleteStorrage();

    }
}
