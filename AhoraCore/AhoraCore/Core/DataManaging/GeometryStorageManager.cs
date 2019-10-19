using AhoraCore.Core.Buffers;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.StandartBuffers;
using AhoraCore.Core.Models;
using AhoraProject.Ahora.Core.IRender;
using OpenTK.Graphics.OpenGL;
using System;

namespace AhoraCore.Core.DataManaging
{
    public class GeometryStorageManager : AManager<GeometryStorrage, int, string>, IRedreable<string>
    {
        private static GeometryStorageManager geometryData;

        public static GeometryStorageManager Data { get { return geometryData; } }

        public static void Initialize()
        {
            geometryData = new GeometryStorageManager();
            ModelLoader.LoadMesh("defaultModel", "Resources\\defaultModel.obj");
        }

        private GeometryStorageManager() : base()
        {
        }

        public void AddGeometry(int attibutesFormat, string geoID, float[] vData, int[] iData)
        {
            AppendData(geoID, attibutesFormat);
            managingData[attibutesFormat].AddItem(geoID, vData, iData);
        }


        public void AddGeometry(int attibutesFormat, int vertiscesNumber, string geoID, float[] vData)
        {
            AppendData(geoID, attibutesFormat);

            if (VericesAttribytes.V_POSITION == attibutesFormat)
            {
                managingData[attibutesFormat].AddItem(geoID, vData, (ID) => {
                    GL.DrawArrays(PrimitiveType.Points, 0, vertiscesNumber);
                });
                return;
            }
            managingData[attibutesFormat].AddItem(geoID, vertiscesNumber, vData);
        }


        public void AddGeometry(int attibutesFormat, string geoID, int vertiscesNumber, FloatBuffer vData)
        {
            AppendData(geoID, attibutesFormat);
            managingData[attibutesFormat].AddItem(geoID, vertiscesNumber, vData.ToArray());
        }


        public void AddGeometry(int attibutesFormat, string geoID, FloatBuffer vData, IntegerBuffer iData)
        {
            AppendData(geoID, attibutesFormat);
            managingData[attibutesFormat].AddItem(geoID, vData.ToArray(), iData.ToArray());
        }

        public void AddGeometrySet(int attibutesFormat, string[] geoIDs, FloatBuffer[] vDatas, IntegerBuffer[] iDatas)
        {
            for (int i = 0; i < geoIDs.Length; i++)
            {
                AddGeometry(attibutesFormat, geoIDs[i], vDatas[i], iDatas[i]);
            }
        }

        public void Render()
        {
            foreach (int key in managingData.Keys)
            {
                Render(key);
            }
        }

        public void Render(int attibutesFormat)
        {
            try
            {
                managingData[attibutesFormat].BeforeRender();
                managingData[attibutesFormat].Render();
                managingData[attibutesFormat].PostRender();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }
        }

        public void RenderIteam(string id)
        {
            try
            {
                managingData[uniqueKeysList[id]].Bind();
                managingData[uniqueKeysList[id]].RenderIteam(id);
                managingData[uniqueKeysList[id]].Unbind();
            }
            catch (Exception e)
            {
                Console.WriteLine("Model "+ id +" case error while drawing");
            }
        }

        public override void RemoveData(string dataKey)
        {
            if (uniqueKeysList.ContainsKey(dataKey))
            {
                managingData[uniqueKeysList[dataKey]].RemoveItem(dataKey);
                uniqueKeysList.Remove(dataKey);
            }
        }

        public override void ClearManager()
        {
            foreach (int k in managingData.Keys)
            {
                managingData[k].ClearStorrage();
            }
        }

        public override void DeleteManager()
        {
            foreach (int k in managingData.Keys)
            {
                managingData[k].DeleteStorrage();
            }
        }

        /// <summary>
        /// /*не рекомендуется. Используйте AddGeometry*/
        /// </summary>
        /// <param name="dataKey"></param>
        /// <param name="attibutesFormat"></param>
        public override void AppendData(string dataKey, int attibutesFormat)
        {


            if (uniqueKeysList.ContainsKey(dataKey))
            {
                throw new Exception("The key is already added");
            }

            if (managingData.ContainsKey(attibutesFormat))
            {
                uniqueKeysList.Add(dataKey, attibutesFormat);
            }
            else
            {
                managingData.Add(attibutesFormat, new GeometryStorrage());
                uniqueKeysList.Add(dataKey, attibutesFormat);
                managingData[attibutesFormat].MarkBufferAttributePointers(attibutesFormat);
            }
        }

        public void BeforeRender()
        {
        }

        public void PostRender()
        {
        }
    }
}