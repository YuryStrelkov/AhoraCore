using System;
using AhoraCore.Core.Buffers.DataStorraging.StorrageTemplate;
using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.Buffers.DataStorraging
{
    public class ShaderStorrage : TemplateStorrage<string, AShader>
    {

        private static ShaderStorrage shaders;

        public static ShaderStorrage Sahaders
        {
            get { return shaders; }
        }

        public static void Initilaze()
        {
            if (shaders!=null)
            {
                shaders.DeleteStorrage();
            }
            shaders = new ShaderStorrage();
            shaders.AddItem("DefaultShader", new DefaultShader());
            shaders.AddItem("MateralShader", new MaterialShader());
            shaders.AddItem("AtmosphereShader", new AtmosphereShader());
            shaders.AddItem("GUIShader",new GUIShader());//GUITextShader()
            shaders.AddItem("GUITextShader", new GUITextShader());

        }

        private ShaderStorrage():base()
        {

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
