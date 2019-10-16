using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Buffers.StandartBuffers;
using AhoraCore.Core.CES;
using AhoraCore.Core.CES.Components;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Materials;
using AhoraCore.Core.Materials.AbstractMaterial;
using AhoraCore.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        private static void ReadModel(JsonTextReader reader)
        {

            GameEntity model ;

            string entityID = "";

            int idx = 0;
            
            float[] tempVec = new float[3] { 0, 0, 0 };

            Dictionary<int,List<string>> AttribsMasks;

            ///F/oatBuffer[] Models;

            ///IntegerBuffer[] ModelsIndeces;

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

                    case "path":
                        reader.Read();

                        ModelLoader.LoadSceneModels(reader.Value.ToString());

                        GameEntityStorrage.Entities.GetItem(entityID).AddComponent(ComponentsTypes.GeometryComponent, new GeometryComponent(entityID));

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
                            tempVec[idx] = float.Parse(reader.Value.ToString());
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
                        GameEntityStorrage.Entities.GetItem(entityID).AddComponent(ComponentsTypes.MaterialComponent, new MaterialComponent(reader.Value.ToString()));
                        reader.Read();
                        break;

                    case "parent":
                        reader.Read();
                        GameEntityStorrage.Entities.SetNewParent(entityID, reader.Value.ToString());
                        reader.Read();
                        break;
                }
              ///  reader.Read();
            }
            reader.Read();

            GameEntityStorrage.Entities.GetItem(entityID).AddComponent(ComponentsTypes.ShaderComponent, new ShaderComponent("MateralShader"));

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
                       case "model":
                        {
                         reader.Read();
                            if (reader.TokenType == JsonToken.StartArray)
                            {
                                while (reader.TokenType != JsonToken.EndArray)
                                {
                                    ReadModel(reader);
                                }

                            } 
                        }
                        break; 
                }

           }

        }
    }
}
