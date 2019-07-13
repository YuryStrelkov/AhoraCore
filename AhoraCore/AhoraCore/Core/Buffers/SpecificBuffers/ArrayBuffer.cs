using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.IBuffres;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;


namespace AhoraCore.Core.Buffers.SpecificBuffers
{
     public class ArrayBuffer: ABuffer, IAttribyteable, IEditMarkedAttribyte
    {

        public struct AttrAndSize
        {
            public int attrType;
            public int attrLength;
            public AttrAndSize(int t, int l)
            {
                attrType = t;
                attrLength = l;
            }

        }
    
        protected bool isBinded = false;

        private int VertexDataSize = 0;

        public byte AtribbytesMask { get; protected set; }

        public Dictionary<int, AttrAndSize> Attribytes { get; protected set; }

        public VerticesBuffer VBO { get; private set; }

        public IndecesBuffer IBO { get; private set; }

        public override void BindBuffer()
        {
            if (isBinded)
            {
                return;
            }
            
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
        }

        public override void CreateBuffer()
        {
            AtribbytesMask = 0x00;
            ID = ID == -1 ? GL.GenVertexArray():ID ;
        }

        public override void CreateBuffer(int capacity)
        {
            CreateBuffer();
            GL.BindVertexArray(ID);
            VBO = new VerticesBuffer();
            VBO.CreateBuffer(capacity);
            IBO = new IndecesBuffer();
            IBO.CreateBuffer(capacity);
            UnbindBuffer();
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
            Attribytes.Add(Attribytes.Count, new AttrAndSize(attrType, sampleSize));
            AtribbytesMask |= (byte)attrType;
            GL.VertexAttribPointer(attribID, sampleSize, VertexAttribPointerType.Float, false, from * sizeof(float), from * 4);
            VertexDataSize += sampleSize;
        }

        public void MarkBufferAttributePointers(int VericesAttribytesMap)
        {
          
            if ((AtribbytesMask == VericesAttribytesMap) && (Attribytes.Count!=0))
            {
                return;
            }

             BindBuffer();
   
            int offset = 0;
         
            if ((VericesAttribytesMap & VericesAttribytes.V_POSITION) == VericesAttribytes.V_POSITION)
            {
                Attribytes.Add(Attribytes.Count, new AttrAndSize(VericesAttribytes.V_POSITION,3));
                AtribbytesMask |= (byte)VericesAttribytes.V_POSITION;
                VertexDataSize += 3;
            }
             if ((VericesAttribytesMap & (byte)VericesAttribytes.V_UVS) == VericesAttribytes.V_UVS)
            {
                Attribytes.Add(Attribytes.Count, new AttrAndSize(VericesAttribytes.V_UVS, 2));
                AtribbytesMask |= (byte)VericesAttribytes.V_UVS;
                VertexDataSize += 2;
            }
             if ((VericesAttribytesMap & (byte)VericesAttribytes.V_NORMAL) == VericesAttribytes.V_NORMAL)
            {
                Attribytes.Add(Attribytes.Count,new  AttrAndSize(VericesAttribytes.V_NORMAL, 3));
                AtribbytesMask |= (byte)VericesAttribytes.V_NORMAL;
                VertexDataSize += 3;
            }
             if ((VericesAttribytesMap & (byte)VericesAttribytes.V_TANGENT) == VericesAttribytes.V_TANGENT)
            {
                Attribytes.Add(Attribytes.Count, new AttrAndSize(VericesAttribytes.V_TANGENT, 3));
                AtribbytesMask |= (byte)VericesAttribytes.V_TANGENT;
                VertexDataSize += 3;
            }
             if ((VericesAttribytesMap & (byte)VericesAttribytes.V_BITANGENT) == VericesAttribytes.V_BITANGENT)
            {
                Attribytes.Add(Attribytes.Count, new AttrAndSize(VericesAttribytes.V_BITANGENT, 3));
                AtribbytesMask |= (byte)VericesAttribytes.V_BITANGENT;
                VertexDataSize += 3;
            }
             if ((VericesAttribytesMap & (byte)VericesAttribytes.V_BONES) == VericesAttribytes.V_BONES)
            {
                Attribytes.Add(Attribytes.Count, new AttrAndSize(VericesAttribytes.V_BONES, 4));
                AtribbytesMask |= (byte)VericesAttribytes.V_BONES;
                VertexDataSize += 4;
            }
             if ((VericesAttribytesMap & (byte)VericesAttribytes.V_BONES_WEIGHTS) == VericesAttribytes.V_BONES_WEIGHTS)
            {
                Attribytes.Add(Attribytes.Count, new AttrAndSize(VericesAttribytes.V_BONES_WEIGHTS, 4));
                AtribbytesMask |= (byte)VericesAttribytes.V_BONES_WEIGHTS;
                VertexDataSize += 4;
            }
             if ((VericesAttribytesMap & (byte)VericesAttribytes.V_COLOR_RGB) == VericesAttribytes.V_COLOR_RGB)
            {
                Attribytes.Add(Attribytes.Count, new AttrAndSize(VericesAttribytes.V_COLOR_RGB, 3));
                AtribbytesMask |= (byte)VericesAttribytes.V_COLOR_RGB;
                VertexDataSize += 3;
            }
             if ((VericesAttribytesMap & (byte)VericesAttribytes.V_COLOR_RGBA) == VericesAttribytes.V_COLOR_RGBA)
            {
                Attribytes.Add(Attribytes.Count, new AttrAndSize(VericesAttribytes.V_COLOR_RGBA, 4));
                AtribbytesMask |= (byte)VericesAttribytes.V_COLOR_RGBA;
                VertexDataSize += 4;
            }

            for (int i = 0; i < Attribytes.Count; i++)
            {
                GL.VertexAttribPointer(i, Attribytes[i].attrLength, VertexAttribPointerType.Float, false, VertexDataSize * sizeof(float), offset);
                offset += Attribytes[i].attrLength * 4;
            }
            UnbindBuffer();
        }

