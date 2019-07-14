using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.Models
{
    public struct Mesh<KeyType>
    {
        public KeyType MeshGeometryID { get; set; }

        public KeyType MeshMaterialID { get; set; }

        public KeyType MeshSaderID { get; set; }
        
        public void CheckCameraintesectionMesh(Camera cam)
        {

        }

        public Mesh(KeyType m_ID,KeyType mat_ID, KeyType s_ID)
        {
            MeshGeometryID = m_ID;

            MeshMaterialID = mat_ID;

            MeshSaderID = s_ID;
        }
    }

    public class Model<KeyType> :Node
    {
        public KeyType ModelID { get; set; }
   
        public KeyType ModelMaterialID { get; set; }

        public KeyType ModelSaderID { get; set; }

        public void UpdateShaderModelTransForm(AShader shader)
        {
            shader.SetUniform("transformationMatrix", GetWorldTransform().GetWorldTransformMat());
        }

        public Model(KeyType ModelID) :base()
        {
            this.ModelID = ModelID;
        }

        public Model(KeyType ModelID, KeyType ModelMaterialID, KeyType ModelSaderID) : base()
        {
            this.ModelID = ModelID;
            this.ModelMaterialID = ModelMaterialID;
            this.ModelSaderID = ModelSaderID;

        }
    }
}
