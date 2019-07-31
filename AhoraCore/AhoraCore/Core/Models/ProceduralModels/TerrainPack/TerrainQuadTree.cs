using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Materials;
using AhoraCore.Core.Shaders;
using OpenTK;
using System.Collections.Generic;


namespace AhoraCore.Core.Models.ProceduralModels.TerranPack
{
    // Сделать компонентом
    public class TerrainQuadTree: AComponent<IGameEntity>
    {
        public Material TerrainMaterial { get; private set; }

        public AShader TerrainShader { get; private set; }

        private static int rootNodes=8;

        List<TerrainNode> terrainNodes;

        public static int GetRootNodesNumber()
        {
            return rootNodes;
        }

         public override void Delete()
        {
            for(int i=0; i< terrainNodes.Count;i++ )
            {
                terrainNodes[i].Delete();
            }
        }

        public override void Enable()
        {
            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Enable();
            }
        }

        public override void Disable()
        {
            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Disable();
            }
        }

        public override void Input()
        {
            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Input();
            }
        }

        public override void Render()
        {
            TerrainShader.Bind();
            TerrainMaterial.Bind(TerrainShader);
            TerrainShader.SetUniform("viewMatrix", CameraInstance.Get().ViewMatrix);
            TerrainShader.SetUniform("projectionMatrix", CameraInstance.Get().PespectiveMatrix);

            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Render();
            }

        }

        public override void Update()
        {
            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Update();
            }
        }

        public override void Clear()
        {
            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Clear();
            }
        }

        public TerrainQuadTree(IGameEntity parent,TerrainConfig config):base()
        {

            SetParent(parent);

            TerrainMaterial = MaterialStorrage.Materials.GetItem("DefaultMaterial");

            TerrainShader = ShaderStorrage.Sahaders.GetItem("TerrainShader");

            terrainNodes = new List<TerrainNode>(rootNodes * rootNodes);

            for (int i=0; i < rootNodes ;i++)
            {
                for (int j = 0; j < rootNodes; j++)
                {
                    terrainNodes.Add(new TerrainNode(config, new Vector2(i / (float)rootNodes, j / (float)rootNodes), 0, new Vector2(i, j)));
                    terrainNodes[i * rootNodes + j].SetParent(this);
                ///    GameEntityStorrage.Entities.GetItem(id).AddComponent("TNModel", new Model("TNModel", "DefaultMaterial", "TerrainShader"));

                }
            }
            GetWorldTransform().SetScaling(config.ScaleXZ,  config.ScaleY, config.ScaleXZ);

            GetWorldTransform().SetTranslation(config.ScaleXZ/2, 0, config.ScaleXZ/2);
        }
    }
}
