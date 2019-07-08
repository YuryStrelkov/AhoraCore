using AhoraCore.Core.Buffers;
using AhoraProject.Ahora.Core.IRender;
using System;
using System.Collections.Generic;

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
            managingData[attibutesFormat].MarkBufferAttributePointers(attibutesFormat);
        }

        public void Render()
        {
            foreach (KeyValuePair<int, GeometryStorrage> kvp in managingData)
            {
                Render(kvp.Key);
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
                managingData[uniqueKeysList[id]].BeforeRender();
                managingData[uniqueKeysList[id]].RenderIteam(id);
                managingData[uniqueKeysList[id]].PostRender();
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
            foreach (KeyValuePair<int, GeometryStorrage> kvp in managingData)
            {
                kvp.Value.ClearStorrage();
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
                managingData.Add(attibutesFormat, new GeometryStorrage(1000));
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
