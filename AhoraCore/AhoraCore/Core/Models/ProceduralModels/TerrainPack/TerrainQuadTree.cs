using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.CES;
using AhoraCore.Core.DataManaging;
using OpenTK;

namespace AhoraCore.Core.Models.ProceduralModels.TerranPack
{
    public class TerrainQuadTree: GameEntity
    {
        private static int rootNodes=8;

        public static int GetRootNodesNumber()
        {
            return rootNodes;
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

      /*  public void UpdateQuadTree()
        {
            GameEntityStorrage.Entities.Update("terrainQuadTree");
        }
        */
        public TerrainQuadTree(TerrainConfig config)
        {
            string id;
            for (int i=0;i<rootNodes ;i++)
            {
                for (int j = 0; j < rootNodes; j++)
                {
                    id = "N_" + i + "_" + j;
                    GameEntityStorrage.Entities.AddItem("terrainQuadTree", id, new TerrainNode(id, config, new Vector2(i/(float)rootNodes, j / (float)rootNodes),0,new Vector2(i,j)));

                    GameEntityStorrage.Entities.GetItem(id).AddComponent("QTNodeModel", new Model("QTNodeModel", "DefaultMaterial", "DefaultShader"));

                }
            }
            GeometryStorrageManager.Data.AddGeometry(VericesAttribytes.V_POSITION, "QTNodeModel", GeneratePath(), GeneratePathIndeces());

            GetWorldTransform().SetScaling(config.ScaleXZ,  config.ScaleY, config.ScaleXZ);

            GetWorldTransform().SetTranslation(config.ScaleXZ/2, 0, config.ScaleXZ/2);
        }
    }
}
