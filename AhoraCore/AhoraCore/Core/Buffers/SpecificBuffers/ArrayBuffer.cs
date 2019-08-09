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

        public override void Bind()
        {
            if (isBinded)
            {
                return;
            }
            
            GL.BindVertexArray(ID);
            VBO.Bind();
            IBO.Bind();
            EnableAttribytes();
            isBinded = true;
        }

        public override void Unbind()
        {
            if (!isBinded)
            {
                return;
            }
            GL.BindVertexArray(0);
            VBO.Unbind();
            IBO.Unbind();
            DisableAttribytes();
            isBinded = false;
        }

        public override void Create()
        {
            AtribbytesMask = 0x00;
            ID = ID == -1 ? GL.GenVertexArray():ID ;
        }

        public override void Create(int capacity)
        {
            Create();
            GL.BindVertexArray(ID);
            VBO = new VerticesBuffer();
            VBO.Create(capacity);
            IBO = new IndecesBuffer();
            IBO.Create(capacity);
            Unbind();
      }

        public override void Delete()
        {
            GL.DeleteVertexArray(ID);
            VBO.Delete();
            IBO.Delete();
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

             Bind();
   
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
            Unbind();
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

                Bind();
                VBO.LoadBufferData(vdata);
                IBO.LoadBufferData(idata);

                Unbind();

                //Console.WriteLine("VBO");
                //DebugBuffers.displayBufferDataVBO(VBO);
                //Console.WriteLine("IBO");
                //DebugBuffers.displayBufferDataIBO(IBO);

            }
            else
            {
                Bind();
                VBO.LoadBufferData(vdata);
                IBO.LoadBufferData(idata);
                Unbind();
            }
        }



        protected void LoadData(float[] vdata)
        {

            if (vdata.Length > VBO.Capacity - VBO.Fillnes)
            {
                EnhanceBufferCapsity(vdata.Length + VBO.Fillnes,IBO.Capacity);
                Bind();
                VBO.LoadBufferData(vdata);
                Unbind();
            }
            else
            {
                Bind();
                VBO.LoadBufferData(vdata);
                Unbind();
            }
        }

        private void EnhanceBufferCapsity(int v_cap, int i_cap)
        {
            ArrayBuffer enhanced = new ArrayBuffer();
            enhanced.Create();
            GL.BindVertexArray(enhanced.ID);
            enhanced.VBO = new VerticesBuffer();
            enhanced.VBO.Create(v_cap);
            enhanced.IBO = new IndecesBuffer();
            enhanced.IBO.Create(i_cap);
            
            enhanced.Unbind();

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

            VBO.Delete();

            IBO.Delete();
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
                offset += Attribytes[i].attrLength  *sizeof(float);
            }
        }

        public override void Bind(BufferTarget bindTarget)
        {
     
        }

        public override void Clear()
        {
            VBO.Clear();
            IBO.Clear();
        }

        public ArrayBuffer() : base()
        {
            Attribytes = new Dictionary<int, AttrAndSize>();
            Create(16000);
        }

    }
}
