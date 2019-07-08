using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using AhoraProject.Ahora.Core.IRender;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.SpecificBuffers;
using System;

namespace AhoraCore.Core.Buffers
{
    public struct GeometryStorrageIteam<T>
    {
        public T ID { get; private set; }

        public T ParentID { get; set; }

        public T ChildID { get; set; }

        public int V_start_i { get; private set; }

        public int V_length { get; private set; }

        public int I_start_i { get; private set; }

        public int I_length  { get; private set; }

        public void shiftLeft(int v_shift, int i_shift)
        {
            V_start_i -= v_shift;
            I_start_i -= i_shift;
        }

        public void shiftRight(int v_shift, int i_shift)
        {
            V_start_i += v_shift;
            I_start_i += i_shift;
        }

        public GeometryStorrageIteam(T ID_, T parenID_,T childID, int v_start, int v_length, int i_start, int i_length)
        {
            ID = ID_;
            ParentID  = parenID_;
            ChildID   = childID;
            V_start_i = v_start;
            V_length  = v_length;
            I_start_i = i_start;
            I_length  = i_length;
        }
    }

    public class GeometryStorrage : ArrayBuffer, IGeometryStorrage<string>, IAttribyteable, IRedreable<string>
    {
        private Dictionary<string, GeometryStorrageIteam<string>> GeometryItemsList;

        private string LaysKey = "",RootKey;

        public void AddGeometry(string key,float[] vData,int[] iData)
        {
            try
            {
                ResolveKeys(key);
                if (Equals(LaysKey, ""))
                {
                    RootKey = key;

                    GeometryItemsList.Add(key, new GeometryStorrageIteam<string>(
                                                               /*GeometryID*/key,
                                                               /*ParentID*/key,
                                                               /*ChildID*/""
                                                               , VBO.Fillnes, vData.Length, IBO.Fillnes, iData.Length));
                }
                else
                {
                    GeometryItemsList.Add(key, new GeometryStorrageIteam<string>(
                                           /*GeometryID*/key,
                                           /*ParentID*/ LaysKey,
                                           /*ChildID*/""
                                           , VBO.Fillnes, vData.Length, IBO.Fillnes, iData.Length));
                }
                LaysKey = key;

                BindBuffer();
                VBO.BindBuffer();
                VBO.LoadBufferData(vData);
                // DebugBuffers.displayBufferDataVBO(this);
                IBO.BindBuffer();
                IBO.LoadBufferData(iData);
                UnbindBuffer();
                //DebugBuffers.displayBufferDataIBO(this);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.StackTrace.ToString());
            }


            /// public GeometryStorrageIteam(int ID_, int parenID_, int v_start, int v_length, int i_start, int i_length)
            //return 0;
        }

        public void ResolveKeys(string keyID)
        {
            if (!Equals(LaysKey, ""))
            {
                GeometryStorrageIteam<string> tmp = GeometryItemsList[LaysKey];
                tmp.ChildID = keyID;
                GeometryItemsList[LaysKey] = tmp;
            }
        }

        public void ClearGeomeryStorrage()
        {
            VBO.ClearBuffer();
            IBO.ClearBuffer();
        }

        public void DeleteGeomeryStorrage()
        {
            DeleteBuffer();
        }

        public void MergeGeometryItems(string[] geometryIDs)
        {
        }

        public void RemoveGeometry(string geometryID)
        {
            ///
            ///Проверить что происходит , когда вообще всё удалим 
            /// 
            try
            {   string key = geometryID;

                BindBuffer();
                
                VerticesBuffer Vtmp = new VerticesBuffer();

                Vtmp.CreateBuffer( VBO.Fillnes - GeometryItemsList[GeometryItemsList[geometryID].ChildID].V_start_i);

                VBO.CopyBufferData (Vtmp, GeometryItemsList[GeometryItemsList[geometryID].ChildID].V_start_i,
                                  
                                    VBO.Fillnes - GeometryItemsList[GeometryItemsList[geometryID].ChildID].V_start_i, 0);

        
                Vtmp.CopyBufferData(VBO, 0, Vtmp.Fillnes, GeometryItemsList[geometryID].V_start_i);

                IndecesBuffer Itmp = new IndecesBuffer();

                Itmp.CreateBuffer(IBO.Fillnes - GeometryItemsList[GeometryItemsList[geometryID].ChildID].I_start_i);

                IBO.CopyBufferData(Itmp, GeometryItemsList[GeometryItemsList[geometryID].ChildID].I_start_i,

                                    IBO.Fillnes - GeometryItemsList[GeometryItemsList[geometryID].ChildID].I_start_i, 0);
                
                Itmp.CopyBufferData(IBO, 0, Itmp.Fillnes, GeometryItemsList[geometryID].I_start_i);
                
                UnbindBuffer();

                Vtmp.DeleteBuffer();

                Itmp.DeleteBuffer();

                while (!Equals(GeometryItemsList[key].ChildID, ""))
                {
                    key = GeometryItemsList[key].ChildID;

                    GeometryItemsList[key].shiftLeft(GeometryItemsList[geometryID].V_length,
                                                     GeometryItemsList[geometryID].I_length);
                }

                ///не переназначена ссылочность 
                ///
                GeometryItemsList.Remove(geometryID);
                ///DebugBuffers.displayBufferDataIBO(this);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.StackTrace.ToString());
            }
        }

        public void BeforeRender()
        {
            BindBuffer();
        }

        public void Render()
        {
            foreach (KeyValuePair<string, GeometryStorrageIteam<string>> gsi in GeometryItemsList)
            {
                RenderIteam(gsi.Value.ID);
            }
        }

        public void RenderIteam(string iteamID)
        {
           GL.DrawElements(PrimitiveType.Triangles, GeometryItemsList[iteamID].I_length,
                            DrawElementsType.UnsignedInt, GeometryItemsList[iteamID].I_start_i * sizeof(int));
        }

        public void PostRender()
        {
            UnbindBuffer();
        }

        public GeometryStorrage() : base()
        {
            GeometryItemsList = new Dictionary<string, GeometryStorrageIteam<string>>();
        }

        public GeometryStorrage(int cap) : base(cap)
        {
            GeometryItemsList = new Dictionary<string, GeometryStorrageIteam<string>>();
        }

    }
}
