using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Materials;
using AhoraCore.Core.Models.ProceduralModels.TerranPack;
using AhoraCore.Core.Shaders;
using System;

namespace AhoraCore.Core.Models.ProceduralModels
{
    public class Terrain:GameEntity
    {
       

        public static void CreateTerrain()
        {
            if (GameEntityStorrage.Entities.Iteams.ContainsKey("terrain"))
            {
                Console.WriteLine("Terrain is allready created");
                return;
            }

            Terrain t = new Terrain();
            t.Init(Properties.Resources.TerrainSettings, false);
        }


        private Terrain():base()
        {

        }

        private TerrainConfig configuration;

        public TerrainConfig Configuration
        {
            get
            {
                return configuration;
            }
        }


        public float[] GeneratePath()
        {
            return new float[] {
             0,0,0,
             0.333f, 0, 0,
             0.666f, 0, 0,
             1, 0, 0,

             0, 0.333f, 0,
             0.333f, 0.333f, 0,
             0.666f, 0.333f, 0,
             1, 0.333f, 0,



             0, 0.666f, 0,
             0.333f, 0.666f, 0,
             0.666f, 0.666f, 0,
             1, 0.666f, 0,


             0, 1, 0,
             0.333f, 1, 0,
             0.666f, 1, 0,
             1, 1, 0 };
        }

        public int[] GeneratePathIndeces()
        {

            ///нужно проверить,в такм ли порядке идёт отрисовка 
            return new int[] {
             1,2,5,/**/5,6,2,
             2,3,6,/**/6,3,7,
             3,4,7,/**/7,4,8,
             2,3,6,/**/6,3,7,

             5,6,10,/**/10,6,11,
             6,7,11,/**/11,7,12,
             7,8,12,/**/12,8,13,
             8,9,13,/**/13,9,14,

             10,11,14,/**/14,11,15,
             11,12,15,/**/15,12,16,
             12,13,16,/**/16,13,17,
             13,14,17,/**/17,14,18
            };

        }



        public void Init(string config, bool fromfile = true)
        {
            configuration = new TerrainConfig();

            if (fromfile)
            {
                configuration.LoadConfigFromFile(config);
            } else
            {
                configuration.LoadConfigFromString(config);
            }

            GameEntityStorrage.Entities.AddItem("terrain", this);

            ShaderStorrage.Sahaders.AddItem("TerrainShader", new TerrainShader());

            GeometryStorrageManager.Data.AddGeometry(VericesAttribytes.V_POSITION, "TerrainNodeModel", GeneratePath(), GeneratePathIndeces());

             AddComponent("terrain_qt",new TerrainQuadTree(this,configuration));
            //GameEntityStorrage.Entities.AddItem("terrain","terrainQuadTree", new TerrainQuadTree(configuration));
        }

        public new void Update()
        {           
            if (CameraInstance.Get().IsUpdated)///only when moved
            {
                  base.Update();
            }
        }
    }
}
