using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.Models.ProceduralModels.TerranPack;
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

            ShaderStorrage.Sahaders.AddItem("TerrainShader", new TerrainShader());

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
