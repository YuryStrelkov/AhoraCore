using AhoraCore.Core.CES;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Shaders;
using AhoraCore.Core.Materials;
using AhoraCore.Core.CES.ICES;
using System;

namespace AhoraCore.Core.Models
{
    public class Model: AComponent<IGameEntity>
    {
        public string ModelID { get; set; }
   
        public string ModelMaterialID { get; set; }

        public string ModelSaderID { get; set; }

        private AShader modelShader;

        private Material modelMaterial;

        public override void Delete()
        {
            GeometryStorageManager.Data.RemoveData(ModelID);
        }

        public override void Disable()
        {
           /// throw new NotImplementedException();
        }

        public override void Input()
        {
            //throw new NotImplementedException();
        }

        public override void Enable()
        {
            //throw new NotImplementedException();
        }

        public override void Render()
        {
            modelShader.Bind();
            modelShader.UpdateUniforms(GetParent());
            modelMaterial.Bind(modelShader);
            GeometryStorageManager.Data.RenderIteam(ModelID);
        }

        public override void Update()
        {
           /// throw new NotImplementedException();
        }

        public override void Clear()
        {
            /// throw new NotImplementedException();
        }

 

        public Model(string ModelID) :base()
        {
            this.ModelID = ModelID;
        }

        public Model(string ModelID, string ModelMaterialID, string ModelSaderID) : base()
        {
            this.ModelID = ModelID;
            this.ModelMaterialID = ModelMaterialID;
            this.ModelSaderID = ModelSaderID;
            modelMaterial = MaterialStorrage.Materials.GetItem(ModelMaterialID);
            modelShader = ShaderStorrage.Sahaders.GetItem(ModelSaderID);
        }
    }
}
