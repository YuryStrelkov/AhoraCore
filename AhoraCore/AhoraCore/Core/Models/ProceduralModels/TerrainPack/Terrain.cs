using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.CES.Components;
using AhoraCore.Core.Materials;
using AhoraCore.Core.Models.ProceduralModels.TerrainPack;
using System;

namespace AhoraCore.Core.Models.ProceduralModels
{
    sealed class TerrainShaderComponent : ShaderComponent
    {
        public TerrainShaderComponent(string ShaderID) : base(ShaderID)
        {
            ComponentType = ComponentsTypes.TerrainShader;
        }
    }

    sealed class TerrainGrassShaderComponent : ShaderComponent
    {
        public TerrainGrassShaderComponent(string ShaderID) : base(ShaderID)
        {
            ComponentType = ComponentsTypes.TerrainFloraShader;
        }
    }

    sealed public class Terrain:GameEntity
    {
        private TerrainConfig configuration;

        public static void CreateTerrain()
        {
            if (GameEntityStorrage.Entities.Iteams.ContainsKey("terrain"))
            {
                Console.WriteLine("Terrain is allready created");
                return;
            }

            Terrain t = new Terrain("terrain");

            GameEntityStorrage.Entities.AddItem(t.EntityID, t);

            t.AddComponent(new TerrainQuadTree(t, t.configuration));

        }

        private Terrain(string ID):base(ID)
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

            configuration.LoadConfigFromFile(config);

            ShaderStorrage.Sahaders.AddItem("TerrainShader", new TerrainShader());

            ShaderStorrage.Sahaders.AddItem("TerrainGrassShader", new TerrainGrassShader());

            TextureStorrage.Textures.AddItem("GrassTexture", new Texture(Properties.Resources.grass));
 
            AddComponent(new TerrainShaderComponent("TerrainShader"));
            AddComponent(new TerrainGrassShaderComponent("TerrainGrassShader"));
            AddComponent(new MaterialComponent("TerrainMaterial"));
            RemoveComponent(ComponentsTypes.GeometryComponent);
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
