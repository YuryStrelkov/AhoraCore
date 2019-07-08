using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.UniformsBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.Buffers.DataStorraging
{

    public struct UniformsStorrageIteam<T>
    {
        public T ID { get; private set; }

        public T ParentID { get; set; }

        public T ChildID { get; set; }

        public int V_start_i { get; private set; }

        public int V_length { get; private set; }

        public void shiftLeft(int v_shift, int i_shift)
        {
            V_start_i -= v_shift;
        }

        public void shiftRight(int v_shift, int i_shift)
        {
            V_start_i += v_shift;
        }

        public UniformsStorrageIteam(T ID_, T parenID_, T childID, int v_start, int v_length)
        {
            ID = ID_;
            ParentID = parenID_;
            ChildID = childID;
            V_start_i = v_start;
            V_length = v_length;
        }
    }

    public class UniformsStorrage : UniformsBuffer<string>, IDataStorrage<string>
    {
        private Dictionary<string, UniformsStorrageIteam<string>> GeometryItemsList;

        private string LaysKey = "", RootKey;

        public void AddItem(string geometryID, float[] verticesData, int[] indecesDtata)
        {
            throw new NotImplementedException();
        }

        public void ClearStorrage()
        {
            throw new NotImplementedException();
        }

        public void DeleteStorrage()
        {
            throw new NotImplementedException();
        }

        public void MergeItems(string[] ItemIDs)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(string geometryID)
        {
            throw new NotImplementedException();
        }

        public void ResolveKeys(string geometryID)
        {
            throw new NotImplementedException();
        }
    }
}
