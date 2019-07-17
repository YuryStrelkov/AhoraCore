using AhoraCore.Core.CES;
using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.Models
{
    public class Model<KeyType>:GameEntity
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
