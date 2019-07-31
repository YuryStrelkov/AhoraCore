using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Models.ProceduralModels.TerranPack;
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

            GeometryStorrageManager.Data.AddGeometry(VericesAttribytes.V_POSITION, "TerrainNodeModel", GeneratePath());

             AddComponent("terrain_qt",new TerrainQuadTree(this,configuration));
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
