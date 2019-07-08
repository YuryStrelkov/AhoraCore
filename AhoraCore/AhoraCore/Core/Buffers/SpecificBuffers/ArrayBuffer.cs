using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System;

namespace AhoraCore.Core.Buffers.SpecificBuffers
{
    public enum VerticesBufferTypes
    {
        VerticesBuffer = 0,

        IndecesBuffer = 1,

        InstanceBuffer = 2,
    }

    public class ArrayBuffer: ABuffer, IAttribyteable
    {
        protected bool isBinded = false;

        public byte AtribbytesMask { get; protected set; }

        public Dictionary<int, int> Attribytes { get; protected set; }

        public VerticesBuffer VBO { get; private set; }

        public IndecesBuffer IBO { get; private set; }

        public InstanceBuffer InstanceBO { get; private set; }
        
        public override void BindBuffer()
        {
            if (isBinded)
            {
                return;
            }
            AtribbytesMask = 255;
            GL.BindVertexArray(ID);
            VBO.BindBuffer();
            IBO.BindBuffer();
            EnableAttribytes();
            isBinded = true;
        }

        public override void UnbindBuffer()
        {
            if (!isBinded)
            {
                return;
            }
            GL.BindVertexArray(0);
            VBO.UnbindBuffer();
            IBO.UnbindBuffer();
            DisableAttribytes();
            isBinded = false;
            AtribbytesMask = 255;
        }

        public override void CreateBuffer()
        {
            ID = ID == -1 ? GL.GenVertexArray():ID ;
        }

        public override void DeleteBuffer()
        {
            GL.DeleteVertexArray(ID);
            VBO.DeleteBuffer();
            IBO.DeleteBuffer();
            Attribytes.Clear();
        }

        public void MarkBufferAttributePointer(int attrType, int attribID, int sampleSize, int from)
        {
            Attribytes.Add(Attribytes.Count, attrType);
            AtribbytesMask |= (byte)attrType;
            GL.VertexAttribPointer(attribID, sampleSize, VertexAttribPointerType.Float, false, from * sizeof(float), from * 4);
        }

        public void MarkBufferAttributePointers(int VericesAttribytesMap)
        {
          
            if ((AtribbytesMask & VericesAttribytesMap) == AtribbytesMask)
            {
                return;
            }

            BindBuffer();

            int offset = 0;
            
            List<int> sizes = new List<int>();

            int v_size = 0;

            if ((VericesAttribytesMap & VericesAttribytes.V_POSITION) == VericesAttribytes.V_POSITION)
            {
                Attribytes.Add(Attribytes.Count, VericesAttribytes.V_POSITION);
                AtribbytesMask |= (byte)VericesAttribytes.V_POSITION;
                sizes.Add(3);
                v_size += 3;
            }
            else if ((VericesAttribytesMap & (byte)VericesAttribytes.V_UVS) == VericesAttribytes.V_UVS)
            {
                Attribytes.Add(Attribytes.Count, VericesAttribytes.V_UVS);
                AtribbytesMask |= (byte)VericesAttribytes.V_UVS;
                sizes.Add(2);
                v_size += 2;
            }
            else if ((VericesAttribytesMap & (byte)VericesAttribytes.V_NORMAL) == VericesAttribytes.V_NORMAL)
            {
                Attribytes.Add(Attribytes.Count, VericesAttribytes.V_NORMAL);
                AtribbytesMask |= (byte)VericesAttribytes.V_NORMAL;
                sizes.Add(3);
                v_size += 3;
            }
            else if ((VericesAttribytesMap & (byte)VericesAttribytes.V_TANGENT) == VericesAttribytes.V_TANGENT)
            {
                Attribytes.Add(Attribytes.Count, VericesAttribytes.V_TANGENT);
                AtribbytesMask |= (byte)VericesAttribytes.V_TANGENT;
                sizes.Add(3);
                v_size += 3;
            }
            else if ((VericesAttribytesMap & (byte)VericesAttribytes.V_BITANGENT) == VericesAttribytes.V_BITANGENT)
            {
                Attribytes.Add(Attribytes.Count, VericesAttribytes.V_BITANGENT);
                AtribbytesMask |= (byte)VericesAttribytes.V_BITANGENT;
                sizes.Add(3);
                v_size += 3;
            }
            else if ((VericesAttribytesMap & (byte)VericesAttribytes.V_BONES) == VericesAttribytes.V_BONES)
            {
                Attribytes.Add(Attribytes.Count, VericesAttribytes.V_BONES);
                AtribbytesMask |= (byte)VericesAttribytes.V_BONES;
                sizes.Add(4);
                v_size += 4;
            }
            else if ((VericesAttribytesMap & (byte)VericesAttribytes.V_BONES_WEIGHTS) == VericesAttribytes.V_BONES_WEIGHTS)
            {
                Attribytes.Add(Attribytes.Count, VericesAttribytes.V_BONES_WEIGHTS);
                AtribbytesMask |= (byte)VericesAttribytes.V_BONES_WEIGHTS;
                sizes.Add(4);
                v_size += 4;
            }
            else if ((VericesAttribytesMap & (byte)VericesAttribytes.V_COLOR_RGB) == VericesAttribytes.V_COLOR_RGB)
            {
                Attribytes.Add(Attribytes.Count, VericesAttribytes.V_COLOR_RGB);
                AtribbytesMask |= (byte)VericesAttribytes.V_COLOR_RGB;
                sizes.Add(3);
                v_size += 3;
            }
            else if ((VericesAttribytesMap & (byte)VericesAttribytes.V_COLOR_RGBA) == VericesAttribytes.V_COLOR_RGBA)
            {
                Attribytes.Add(Attribytes.Count, VericesAttribytes.V_COLOR_RGBA);
                AtribbytesMask |= (byte)VericesAttribytes.V_COLOR_RGBA;
                sizes.Add(4);
                v_size += 4;
            }

            for (int i = 0; i < sizes.Count; i++)
            {
                GL.VertexAttribPointer(i, sizes[i], VertexAttribPointerType.Float, false, v_size*sizeof(float), offset);
                offset += sizes[i] * 4;
            }
            UnbindBuffer();
        }

        public void EnableAttribytes()
        {
            foreach (KeyValuePair<int, int> kvp in Attribytes)
            {
                GL.EnableVertexAttribArray(kvp.Key);
            };
        }

        public void DisableAttribytes()
        {
            foreach (KeyValuePair<int, int> kvp in Attribytes)
            {
                GL.DisableVertexAttribArray(kvp.Key);
            };
        }
        
        public ArrayBuffer(  int targetCapacity) : base()
        {
            Attribytes = new Dictionary<int, int>();
            CreateBuffer();
            GL.BindVertexArray(ID);
            VBO = new VerticesBuffer();
            VBO.CreateBuffer(targetCapacity);
            IBO = new IndecesBuffer();
            IBO.CreateBuffer(targetCapacity);
            IBO.UnbindBuffer();
            InstanceBO = new InstanceBuffer();
            InstanceBO.CreateBuffer(16 * 10);
            UnbindBuffer();
        }

        public ArrayBuffer() : base()
        {
            AtribbytesMask = 0x00;
            Attribytes = new Dictionary<int, int>();
            CreateBuffer();
            GL.BindVertexArray(ID);
            VBO = new VerticesBuffer();
            VBO.CreateBuffer(100000);
            IBO = new IndecesBuffer();
            IBO.CreateBuffer(100000);
            IBO.UnbindBuffer();
            InstanceBO = new InstanceBuffer();
            InstanceBO.CreateBuffer(16*10);
            UnbindBuffer();
        }

    }
}
