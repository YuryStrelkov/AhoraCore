using System;
using AhoraCore.Core.Buffers.DataStorraging.StorrageTemplate;
using AhoraCore.Core.Materials;

namespace AhoraCore.Core.Buffers.DataStorraging
{
    public class TextureStorrage : TemplateStorrage<string, Texture>
    {

        private static TextureStorrage textures;

        public static TextureStorrage Textures
        {
            get { return textures; }
        }

        public static void Initilaze()
        {
            textures = new TextureStorrage();
        }

        private TextureStorrage():base()
        {
            AddItem("DefaultTexture", new Texture(1f,0.0f,0.0f));
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