        public void EnableAttribytes()
        {
            foreach (int key in Attribytes.Keys)
            {
                GL.EnableVertexAttribArray(key);
            }
        }

        public void DisableAttribytes()
        {
            foreach (int key in Attribytes.Keys)
            {
                GL.DisableVertexAttribArray(key);
            }
        }

        protected void LoadData(float [] vdata,  int [] idata)
        {

            if (Fillnes != 0)
            {
                for (int i = 1; i < idata.Length; i++)
                {
                    idata[i] += Fillnes;
                }
            }
            if (vdata.Length > VBO.Capacity - VBO.Fillnes)
            {
                EnhanceBufferCapsity(vdata.Length + VBO.Fillnes, idata.Length + IBO.Fillnes);

                BindBuffer();
                VBO.LoadBufferData(vdata);
                IBO.LoadBufferData(idata);
                UnbindBuffer();
            }
            else
            {
                BindBuffer();
                VBO.LoadBufferData(vdata);
                IBO.LoadBufferData(idata);
                UnbindBuffer();
            }
        }

        private void EnhanceBufferCapsity(int v_cap, int i_cap)
        {
            ArrayBuffer enhanced = new ArrayBuffer();
            enhanced.CreateBuffer();
            GL.BindVertexArray(enhanced.ID);
            enhanced.VBO = new VerticesBuffer();
            enhanced.VBO.CreateBuffer(v_cap);
            enhanced.IBO = new IndecesBuffer();
            enhanced.IBO.CreateBuffer(i_cap);
            
            enhanced.UnbindBuffer();

            if (VBO.Fillnes!=0)
            {
                VBO.CopyBufferData(enhanced.VBO, 0, VBO.Fillnes, 0);
            }
            if (IBO.Fillnes!=0)
            {
                IBO.CopyBufferData(enhanced.IBO, 0, IBO.Fillnes, 0);
            }

          ///Удаляем старое
            GL.DeleteVertexArray(ID);

            VBO.DeleteBuffer();

            IBO.DeleteBuffer();
            ////Переназначаем ID старых на ID новых
            VBO = enhanced.VBO;

            IBO = enhanced.IBO;

            ID  = enhanced.ID;
            ////Биндим  и переназначаем VertexAttribPointers
            GL.BindVertexArray(ID);

            int offset = 0;

            for (int i = 0; i < Attribytes.Keys.Count; i++)
            {
                GL.VertexAttribPointer(i, Attribytes[i].attrLength, VertexAttribPointerType.Float, false, VertexDataSize * sizeof(float), offset);
                offset += Attribytes[i].attrLength * 4;
            }
        }

        public ArrayBuffer() : base()
        {
            Attribytes = new Dictionary<int, AttrAndSize>();
            CreateBuffer(16);
        }

    }
}
