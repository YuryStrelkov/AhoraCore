using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Materials;
using System;

namespace AhoraCore.Core.CES.Components
{
    public class MaterialComponent : AComponent<IGameEntity>
    {

        private string materialID;

        public string MaterialID {
            get {
                return materialID;
                }
            set
            {
                materialID = value;
                MateriaL = MaterialStorrage.Materials.GetItem(MaterialID);
            }
        }

        public Material MateriaL { get; private set; }

        public override void Clear()
        {
        }

        public override void Delete()
        {
        }

        public override void Disable()
        {
        }

        public override void Enable()
        {
        }

        public override void Input()
        {
        }

        public override void Render()
        {
            MateriaL.Bind(GetParent().GetComponent<ShaderComponent>(ComponentsTypes.ShaderComponent).Shader);
            ///Console.WriteLine("3");
        }

        public override void Update()
        {
        }

        public MaterialComponent(string MaterialID)
        {
            this.MaterialID = MaterialID;
        }
    }
}
