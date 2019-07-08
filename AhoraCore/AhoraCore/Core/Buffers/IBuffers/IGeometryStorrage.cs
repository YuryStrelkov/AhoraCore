using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.Buffers.IBuffers
{
    public interface IGeometryStorrage<T>
    {

        void AddGeometry(T geometryID, float[] verticesData, int[] indecesDtata);

        void ResolveKeys(T geometryID);

        void RemoveGeometry(T geometryID);

        void MergeGeometryItems(T[] geometryIDs);

        void ClearGeomeryStorrage();

        void DeleteGeomeryStorrage();

    }
}
