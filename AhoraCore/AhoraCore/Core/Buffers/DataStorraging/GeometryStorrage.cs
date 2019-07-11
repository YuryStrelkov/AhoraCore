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
    /// <summary>
    /// Основной класс предназначенный для взаимодействия с буферами VAO,VBO,IBO их инстансирования в случае необходимости.
    /// В одном экземпляре класса  GeometryStorrage могут храниться любые модели с одним и тем же набором вершинных атрибутов 
    /// Доступ к объёкту геометрии осуществляется по его ID
    /// </summary>
    public class GeometryStorrage : ArrayBuffer, IDataStorrage<string>, IRedreable<string>
    {
        private Dictionary<string, GeometryStorrageIteam<string>> GeometryItemsList;

        private Dictionary<string, InstanceBuffer> GeometryItemsInstansesList;

        private string LaysKey = "", RootKey;

        public void AddItem(string key, float[] vData, int[] iData)
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

        public void ClearStorrage()
        {
            VBO.ClearBuffer();
            IBO.ClearBuffer();
            foreach (KeyValuePair<string, InstanceBuffer> kvp in GeometryItemsInstansesList)
            {
                kvp.Value.ClearBuffer();
            }
        }

        public void DeleteStorrage()
        {
            DeleteBuffer();
            foreach (KeyValuePair<string, InstanceBuffer> kvp in GeometryItemsInstansesList)
            {
                kvp.Value.DeleteBuffer();
            }
        }

        public void MergeItems(string[] geometryIDs)
        {
        }

        public void RemoveItem(string geometryID)
        {
            ///
            ///Проверить что происходит , когда вообще всё удалим 
            /// 
            try
            { string key = geometryID;

                BindBuffer();

                VerticesBuffer Vtmp = new VerticesBuffer();

                Vtmp.CreateBuffer(VBO.Fillnes - GeometryItemsList[GeometryItemsList[geometryID].ChildID].V_start_i);

                VBO.CopyBufferData(Vtmp, GeometryItemsList[GeometryItemsList[geometryID].ChildID].V_start_i,

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
            foreach (string key in GeometryItemsList.Keys)
            {
                RenderIteam(key);
            }
        }

        public void RenderIteam(string iteamID)
        {
            if (GeometryItemsInstansesList.ContainsKey(iteamID))
            {
                GeometryItemsInstansesList[iteamID].EnableAttribytes();
                GL.DrawElementsInstanced(BeginMode.Triangles, GeometryItemsList[iteamID].I_length,
                                         DrawElementsType.UnsignedInt, (IntPtr)(GeometryItemsList[iteamID].I_start_i * sizeof(int)),
                                         GeometryItemsInstansesList[iteamID].Fillnes / 16);
                GeometryItemsInstansesList[iteamID].DisableAttribytes();
            }
            else
            {
                /////сетка
                GL.DrawElements(PrimitiveType.LineStripAdjacencyExt, GeometryItemsList[iteamID].I_length,
                                DrawElementsType.UnsignedInt, GeometryItemsList[iteamID].I_start_i * sizeof(int));
                /////оболочка
                //GL.DrawElements(PrimitiveType.Triangles, GeometryItemsList[iteamID].I_length,
                //               DrawElementsType.UnsignedInt, GeometryItemsList[iteamID].I_start_i * sizeof(int));
            }
        }

        public void PostRender()
        {
            UnbindBuffer();
        }

        public void AssignInstToGeo(string geoID, float[] data)
        {
            if (GeometryItemsList.ContainsKey(geoID))
            {
                GeometryItemsInstansesList.Add(geoID, new InstanceBuffer(Attribytes.Count));
                GeometryItemsInstansesList[geoID].CreateBuffer(data.Length);
                GeometryItemsInstansesList[geoID].LoadBufferData(data);
            }
            else
            {
                Console.Out.WriteLine("Unable to assign instancing to object " + geoID + " -  no geometry for this key");
            }
        }

        public void UpdateConcreteInst(string geoID, int[] instID, float[] data)
        {
            if (GeometryItemsList.ContainsKey(geoID))
            {
                float[] tmp = new float[16];

                GeometryItemsInstansesList[geoID].BindBuffer();

                for (int i = 0; i < instID.Length; i++)
                {
                    tmp[i] = data[i * 16];
                    tmp[i+1] = data[i * 16+1];
                    tmp[i+2] = data[i * 16+2];
                    tmp[i+3] = data[i * 16+3];
                    tmp[i+4] = data[i * 16+4];
                    tmp[i+5] = data[i * 16+5];
                    tmp[i+6] = data[i * 16+6];
                    tmp[i+7] = data[i * 16+7];
                    tmp[i+8] = data[i * 16+8];
                    tmp[i+9] = data[i * 16+9];
                    tmp[i+10] = data[i * 16+10];
                    tmp[i+11] = data[i * 16+11];
                    tmp[i+12] = data[i * 16+12];
                    tmp[i+13] = data[i * 16+13];
                    tmp[i+14] = data[i * 16+14];
                    tmp[i + 15] = data[i * 16+15];

                    GeometryItemsInstansesList[geoID].LoadBufferSubdata(tmp, instID[i] * 16);
                }
            }
            else
            {
                Console.Out.WriteLine("Unable to assign instancing to object " + geoID + " -  no geometry for this key");
            }
        }

        public void UpdateWholeInst(string geoID, float[] data)
        {
            if (GeometryItemsList.ContainsKey(geoID))
            {
                GeometryItemsInstansesList[geoID].BindBuffer();
                 GeometryItemsInstansesList[geoID].LoadBufferSubdata(data, 0);
            }
            else
            {
                Console.Out.WriteLine("Unable to assign instancing to object " + geoID + " -  no geometry for this key");
            }
        }

        public GeometryStorrage() : base()
        {
            GeometryItemsList = new Dictionary<string, GeometryStorrageIteam<string>>();

            GeometryItemsInstansesList = new Dictionary<string, InstanceBuffer>();
        }

        public GeometryStorrage(int cap) : base(cap)
        {
            GeometryItemsList = new Dictionary<string, GeometryStorrageIteam<string>>();
            GeometryItemsInstansesList = new Dictionary<string, InstanceBuffer>();
        }

    }
}
