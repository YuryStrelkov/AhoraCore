using AhoraCore.Core.Shaders;
using AhoraCore.Core.CES.ICES;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.Context;

namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
    sealed public class TerrainGrassShader : AShader
    {
        public TerrainGrassShader() : base()
        {
            if (MainContext.GetRenderMethod() == Rendering.RenderMethods.Forward)
            {
                LoadShaderFromstring(Properties.Resources.TerrainVS, ShaderType.VertexShader);
                LoadShaderFromstring(Properties.Resources.GrassFS, ShaderType.FragmentShader);
                LoadShaderFromstring(Properties.Resources.GrassTC, ShaderType.TessControlShader);
                LoadShaderFromstring(Properties.Resources.GrassGS, ShaderType.GeometryShader);
                LoadShaderFromstring(Properties.Resources.GrassTE, ShaderType.TessEvaluationShader);
            }

            if (MainContext.GetRenderMethod() == Rendering.RenderMethods.Deffered)
            {
                LoadShaderFromstring(Properties.Resources.DefferedGrassVS, ShaderType.VertexShader);
                LoadShaderFromstring(Properties.Resources.DefferedGrassFS, ShaderType.FragmentShader);
                LoadShaderFromstring(Properties.Resources.DefferedGrassTC, ShaderType.TessControlShader);
                LoadShaderFromstring(Properties.Resources.DefferedGrassGS, ShaderType.GeometryShader);
                LoadShaderFromstring(Properties.Resources.DefferedGrassTE, ShaderType.TessEvaluationShader);
            }
          
            Link();
            Validate();
            BindAttributes();
            BindUniforms();
        }

        public override void UpdateUniforms()
        {
        }

        public override void UpdateUniforms(IGameEntity e)
        {
        }

        protected override void BindAttributes()
        {
            AddAttribyte("p_position");
            AddAttribyte("p_normal");
            AddAttribyte("p_uv");

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

            AddUniform("diffuseMap");

            AddUniform("normalMap");

            AddUniform("specularMap");

            AddUniform("heightMap");

            AddUniform("blendMap");

            AddUniform("reflectGlossMap");

            AddUniform("grassMap");

            AddUniform("transparencyMap");
        }

    }
}
