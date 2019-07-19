using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Models;
using AhoraProject.Ahora.Core.IRender;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using AhoraCore.Core.Buffers.StandartBuffers;
using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.CES;

namespace AhoraCore.Core.Scene3D
{
    public class Scene : GameEntityStorrage,IRedreable<string>
    {
        /// <summary>
        /// Так получилось, что все меши отсортированы по наборам атрибутов, однако все меши с одним и тем же
        /// набором атрибутов хранятся в одном буфере, а доступ к конкретной модели осущетсвляется по индексам.
        /// </summary>
        public GeometryStorrageManager SceneMeshes { get; private set; }
        //Нужна хотя бы одна дефолтная текстура
        public TextureStorrage SceneTextures { get; private set; }
        //Нужен хотя бы один дефолтный мфтериал
        public MaterialStorrage SceneMaterials { get; private set; }
     
        public Dictionary<string, Model<string>> SceneModels { get; private set; }///убрать метод Bind(BindingTarget target) в отдельный интерфейс и заменить 
            // Dictionary<string, Model<string>>  на ModelStorrage
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
                    SceneMeshes.AddGeometrySet( attrMask, AttrMasksPerModelNames[attrMask].ToArray(), Vertices[attrMask].ToArray(), Indeces[attrMask].ToArray());
                }
            }
            ModelLoader.LoadHeirarhy( scn.RootNode,  this);
        }

        public Scene():base()
            {
                SceneMeshes = new GeometryStorrageManager();
                SceneTextures = new TextureStorrage();
                SceneMaterials = new MaterialStorrage();
                SceneModels = new Dictionary<string, Model<string>>();
            }

        public Scene(string path) : base()
        {
            SceneMeshes = new GeometryStorrageManager();
            SceneTextures = new TextureStorrage();
            SceneMaterials = new MaterialStorrage();
            SceneModels = new Dictionary<string, Model<string>>();

            Load( path);
        }

        public void BeforeRender()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.ClearColor(0.5f, 0.5f, 0.5f, 1);

            GL.Enable(EnableCap.DepthTest);
        }

        public void Render()
        {
            BeforeRender();

            Render(RootID);

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

                SceneMeshes.RenderIteam(iteamID);
 
        }

        public void PostRender()
        {
            
        }
    }
}
