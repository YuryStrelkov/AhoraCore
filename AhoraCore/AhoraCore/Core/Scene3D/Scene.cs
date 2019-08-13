using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Models;
using AhoraProject.Ahora.Core.IRender;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using AhoraCore.Core.Buffers.StandartBuffers;
using AhoraCore.Core.CES;

namespace AhoraCore.Core.Scene3D
{
    public class Scene ///: GameEntityStorrage,IRedreable<string>
    {
        public void Load(string path)
        {
             Dictionary<int, List<string>> AttrMasksPerModelNames;
             Dictionary<int, List<FloatBuffer>> Vertices;
             Dictionary<int, List<IntegerBuffer>> Indeces;
             Dictionary<int, List<int>> MaterialIDs;
            ///*Load Materials Here*///
            ///*Load Heirarhy Here*///
            ///
            Assimp.Scene scn = ModelLoader.LoadScene(path);
            ///Загрузка модели или сцены
    
            ///Загурка вершин и индексов в виде Dictionary< маска атрибутов меша, список буферов вершин/индексов>
            ///Загрузка ID материалов в виде < маска атрибутов меша, список ID материалов>
            ModelLoader.LoadSceneGeometry(ref scn, out AttrMasksPerModelNames,out Vertices,out Indeces,out MaterialIDs);
            ///Загрузка материалов -> ModelLoader.LoadSceneMaterials(Dictionary<TexID, Texture>, Dictionary<MatID, Material>) Material - содержит набор ID из словаря с текстурами
            foreach (int attrMask in AttrMasksPerModelNames.Keys)
            {
                for (int nameIdx=0; nameIdx< AttrMasksPerModelNames[attrMask].Count; nameIdx++)
                {   ///Индексирование модели происходит непоследственно в буфере SceneMeshes, что бы нариовать модель и этого буфера, просто запроси ее отрисовку
                    GeometryStorageManager.Data.AddGeometrySet( attrMask, AttrMasksPerModelNames[attrMask].ToArray(), Vertices[attrMask].ToArray(), Indeces[attrMask].ToArray());
                }
            }
       //     ModelLoader.LoadHeirarhy( scn.RootNode,  this);
        }

        //public Scene():base()
        //    {
        //      //  SceneMeshes = new GeometryStorrageManager();
        //    }

        //public Scene(string path) : base()
        //{
        //  ///  SceneMeshes = new GeometryStorrageManager();
        //    Load( path);
        //}

        public void BeforeRender()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.ClearColor(0.5f, 0.5f, 0.5f, 1);

            GL.Enable(EnableCap.DepthTest);
        }

        public void Render()
        {
            BeforeRender();

          ///  Render(RootID);

            PostRender();
        }

        public void RenderIteam(string iteamID)
        {
            
                ///Enable Shader;
              ///  SceneShaders.GetItem(SceneModels[iteamID].ModelSaderID).Bind();
                ///Enable Material;
              ///  SceneMaterials.GetItem(SceneModels[iteamID].ModelMaterialID).Bind(SceneShaders.GetItem(SceneModels[iteamID].ModelSaderID));
                ///Updating Model transformayion un shader
               // SceneModels[iteamID].UpdateShaderModelTransForm(SceneShaders.GetItem(SceneModels[iteamID].ModelSaderID));

               // SceneMeshes.RenderIteam(iteamID);
 
        }

        public void PostRender()
        {
            
        }
    }
}
