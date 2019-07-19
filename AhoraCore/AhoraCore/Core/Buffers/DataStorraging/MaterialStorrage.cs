using System;
using AhoraCore.Core.Buffers.DataStorraging.StorrageTemplate;
using AhoraCore.Core.Materials;

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
            GetItem("DefaultMaterial").SetDiffuse("DefaultTexture");
            GetItem("DefaultMaterial").SetNormals("DefaultTexture");
            GetItem("DefaultMaterial").SetSpecular("DefaultTexture");
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
