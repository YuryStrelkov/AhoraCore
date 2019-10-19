using AhoraCore.Core.Buffers.StandartBuffers;
using System.Collections.Generic;

namespace AhoraCore.Core.Buffers.IBuffers
{
  public  interface IGeometryDataStorrage<T>
    {

        void AddItem(T geometryID, int verticesNumber, float[] verticesData);
        void AddItem(T geometryID, float[] verticesData, int[] indecesDtata);
        void AddItems(List<T> geometryIDs, List<FloatBuffer> verticesData, List<IntegerBuffer> indecesDtata);
    }
}
