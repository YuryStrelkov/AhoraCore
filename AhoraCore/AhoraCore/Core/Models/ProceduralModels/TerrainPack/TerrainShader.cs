using AhoraCore.Core.Shaders;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Materials.AbstractMaterial;
using AhoraCore.Core.Context;

namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
    sealed public class TerrainShader : AShader
    {
        public TerrainShader() : base()
        {

            if (MainContext.GetRenderMethod() == Rendering.RenderMethods.Forward)
            {
                LoadShaderFromstring(Properties.Resources.TerrainVS, ShaderType.VertexShader);
                LoadShaderFromstring(Properties.Resources.TerrainFS, ShaderType.FragmentShader);
                LoadShaderFromstring(Properties.Resources.TerrainTC, ShaderType.TessControlShader);
                LoadShaderFromstring(Properties.Resources.TerrainGS, ShaderType.GeometryShader);
                LoadShaderFromstring(Properties.Resources.TerrainTE, ShaderType.TessEvaluationShader);
            }

            if (MainContext.GetRenderMethod() == Rendering.RenderMethods.Deffered)
            {
                LoadShaderFromstring(Properties.Resources.DefferedTerrainVS, ShaderType.VertexShader);
                LoadShaderFromstring(Properties.Resources.DefferedTerrainFS, ShaderType.FragmentShader);
                LoadShaderFromstring(Properties.Resources.DefferedTerrainTC, ShaderType.TessControlShader);
                LoadShaderFromstring(Properties.Resources.DefferedTerrainGS, ShaderType.GeometryShader);
                LoadShaderFromstring(Properties.Resources.DefferedTerrainTE, ShaderType.TessEvaluationShader);
            }
            Link();
            Validate();
            BindAttributes();
            BindUniforms();

        }

        public override void UpdateUniforms()
        {
            SetUniform("cameraPosition", Cameras.CameraInstance.Get().GetWorldPos());
        }

        public override void UpdateUniforms(IGameEntity e)
        {
        }

        protected override void BindAttributes()
        {
            AddAttribyte("p_position"); 
        }

        protected override void BindUniforms()
        {

            AddUniformBlock("CameraData");
            AddUniformBlock("MaterialData");
            AddUniformBlock("TransformData");
            AddUniformBlock("TerrainSettings");

            AddUniform("LocTransMatrix");

            AddUniform("index");//gap location ScaleY  index lod_morph_area cameraPosition

            AddUniform("gap");

            AddUniform("lod");

            AddUniform("location");

            AddUniform("cameraPosition");

            AddUniform("heightMap");

            AddUniform("normalMap");

            AddUniform("blendMap");

            AddUniform("materials[0].diffuseMap");
            AddUniform("materials[0].normalMap");
            AddUniform("materials[0].specularMap");
            AddUniform("materials[0].heightMap");

            AddUniform("materials[1].diffuseMap");
            AddUniform("materials[1].normalMap");
            AddUniform("materials[1].specularMap");
            AddUniform("materials[1].heightMap");


            AddUniform("materials[2].diffuseMap");
            AddUniform("materials[2].normalMap");
            AddUniform("materials[2].specularMap");
            AddUniform("materials[2].heightMap");

        }
    }
}
