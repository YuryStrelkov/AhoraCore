using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.StandartBuffers;
using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.CES.Components;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Materials;
using AhoraCore.Core.Models.ProceduralModels.TerrainPack;
using System;

namespace AhoraCore.Core.Models.ProceduralModels
{
    public class Terrain:GameEntity
    {
        private TerrainConfig configuration;

        public static void CreateTerrain()
        {
            if (GameEntityStorrage.Entities.Iteams.ContainsKey("terrain"))
            {
                Console.WriteLine("Terrain is allready created");
                return;
            }

            Terrain t = new Terrain();

            GameEntityStorrage.Entities.AddItem("terrain", t);
        }

        private Terrain():base()
        {
            Init(Properties.Resources.TerrainSettings, false);
        }

        public TerrainConfig Configuration
        {
            get
            {
                return configuration;
            }
        }

        private void CreateGrassLods()
        {
            FloatBuffer []vbuffers ;

            IntegerBuffer []ibuffers;

            int[] AttribsMasks;


            ModelLoader.LoadModel("D:\\GitHub\\AhoraCore\\AhoraCore\\AhoraCore\\Resources\\grass_lods.obj", out AttribsMasks, out vbuffers, out ibuffers);

            GeometryStorageManager.Data.AddGeometrySet(AttribsMasks[0], new string[] { "grass_lod_0", "grass_lod_1", "grass_lod_2"}, vbuffers, ibuffers);

            //ModelLoader.LoadModel("D:\\GitHub\\AhoraCore\\AhoraCore\\AhoraCore\\Resources\\grass_lods_.obj", out AttribsMasks, out vbuffers, out ibuffers);

            //GeometryStorageManager.Data.AddGeometrySet(AttribsMasks[0], new string[]{ "grass_lod_0", "grass_lod_1" , "grass_lod_2", "grass_lod_3", "grass_lod_4" }, vbuffers, ibuffers); 

        }

        public void Init(string config, bool fromfile = true)
        {
            configuration = new TerrainConfig();

            if (fromfile)
            {
                configuration.LoadConfigFromFile(config);
            }
            else
            {
                configuration.LoadConfigFromString(config);
            }

            ShaderStorrage.Sahaders.AddItem("TerrainShader", new TerrainShader());

            ShaderStorrage.Sahaders.AddItem("TerrainGrassShader", new TerrainGrassShader());

            TextureStorrage.Textures.AddItem("GrassTexture", new Texture(Properties.Resources.grass));

            ///MaterialStorrage.Materials.AddItem("TerrainMaterial", new TerrainMaterial());
            
             
            AddComponent(ComponentsTypes.TerrainShader, new ShaderComponent("TerrainShader"));
            AddComponent(ComponentsTypes.TerrainFloraShader, new ShaderComponent("TerrainGrassShader"));
            AddComponent(ComponentsTypes.MaterialComponent, new MaterialComponent("TerrainMaterial"));
            AddComponent(ComponentsTypes.NodeComponent, new TerrainQuadTree(this, configuration));
            RemoveComponent(ComponentsTypes.GeometryComponent);
            CreateGrassLods();


        }

        public new void Update()
        {           
             if (CameraInstance.Get().IsUpdated)///only when moved
            {
                GetComponent(ComponentsTypes.NodeComponent).Update();
            }
        }
        
        public new void Render()
        {
            GetComponent(ComponentsTypes.NodeComponent).Render();
        }
    }
}
