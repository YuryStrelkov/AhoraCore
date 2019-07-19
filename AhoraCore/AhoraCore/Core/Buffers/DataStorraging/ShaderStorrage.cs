using System;
using AhoraCore.Core.Buffers.DataStorraging.StorrageTemplate;
using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.Buffers.DataStorraging
{
    public class ShaderStorrage : TemplateStorrage<string, AShader>
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
