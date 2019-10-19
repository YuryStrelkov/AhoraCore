using AhoraCore.Core.Buffers.UniformsBuffer;
using AhoraCore.Core.Shaders;
using System;


namespace AhoraCore.Core.CES.ICES
{
    public enum UniformBindingsLocations
    {
        ShaderData = 0,
        MaterialData = 1,
        TransformData = 2,
        ComponentData = 3,
        GameEntityData = 4,
        CameraData = 5,
        LightData = 7,
        TerrainSettings = 8,
        TextProperties = 9
    }

    public class UniformBufferedObject : IUniformBufferedObject
    {
        public  bool IsBuffered { get; protected set; }

        public string BufferName { get;  set; }

        public UniformsBuffer<string> UniformBuffer { get; protected set; }

        public void UpdateBufferIteam(string ItemName, float[]data)
        {
            UniformBuffer.Bind();
            UniformBuffer.UpdateBufferIteam(ItemName, data);
        }

        public void EnableBuffering(string bufferName)
        {
             IsBuffered = true;
             BufferName = bufferName;
             UniformBuffer = new UniformsBuffer<string>();
        }

        public void MarkBuffer(string[] itemNames, int[] itemLengths)
        {

            /////
            ////Добавлять элементы буфера строго в порядке следования их в шедере    
            /////
            if (!IsBuffered)
            {
                Console.WriteLine("Buffering is off ...");
                return;
            }
            for (int i = 0; i < itemNames.Length; i++)
            {
                UniformBuffer.addBufferItem(itemNames[i], itemLengths[i]);
            }
        }

        public void MarkBufferItem(string itemName, int itemLength)
        {
            if (!IsBuffered)
            {
                Console.WriteLine("Buffering is off ...");
                return;
            }
            UniformBuffer.addBufferItem(itemName, itemLength);
        }

        public void ConfirmBuffer()
        {
            if (!IsBuffered)
            {
                Console.WriteLine("Buffering is off ...");
                return;
            }
            if (UniformBuffer.ID == -1)
            {
                UniformBuffer.Create(1);
            }
        }

        public void ConfirmBuffer(int capacity)
        {
            if (!IsBuffered)
            {
                Console.WriteLine("Buffering is off ...");
                return;
            }
            UniformBuffer.Create(capacity);
        }

        public void SetBindigLocation(UniformBindingsLocations location)
        {
            if (!IsBuffered)
            {
                Console.WriteLine("Buffering is off ...");
                return;
            }
            UniformBuffer.Buff_binding_Point = (int)location;
        }

        public void Bind(AShader bindingTarget)
        {
            bindingTarget.SetUniformBlock(BufferName, UniformBuffer);/// UniformBuffer.LinkBufferToShder(bindingTarget, BufferName);
        }

        public UniformBufferedObject()
        {
            IsBuffered = false;
        }
    }
}
