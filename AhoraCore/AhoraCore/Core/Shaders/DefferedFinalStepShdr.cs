using AhoraCore.Core.CES.ICES;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Shaders
{
    public class DefferedFinalStepShdr : AShader
    {
        public DefferedFinalStepShdr() : base()
        {
            LoadShaderFromstring(Properties.Resources.DefferedFinalPassVS, ShaderType.VertexShader);
            LoadShaderFromstring(Properties.Resources.DefferedFinalPassFS, ShaderType.FragmentShader);

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
            AddAttribyte("aPosition");
            AddAttribyte("aTexCoord");
        }

        protected override void BindUniforms()
        {

            AddUniformBlock("CameraData");

          ///  AddUniformBlock("LightsData");

            AddUniform("position");
            AddUniform("scale");
            AddUniform("Positions");
            AddUniform("Normals");
            AddUniform("Colors");
            AddUniform("Fresnels");
            AddUniform("SSAO");
        }
    }
}
