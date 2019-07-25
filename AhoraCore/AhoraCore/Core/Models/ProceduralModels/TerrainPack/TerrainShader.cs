using System;
using AhoraCore.Core.CES;
using AhoraCore.Core.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;


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
            SetUniform("viewMatrix", Cameras.CameraInstance.Get().ViewMatrix);
            SetUniform("WorldTransMatrix", Matrix4.Identity);
            SetUniform("LocTransMatrix", Matrix4.Identity);
            SetUniform("projectionMatrix", Cameras.CameraInstance.Get().PespectiveMatrix);
        }

        public override void UpdateUniforms(GameEntity e)
        {
            SetUniform("viewMatrix", Cameras.CameraInstance.Get().ViewMatrix);
            SetUniform("WorldTransMatrix", e.GetWorldTransform().GetTransformMat());
            SetUniform("LocTransMatrix", e.GetLocalTransform().GetTransformMat());
            SetUniform("projectionMatrix", Cameras.CameraInstance.Get().PespectiveMatrix);
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
