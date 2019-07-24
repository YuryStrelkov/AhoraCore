using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.Models.ProceduralModels.TerranPack;


namespace AhoraCore.Core.Models.ProceduralModels
{
    public class Terrain:GameEntity
    {
        private TerrainConfig configuration;

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
            GameEntityStorrage.Entities.AddItem("terrain", this);
            GameEntityStorrage.Entities.AddItem("terrain","terrainQuadTree", new TerrainQuadTree(configuration));
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
