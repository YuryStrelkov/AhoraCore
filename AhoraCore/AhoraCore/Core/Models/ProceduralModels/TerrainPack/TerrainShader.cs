using AhoraCore.Core.Shaders;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.CES.ICES;

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
            
            AddUniform("grassDiff");
            AddUniform("grassNormal");
            AddUniform("grassDisp");
            AddUniform("grassSpec");


            AddUniform("groundDiff");
            AddUniform("groundNormal");
            AddUniform("groundDisp");
            AddUniform("groundSpec");

            AddUniform("rockDiff");
            AddUniform("rockNormal");
            AddUniform("rockDisp");
            AddUniform("rockSpec");
        }
    }
}
