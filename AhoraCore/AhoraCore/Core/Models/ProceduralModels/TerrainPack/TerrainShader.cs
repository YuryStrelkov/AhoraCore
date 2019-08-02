using AhoraCore.Core.Shaders;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.CES.ICES;

namespace AhoraCore.Core.Models.ProceduralModels.TerranPack
{
    public class TerrainShader : AShader
    {
        public TerrainShader() : base()
        {
            LoadShaderFromstring(Properties.Resources.TerrainVS, ShaderType.VertexShader);
            LoadShaderFromstring(Properties.Resources.TerrainFS, ShaderType.FragmentShader);
            LoadShaderFromstring(Properties.Resources.TerrainTC, ShaderType.TessControlShader);
            LoadShaderFromstring(Properties.Resources.TerrainGS, ShaderType.GeometryShader);
            LoadShaderFromstring(Properties.Resources.TerrainTE, ShaderType.TessEvaluationShader);

            Link();
            Validate();
            BindAttributes();
            BindUniforms();

        }

        public override void UpdateUniforms()
        {
          /*  SetUniform("viewMatrix", Cameras.CameraInstance.Get().ViewMatrix);
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
//            AddAttribyte("p_normal");
  //          AddAttribyte("p_texcoord");
        }

        protected override void BindUniforms()
        {
            AddUniform("LocTransMatrix");
            AddUniform("WorldTransMatrix");
            AddUniform("projectionMatrix");
            AddUniform("viewMatrix");


            AddUniform("diffuseMap");
            AddUniform("normalMap");
            AddUniform("specularMap");
            AddUniform("heightMap");
            AddUniform("reflectGlossMap");
            AddUniform("transparencyMap");
        }
    }
}
