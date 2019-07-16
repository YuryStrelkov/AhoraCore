using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using AhoraProject.Ahora.Core.IRender;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.SpecificBuffers;
using System;
using AhoraCore.Core.Utils;
using AhoraCore.Core.Buffers.StandartBuffers;

namespace AhoraCore.Core.Buffers
{
  
    /// <summary>
    /// Основной класс предназначенный для взаимодействия с буферами VAO,VBO,IBO их инстансирования в случае необходимости.
    /// В одном экземпляре класса  GeometryStorrage могут храниться любые модели с одним и тем же набором вершинных атрибутов 
    /// Доступ к объёкту геометрии осуществляется по его ID
    /// </summary>
    public class GeometryStorrage : ArrayBuffer, IDataStorrage<string>, IGeometryDataStorrage<string>, IRedreable<string>
    {
        private IndexedList<string> VerticesIndeces;

        private IndexedList<string> FacesIndeces;

        private Dictionary<string, InstanceBuffer> GeometryItemsInstansesList;

        public void AddItems(List<string> geometryIDs, List<FloatBuffer> verticesData, List<IntegerBuffer> indecesDtata)
        {
            for (int i=0; i< geometryIDs.Count;i++)
            {
                AddItem(geometryIDs[i], verticesData[i].BufferData, indecesDtata[i].BufferData);
            }
        }

        public void AddItem(string key, float[] vData, int[] iData)
        {
            VerticesIndeces.Add(key, vData.Length);
            FacesIndeces.Add(key, iData.Length);
            LoadData(vData, iData);
        }

        public void ClearStorrage()
        {

            VerticesIndeces.Clear();
            FacesIndeces.Clear();
            VBO.Clear();
            IBO.Clear();
            foreach (string key in GeometryItemsInstansesList.Keys)
            {
                GeometryItemsInstansesList[key].Clear();
            }
        }

        public void DeleteStorrage()
        {
            Delete();
            VerticesIndeces.Clear();
            FacesIndeces.Clear();
            foreach (string key  in GeometryItemsInstansesList.Keys)
            {
                GeometryItemsInstansesList[key].Delete();
            }
        }

        public void RemoveItem(string geometryID)
        {
            if (FacesIndeces.Indexes.ContainsKey(geometryID))
            {
                IBO.Delete(FacesIndeces.GetOffset(geometryID), FacesIndeces.GetLength(geometryID));
                VBO.Delete(VerticesIndeces.GetOffset(geometryID), VerticesIndeces.GetLength(geometryID));
                FacesIndeces.Remove(geometryID);
                VerticesIndeces.Remove(geometryID);
            }
        }

        public void BeforeRender()
        {
            Bind();
        }

        public void Render()
        {
            foreach (string key in VerticesIndeces.Indexes.Keys)
            {
                RenderIteam(key);
            }
        }

        public void RenderIteam(string iteamID)
        {
            Bind();
            if (GeometryItemsInstansesList.ContainsKey(iteamID))
            {
                
                GeometryItemsInstansesList[iteamID].EnableAttribytes();
                GL.DrawElementsInstanced(BeginMode.Triangles, FacesIndeces.GetLength(iteamID),
                                         DrawElementsType.UnsignedInt, (IntPtr)(0 * sizeof(int)),
                                         GeometryItemsInstansesList[iteamID].Fillnes / 16);
                GeometryItemsInstansesList[iteamID].DisableAttribytes();
            }
            else
            {
                /////сетка
                //GL.DrawElements(PrimitiveType.Lines, GeometryItemsList[iteamID].I_length,
                //                DrawElementsType.UnsignedInt, GeometryItemsList[iteamID].I_start_i * sizeof(int));
                // оболочка
                //PrimitiveType.Triangles, 3*FacesCount, DrawElementsType.UnsignedInt, 0
                GL.DrawElements(PrimitiveType.Triangles, FacesIndeces.GetLength(iteamID),
                              DrawElementsType.UnsignedInt, FacesIndeces.GetOffset(iteamID) * sizeof(int));
            }
            Unbind();
        }

        public void PostRender()
        {
            Unbind();
        }

        public void AssignInstToGeo(string geoID, float[] data)
        {
            if (FacesIndeces.Indexes.ContainsKey(geoID))
            {
                GeometryItemsInstansesList.Add(geoID, new InstanceBuffer(Attribytes.Count));
                GeometryItemsInstansesList[geoID].Create(data.Length);
                GeometryItemsInstansesList[geoID].LoadBufferData(data);
            }
            else
            {
                Console.Out.WriteLine("Unable to assign instancing to object " + geoID + " -  no geometry for this key");
            }
        }

        public void UpdateConcreteInst(string geoID, int[] instID, float[] data)
        {
            if (FacesIndeces.Indexes.ContainsKey(geoID))
            {
                float[] tmp = new float[16];

                GeometryItemsInstansesList[geoID].Bind();

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
            if (FacesIndeces.Indexes.ContainsKey(geoID))
            {
                GeometryItemsInstansesList[geoID].Bind();
                 GeometryItemsInstansesList[geoID].LoadBufferSubdata(data, 0);
            }
            else
            {
                Console.Out.WriteLine("Unable to assign instancing to object " + geoID + " -  no geometry for this key");
            }
        }

        public GeometryStorrage() : base()
        {
            FacesIndeces = new IndexedList<string>();
            VerticesIndeces = new IndexedList<string>();
            GeometryItemsInstansesList = new Dictionary<string, InstanceBuffer>();
        }

    }
}
