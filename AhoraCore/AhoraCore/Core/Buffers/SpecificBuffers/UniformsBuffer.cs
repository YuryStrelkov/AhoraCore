﻿using AhoraCore.Core.Buffers.IBuffres;
using AhoraCore.Core.Shaders;
using AhoraCore.Core.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
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


    public sealed class UniformsBuffer<KeyType> : EditableBuffer<float>
    {
        private Dictionary<KeyType, BufferIteam> bufferItemsMap;
        
        private int mapSize = 0;

        public int Buff_binding_Point = 1;

        public int Uniform_block_index;

        public void addBufferItem(KeyType name, int dataLength)
        {
            if (!bufferItemsMap.ContainsKey(name))
            {
                BufferIteam item = new BufferIteam(dataLength, mapSize);

                mapSize += dataLength;

                bufferItemsMap.Add(name, item);
            }
       }

        public void UpdateBufferIteam(KeyType bufferItemName, int itemID, float[] data)
        {
            LoadBufferSubdata(data, itemID * mapSize + bufferItemsMap[bufferItemName].Offset);
        }

        public void Bind(AShader shader)
        {
            GL.BindBuffer(BindingTarget, ID);
            GL.UniformBlockBinding(shader.ShaderID, Uniform_block_index, Buff_binding_Point);
            GL.BindBufferBase(BufferRangeTarget.UniformBuffer, Buff_binding_Point, ID);
        }

        public void UpdateBufferIteam(KeyType bufferItemName, float[] data)
        {
            LoadBufferSubdata(data,  bufferItemsMap[bufferItemName].Offset);
        }

        public void UpdateBufferIteam(KeyType bufferItemName, Matrix4 data)
        {
            LoadBufferSubdata(MathUtils.ToArray(data), bufferItemsMap[bufferItemName].Offset);
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
           //Сперва разметить буфер, а потом уже создать 
           //CreateBuffer(100);
        }

        private void EnhanceBufferCapsity(int mapsets)
        {
            UniformsBuffer<KeyType> enhanced = new UniformsBuffer<KeyType>();
            
            enhanced.Create();

            enhanced.Bind();

            enhanced.Capacity = mapsets;

            GL.BufferData(BindingTarget, enhanced.Capacity * IteamByteSize, (IntPtr)0, BufferUsageHint.DynamicDraw);
            
            enhanced.Unbind();

            CopyBufferData(enhanced, 0, 0, Capacity);

            Delete();

            ID = enhanced.ID;

            Fillnes = enhanced.Fillnes;

            Capacity = enhanced.Capacity;
        }



        public override void Create(int numberOfMapSets)
        {
            if (Capacity == 0)
            {
                base.Create(numberOfMapSets * mapSize);
            }
            else
            {
                EnhanceBufferCapsity(mapSize);
            }
        }

    }
}
