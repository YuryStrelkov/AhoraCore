using AhoraCore.Core.Cameras;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Models.ProceduralModels.TerrainPack;
using OpenTK;

namespace AhoraCore.Core.Models.ProceduralModels.TerranPack
{

    /// <summary>
    /// Сделать компонентом!!!
    /// </summary>
    public  class TerrainNode: ATerrainNode
    {
     

        public override void Render()
        {
            if (isLeaf)
            {
                GetParent().TerrainShader.Bind();
                GetParent().TerrainMaterial.Bind(GetParent().TerrainShader);
                GetParent().TerrainShader.SetUniform("viewMatrix", CameraInstance.Get().ViewMatrix);
                GetParent().TerrainShader.SetUniform("projectionMatrix", CameraInstance.Get().PespectiveMatrix);
                ///Node_0
                GetParent().TerrainShader.SetUniform("WorldTransMatrix", childsNodes[0].GetNodeWorldTrans().GetTransformMat());
                GetParent().TerrainShader.SetUniform("LocTransMatrix", childsNodes[0].GetNodeLoclTrans().GetTransformMat());
                GeometryStorrageManager.Data.RenderIteam("TerrainNodeModel");
                ///Node_0
                GetParent().TerrainShader.SetUniform("WorldTransMatrix", childsNodes[1].GetNodeWorldTrans().GetTransformMat());
                GetParent().TerrainShader.SetUniform("LocTransMatrix", childsNodes[1].GetNodeLoclTrans().GetTransformMat());
                GeometryStorrageManager.Data.RenderIteam("TerrainNodeModel");
                ///Node_0
                GetParent().TerrainShader.SetUniform("WorldTransMatrix", childsNodes[2].GetNodeWorldTrans().GetTransformMat());
                GetParent().TerrainShader.SetUniform("LocTransMatrix", childsNodes[2].GetNodeLoclTrans().GetTransformMat());
                GeometryStorrageManager.Data.RenderIteam("TerrainNodeModel");
                ///Node_0
                GetParent().TerrainShader.SetUniform("WorldTransMatrix", childsNodes[3].GetNodeWorldTrans().GetTransformMat());
                GetParent().TerrainShader.SetUniform("LocTransMatrix", childsNodes[3].GetNodeLoclTrans().GetTransformMat());
                GeometryStorrageManager.Data.RenderIteam("TerrainNodeModel");
            }
        }

        public override void Update()
        {
            if (CameraInstance.Get().GetLocalTransform().Position.Y > config.ScaleY)
            {
                worldPosition.Y = config.ScaleY;
            }
            else
            {
                worldPosition.Y = CameraInstance.Get().GetLocalTransform().Position.Y;
                UpdateChildsNodes();
            }
      //      base.Update();
        }

        protected override void AddChildNodes(int lod)
        {
            if (IsLeaf)
            {
                IsLeaf = false;
            }
            if (childsNodes.Count == 0)
            {
                for (int i = 0 ; i < 2 ; i++ )
                {
                    for (int j = 0; j < 2; j++)
                    {
                        childsNodes.Add( new TerrainNode(config, new Vector2(i * gap / 2, j * gap / 2), lod, new Vector2(i, j)));
                    }
                }
           }
        }

        public override void Delete()
        {
        }

        public override void Enable()
        {
        }

        public override void Disable()
        {
        }

        public override void Input()
        {
        }

        public override void Clear()
        {
        }

        public TerrainNode(TerrainConfig config, Vector2 location, int lod, Vector2 index):base( config,  location,  lod,  index)
        {
            
        }

    }
}
