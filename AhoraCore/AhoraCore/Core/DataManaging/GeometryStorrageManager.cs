using AhoraCore.Core.Buffers;
using AhoraCore.Core.Buffers.StandartBuffers;
using AhoraProject.Ahora.Core.IRender;
using System;

namespace AhoraCore.Core.DataManaging
{
    public class GeometryStorrageManager:AManager<GeometryStorrage, int, string>, IRedreable<string>
    {
        public GeometryStorrageManager():base()
        {
        }

        public void AddGeometry(string geoID, int attibutesFormat, float [] vData, int [] iData)
        {
            AppendData(geoID, attibutesFormat);
            managingData[attibutesFormat].AddItem(geoID, vData, iData);
        }

        public void AddGeometry(string geoID, int attibutesFormat, FloatBuffer vData, IntegerBuffer iData)
        {
            AppendData(geoID, attibutesFormat);
            managingData[attibutesFormat].AddItem(geoID, vData.BufferData, iData.BufferData);
             
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
                Console.WriteLine(e.StackTrace.ToString());
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
            foreach (GeometryStorrage val  in managingData.Values)
            {
                val.ClearStorrage();
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
