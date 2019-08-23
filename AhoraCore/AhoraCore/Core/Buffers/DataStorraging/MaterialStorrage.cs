using AhoraCore.Core.Buffers.DataStorraging.StorrageTemplate;
using AhoraCore.Core.Materials;
using AhoraCore.Core.Materials.AbstractMaterial;

namespace AhoraCore.Core.Buffers.DataStorraging
{
    public class MaterialStorrage : TemplateStorrage<string, Material>
    {
        private static MaterialStorrage materials_;



        public static MaterialStorrage Materials
        {
            get { return materials_; }
        }

        public static void Initilaze()
        {
            materials_ = new MaterialStorrage();
        }

        private MaterialStorrage():base()
        {
            AddItem("DefaultMaterial", new Material());
            GetItem("DefaultMaterial").AssignTexture2Channel("DefaultTexture",TextureChannels.Diffuse);//   SetDiffuse();
            GetItem("DefaultMaterial").AssignTexture2Channel("DefaultTexture", TextureChannels.Normal); ;
            GetItem("DefaultMaterial").AssignTexture2Channel("DefaultTexture", TextureChannels.Specular); ;
        }

        public override void ClearIteamData(string ID)
        {
            Iteams[ID].Data.Clear();
        }

        public override void DeleteIteamData(string ID)
        {
            Iteams[ID].Data.Delete();
        }
    }
}
