using AhoraCore.Core.Buffers.IBuffres;
using AhoraCore.Core.Shaders;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace AhoraCore.Core.Buffers.UniformsBuffer
{

    public struct BufferIteam
    {
        public int Offset { set; get; }

        public int Length { set; get; }

        public BufferIteam(int length, int offset)
        {
            Offset = offset;
            Length = length;
        }
        
    }


    public class UniformsBuffer<KeyType> : EditableBuffer<float, UniformsBuffer<KeyType>>
    {
        private Dictionary<KeyType, BufferIteam> bufferItemsMap;

        private int mapSize = 0;

        private int numberOfIteams;

        public int Buff_binding_Point = 1;

        public int Uniform_block_index;

        public void addBufferItem(KeyType name, int dataLength)
        {
            BufferIteam item = new BufferIteam(dataLength, mapSize);

            mapSize += dataLength;

            bufferItemsMap.Add(name, item);
        }

        public void LinkBufferToShder(AShader shader, string block_name)
        {
            GL.BindBuffer(BufferTarget.UniformBuffer, ID);
            Uniform_block_index = GL.GetUniformBlockIndex(shader.ShaderID, block_name);
            GL.UniformBlockBinding(shader.ShaderID, Uniform_block_index, Buff_binding_Point);
            GL.BindBufferBase(BufferRangeTarget.UniformBuffer, Buff_binding_Point, ID);
        }

        public void LinkBufferToShder(AShader shader, string block_name, int bindingPoint)
        {
            Buff_binding_Point = bindingPoint;
            GL.BindBuffer(BufferTarget.UniformBuffer, ID);
            Uniform_block_index = GL.GetUniformBlockIndex(shader.ShaderID, block_name);
            GL.UniformBlockBinding(shader.ShaderID, Uniform_block_index, bindingPoint);
            GL.BindBufferBase(BufferRangeTarget.UniformBuffer, bindingPoint, ID);
        }

        public void UpdateBufferIteam(KeyType bufferItemName, int itemID, float[] data)
        {
            LoadBufferSubdata(data, itemID * mapSize + bufferItemsMap[bufferItemName].Offset);
        }


        public void UpdateBufferIteam(KeyType bufferItemName, float[] data)
        {
            LoadBufferSubdata(data,  bufferItemsMap[bufferItemName].Offset);
        }



        public void UpdateBufferIteam(KeyType bufferItemName, int itemID, float data)
        {
            LoadBufferSubdata(new float[] { data }, itemID * mapSize + bufferItemsMap[bufferItemName].Offset);
        }


        public void UpdateBufferIteam(KeyType bufferItemName, float data)
        {
            LoadBufferSubdata(new float[] { data }, bufferItemsMap[bufferItemName].Offset);
        }


        public UniformsBuffer() : base()
        {
            BindingTarget = BufferTarget.UniformBuffer;
            bufferItemsMap = new Dictionary<KeyType, BufferIteam>();
           //Сперва разметит буфер, а потом уже создать 
           //CreateBuffer(100);
        }

        public override void Create(int numberOfMapSets)
        {
            base.Create(numberOfMapSets * mapSize);
        }

    }
}
