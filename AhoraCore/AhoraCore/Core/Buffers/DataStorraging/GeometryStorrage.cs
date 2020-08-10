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
        public delegate void RenderMethod(string ID);

        private string lastKey;

        public string LastKey { get { return lastKey; } }

        private IndexedList<string> VerticesIndeces;

        private IndexedList<string> FacesIndeces;

        private Dictionary<string, RenderMethod> RenderMethods;

        private Dictionary<string, InstanceBuffer> GeometryItemsInstansesList;

        public void AddItems(List<string> geometryIDs, List<FloatBuffer> verticesData, List<IntegerBuffer> indecesDtata)
        {
            for (int i=0; i< geometryIDs.Count;i++)
            {
                AddItem(geometryIDs[i], verticesData[i].ToArray(), indecesDtata[i].ToArray());
            }
        }

        public void AddItem(string key, int vertiesNumber, float[] vData)
        {
            lastKey = key;
            VerticesIndeces.Add(key, vData.Length);
            RenderMethods.Add(key, (ID) =>
            {
                GL.DrawArrays(PrimitiveType.Triangles, 0 , vertiesNumber);
            }
           );
            LoadData(vData);
        }

        public void AddItem(string key, float[] vData, RenderMethod renderer)
        {
            lastKey = key;
            VerticesIndeces.Add(key, vData.Length);
            RenderMethods.Add(key, renderer);
            LoadData(vData);
        }


        public void AddItem(string key, float[] vData, int[] iData)
        {
            lastKey = key;
            VerticesIndeces.Add(key, vData.Length);
            FacesIndeces.Add(key, iData.Length);
            RenderMethods.Add(key, (ID) => 
            {
                 GL.DrawElements(PrimitiveType.Triangles,      FacesIndeces.GetLength(ID),
                                 DrawElementsType.UnsignedInt, FacesIndeces.GetOffset(ID)*sizeof(int));
            }
            );
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

            if (lastKey == geometryID)
            {
                lastKey = FacesIndeces.Iteams[lastKey].Parent;
            }
            if (FacesIndeces.Iteams.ContainsKey(geometryID))
            {
                if (FacesIndeces.Iteams.ContainsKey(geometryID))
                {
                  //  IBO.Delete(FacesIndeces.GetOffset(geometryID), FacesIndeces.GetLength(geometryID));
                }
                if (GeometryItemsInstansesList.ContainsKey(geometryID))
                {
                    GeometryItemsInstansesList.Remove(geometryID);
                }
              //  VBO.Delete(VerticesIndeces.GetOffset(geometryID), VerticesIndeces.GetLength(geometryID));
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
            foreach (string key in VerticesIndeces.Iteams.Keys)
            {
                RenderIteam(key);
            }
        }

        public void RenderIteam(string iteamID)
        {
            Bind();
            RenderMethods[iteamID](iteamID);
            Unbind();
        }

        public void PostRender()
        {
            Unbind();
        }

        public void AssignInstToGeo(string geoID, float[] data)
        {
            if (FacesIndeces.Iteams.ContainsKey(geoID))
            {
                GeometryItemsInstansesList.Add(geoID, new InstanceBuffer(Attribytes.Count));
                GeometryItemsInstansesList[geoID].Create(data.Length);
                GeometryItemsInstansesList[geoID].LoadBufferData(data);
                RenderMethods[geoID] = (ID) => 
                {
                    GeometryItemsInstansesList[ID].EnableAttribytes();
                    GL.DrawElementsInstanced(BeginMode.Triangles, FacesIndeces.GetLength(ID),
                                             DrawElementsType.UnsignedInt, (IntPtr)(0 * sizeof(int)),
                                             GeometryItemsInstansesList[ID].Fillnes / 16);
                    GeometryItemsInstansesList[ID].DisableAttribytes();
                };
            }
            else
            {
                Console.Out.WriteLine("Unable to assign instancing to object " + geoID + " -  no geometry for this key");
            }
        }

        public void UpdateConcreteInst(string geoID, int[] instID, float[] data)
        {
            if (FacesIndeces.Iteams.ContainsKey(geoID))
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
            if (FacesIndeces.Iteams.ContainsKey(geoID))
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
            RenderMethods = new Dictionary<string, RenderMethod>();
        }

    }
}
