using AhoraCore.Core.Cameras;
using AhoraCore.Core.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
    /// <summary>
    /// TODO Трансформациии увязать нормальным образом 
    /// </summary>

    sealed public  class TerrainNode: ATerrainNode
    {
        

        public string grassLodName{ get;private set;}


        public override void Render()
        {
            Render(GetParent().TerrainShader);
        }

        public override void Render(AShader shader)
        {

            if (isLeaf)
            {

                shader.SetUniform("index", Index);

                shader.SetUniformf("gap", Gap);

                shader.SetUniformi("lod", Lod);

                shader.SetUniform("location", Location);

                shader.SetUniform("LocTransMatrix", GetNodeLoclTrans().GetTransformMat());

                GL.DrawArrays(PrimitiveType.Patches, 0, GetParent().NodePachModel.VerticesNumber);
            }
            else
            {

                if (IsRenderable)
                {
                    childsNodes[0].Render(shader);
                    childsNodes[1].Render(shader);
                    childsNodes[2].Render(shader);
                    childsNodes[3].Render(shader);
                }


            }
        }

        public override void Update()
        {
         
            IsRenderable = FrustumCulled(CameraInstance.Get())?true: false;

           if (CameraInstance.Get().GetWorldPos().Y > config.ScaleY)
            {
                worldPosition.Y = config.ScaleY;
            }
            else
            {
                worldPosition.Y = CameraInstance.Get().GetWorldPos().Y;
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
