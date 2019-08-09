using AhoraCore.Core.Shaders;
using AhoraCore.Core.CES.ICES;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
    public class TerrainGrassShader : AShader
    {
        public TerrainGrassShader() : base()
        {
            LoadShaderFromstring(Properties.Resources.GrassVS, ShaderType.VertexShader);
            LoadShaderFromstring(Properties.Resources.GrassFS, ShaderType.FragmentShader);
           
            Link();
            Validate();
            BindAttributes();
            BindUniforms();
        }

        public override void UpdateUniforms()
        {
            /*SetUniform("viewMatrix", Cameras.CameraInstance.Get().ViewMatrix);
              SetUniform("projectionMatrix", Cameras.CameraInstance.Get().PespectiveMatrix);
              SetUniform("WorldTransMatrix", Matrix4.Identity);
              SetUniform("LocTransMatrix", Matrix4.Identity);*/
        }

        public override void UpdateUniforms(IGameEntity e)
        {
            SetUniform("viewMatrix", Cameras.CameraInstance.Get().ViewMatrix);
            SetUniform("projectionMatrix", Cameras.CameraInstance.Get().PespectiveMatrix);
            SetUniform("WorldTransMatrix", e.GetWorldTransform().GetTransformMat());
            SetUniform("LocTransMatrix", e.GetLocalTransform().GetTransformMat());
        }

        protected override void BindAttributes()
        {
            AddAttribyte("p_position");
            AddAttribyte("p_normal");
            AddAttribyte("p_uv");

        }

        protected override void BindUniforms()
        {
            AddUniform("LocTransMatrix");

            AddUniform("WorldTransMatrix");

            AddUniform("projectionMatrix");

            AddUniform("viewMatrix");

            AddUniform("ScaleY");

            AddUniform("ScaleXZ");

            AddUniform("lod");

            AddUniform("gap");

            AddUniform("diffuseMap");

            AddUniform("normalMap");

            AddUniform("specularMap");

            AddUniform("heightMap");

            AddUniform("reflectGlossMap");

            AddUniform("transparencyMap");
        }

    }
}
