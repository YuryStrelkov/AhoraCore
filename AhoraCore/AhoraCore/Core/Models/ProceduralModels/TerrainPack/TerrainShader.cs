using AhoraCore.Core.Shaders;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Materials.AbstractMaterial;

namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
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

            AddUniform(TextureShaderChannels.GroundDiffuse);
            AddUniform(TextureShaderChannels.GroundNormal);
            AddUniform(TextureShaderChannels.GroundDisplacement);
            AddUniform(TextureShaderChannels.GroundReflectGloss);

            AddUniform(TextureShaderChannels.RockDiffuse);
            AddUniform(TextureShaderChannels.RockNormal);
            AddUniform(TextureShaderChannels.RockDisplacement);
            AddUniform(TextureShaderChannels.RockReflectGloss);


            AddUniform(TextureShaderChannels.GrassDiffuse);
            AddUniform(TextureShaderChannels.GrassNormal);
            AddUniform(TextureShaderChannels.GrassDisplacement);
            AddUniform(TextureShaderChannels.GrassReflectGloss);

        }
    }
}
