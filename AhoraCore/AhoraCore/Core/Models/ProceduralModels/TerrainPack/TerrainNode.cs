using AhoraCore.Core.Cameras;
using AhoraCore.Core.Models.ProceduralModels.TerrainPack;
using OpenTK;


namespace AhoraCore.Core.Models.ProceduralModels.TerranPack
{
    /// <summary>
    /// TODO Трансформациии увязать нормальным образом 
    /// </summary>

    public  class TerrainNode: ATerrainNode
    {


        public override void Render()
        {
            
            if (isLeaf)
            {

                GetParent().TerrainShader.SetUniform("index", Index);

                GetParent().TerrainShader.SetUniformf("gap", Gap);

                GetParent().TerrainShader.SetUniformi("lod", Lod);

                GetParent().TerrainShader.SetUniform("location", Location);

                GetParent().TerrainShader.SetUniform("WorldTransMatrix", GetParent().GetWorldTransform().GetTransformMat());

                GetParent().TerrainShader.SetUniform("LocTransMatrix", GetNodeLoclTrans().GetTransformMat());

                GetParent().NodePachModel.DrawPatch();    
           }
            else
            {
                childsNodes[0].Render();
                childsNodes[1].Render();
                childsNodes[2].Render();
                childsNodes[3].Render();
            }
        }

        public override void Update()
        {
           if (CameraInstance.Get().GetWorldTransform().Position.Y > config.ScaleY)
            {
                worldPosition.Y = config.ScaleY;
            }
            else
            {
                worldPosition.Y = CameraInstance.Get().GetWorldTransform().Position.Y;
            }
            UpdateChildsNodes();
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

        public TerrainNode(TerrainConfig config, Vector2 location, int lod, Vector2 index) : base(config, location, lod, index)
        {

        }

    }
}
