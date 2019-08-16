using AhoraCore.Core.Buffers.UniformsBuffer;
using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.CES.ICES
{
    public interface IUniformBufferedObject
    {
        string BufferName { get; }

        bool IsBuffered { get; }

        void ConfirmBuffer();

        void ConfirmBuffer(int capacity);

        void EnableBuffering(string bufferName);

        void MarkBuffer(string[] itemNames, int[] itemLengths);

        void MarkBufferItem(string itemName, int itemLength);

        void SetBindigLocation(UniformBindingsLocations location);

        void UpdateBufferIteam(string ItemName, float[] data);
    }
}