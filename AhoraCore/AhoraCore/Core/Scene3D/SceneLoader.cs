using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Materials;
using AhoraCore.Core.Materials.AbstractMaterial;
using Newtonsoft.Json;
using System.IO;

namespace AhoraCore.Core.Scene3D
{
    public static class SceneLoader
    {
        private static void ReadTextures(JsonTextReader reader)
        {
            reader.Read();
            if (reader.TokenType == JsonToken.StartArray)
            {
                while (reader.TokenType != JsonToken.EndArray)
                {
                    reader.Read();

                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        reader.Read(); reader.Read();
                        string type = reader.Value.ToString();
                        reader.Read(); reader.Read();
                        string name = reader.Value.ToString();
                        reader.Read(); reader.Read();
                        string path = reader.Value.ToString();
                        TextureStorrage.Textures.AddItem(name, new Texture(path));
                    }
                }
            }
        }


        public static void LoadScene(string sceneFile)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(sceneFile));
            AMaterial tmpMat;

            while (reader.Read())
            {
                if (reader.Value == null)
                {
                    continue;
                }
                  if (reader.TokenType != JsonToken.PropertyName)
                {
                    continue;
                }

                switch (reader.Value.ToString())
                {
                    case "texture":
                        {
                           ReadTextures(reader);
                        } break;
                    case "mateial":
                        {
                            reader.Read();
                            if (reader.TokenType == JsonToken.StartArray)
                            {
                                while (reader.TokenType != JsonToken.EndArray)
                                {
                                   tmpMat = new Material();
                                   tmpMat.ReadMaterial(reader);
                                   MaterialStorrage.Materials.AddItem(tmpMat.MaterialName, tmpMat);
                                }

                            }
                        }
                        break;

                }

           }

        }
    }
}
