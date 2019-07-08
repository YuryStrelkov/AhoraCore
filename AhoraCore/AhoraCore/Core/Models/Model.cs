using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.Models
{
    public struct Mesh
    {
        public int MeshGeometryID { get; set; }

        public int MeshMaterialID { get; set; }

        public int MeshSaderID { get; set; }

        public Mesh(int m_ID,int mat_ID, int s_ID)
        {
            MeshGeometryID = m_ID;

            MeshMaterialID = mat_ID;

            MeshSaderID = s_ID;
        }
    }

    public class Model
    {
        public Dictionary<int, Mesh> Meshes { get; set; }

        public Model(string fileName)
        {
            Meshes = new Dictionary<int, Mesh>();
        }
    }
}
