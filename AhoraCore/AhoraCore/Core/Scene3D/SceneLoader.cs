using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.CES;
using AhoraCore.Core.CES.Components;
using AhoraCore.Core.Materials;
using AhoraCore.Core.Materials.AbstractMaterial;
using AhoraCore.Core.Models;
using Newtonsoft.Json;
using OpenTK;
using System;
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

        private static void ReadGeometry(JsonTextReader reader)
        {
            reader.Read();

            string name = "";

            if (reader.TokenType == JsonToken.StartArray)
            {
                while (reader.TokenType != JsonToken.EndArray)
                {
           

                    while (reader.TokenType != JsonToken.EndObject)
                    {
                        if (reader.Value == null)
                        {
                            reader.Read();
                            continue;
                        }
                        switch (reader.Value.ToString())
                        {
                            case "name":
                                reader.Read();
                                name = reader.Value.ToString();
                                reader.Read();
                                break;
                            case "path":
                                reader.Read();
                                if (name!="")
                                {
                                    ModelLoader.LoadMesh(name, reader.Value.ToString());
                                    reader.Read();
                                    break;
                                }

                                ModelLoader.LoadAllMeshes(reader.Value.ToString());
                                reader.Read();
                                break;
                        }
                    }
                    reader.Read();
                    name = "";
                }
            }
        }

        private static void ReadGameEntity(JsonTextReader reader)
        {

            GameEntity model ;

            string entityID = "";

            int idx = 0;
            
            float[] tempVec = new float[3] { 0, 0, 0 };

            while (reader.TokenType != JsonToken.EndObject)
            {
                if (reader.Value == null)
                {
                    reader.Read();
                    continue;
                }

                Console.WriteLine(reader.Value.ToString());
                switch (reader.Value.ToString())
                {
                    case "name":
                        reader.Read();
                        entityID = reader.Value.ToString();
                        model = new GameEntity(entityID);
                        GameEntityStorrage.Entities.AddItem(entityID, model);
                        reader.Read();
                    break;

                    case "geometry":
                        reader.Read();

                      ///  ModelLoader.LoadAllMeshes(reader.Value.ToString());

                        GameEntityStorrage.Entities.GetItem(entityID).AddComponent(new GeometryComponent(reader.Value.ToString()));

                        reader.Read();
                        break;

                    case "worldPosition":
                        while (reader.TokenType != JsonToken.StartArray)
                        {
                            reader.Read();
                        }
                        reader.Read();
                        while (reader.TokenType != JsonToken.EndArray)
                        {
                            tempVec[idx] = float.Parse(reader.Value.ToString());
                            reader.Read();
                            idx++;
                        }
                        idx = 0;
                        GameEntityStorrage.Entities.GetItem(entityID).SetWorldTranslation(tempVec[0], tempVec[1], tempVec[2]);
                        reader.Read();
                        break;

                    case "worldRotation":
                        while (reader.TokenType != JsonToken.StartArray)
                        {
                            reader.Read();
                        }
                        reader.Read();
                        while (reader.TokenType != JsonToken.EndArray)
                        {
                            tempVec[idx] = float.Parse(reader.Value.ToString())/360.0f*MathHelper.TwoPi;
                            reader.Read();
                            idx++;
                        }
                        idx = 0;
                        GameEntityStorrage.Entities.GetItem(entityID).SetWorldRotation(tempVec[0], tempVec[1], tempVec[2]);
                         reader.Read();
                        break;

                    case "worldScale":
                        while (reader.TokenType != JsonToken.StartArray)
                        {
                            reader.Read();
                        }
                        reader.Read();
                        while (reader.TokenType != JsonToken.EndArray)
                        {
                            tempVec[idx] = float.Parse(reader.Value.ToString());
                            reader.Read();
                            idx++;
                        }
                        idx = 0;
                        GameEntityStorrage.Entities.GetItem(entityID).SetWorldScale(tempVec[0], tempVec[1], tempVec[2]);
                        reader.Read();
                        break;

                    case "material":
                        reader.Read();
                        GameEntityStorrage.Entities.GetItem(entityID).AddComponent(new MaterialComponent(reader.Value.ToString()));
                        reader.Read();
                        break;

                    case "parent":
                        reader.Read();
                        GameEntityStorrage.Entities.SetNewParent(entityID, reader.Value.ToString());
                        reader.Read();
                        break;
                    default: reader.Read();break;
                }
              ///  reader.Read();
            }
            reader.Read();

            GameEntityStorrage.Entities.GetItem(entityID).AddComponent( new ShaderComponent("MateralShader"));

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
                    case "geometry":
                        ReadGeometry(reader); ///reader.Read();
                        break;
                    case "gameEntity":
                 
                         reader.Read();
                            if (reader.TokenType == JsonToken.StartArray)
                            {
                                while (reader.TokenType != JsonToken.EndArray)
                                {
                                    ReadGameEntity(reader);
                                }

                            } 
                       
                        break; 
                }

           }

        }
    }
}
