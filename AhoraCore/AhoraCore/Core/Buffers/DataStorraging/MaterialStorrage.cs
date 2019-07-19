using System;
using AhoraCore.Core.Buffers.DataStorraging.StorrageTemplate;
using AhoraCore.Core.Materials;

namespace AhoraCore.Core.Buffers.DataStorraging
{
    public class MaterialStorrage : TemplateStorrage<string, Material>
    {
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
