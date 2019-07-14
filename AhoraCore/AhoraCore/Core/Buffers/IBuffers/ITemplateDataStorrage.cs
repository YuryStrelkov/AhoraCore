namespace AhoraCore.Core.Buffers.IBuffers
{
   public interface ITemplateDataStorrage<KeyType,ValueType>
    {
        void AddItem(KeyType ID, ValueType shader);
    }
}
