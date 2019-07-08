namespace AhoraCore.Core.Buffers.IBuffers
{
         public interface IEditMarkedAttribyte
    {
            void MarkBufferAttributePointer(int attrType, int attribID, int sampleSize, int from);
            void MarkBufferAttributePointers(int VericesAttribytesMap);
    }
   
}
