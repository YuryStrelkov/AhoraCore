using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.Buffers.IBuffers
{
  public  interface IGeometryDataStorrage<T>
    {
        void AddItem(T geometryID, float[] verticesData, int[] indecesDtata);
    }
}
