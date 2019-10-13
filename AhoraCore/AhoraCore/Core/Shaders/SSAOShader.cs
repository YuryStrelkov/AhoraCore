using AhoraCore.Core.CES.ICES;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Shaders
{
    public class SSAOShader : AShader
    {

        public SSAOShader() : base()
        {
            LoadShaderFromstring(Properties.Resources.SSAOPassVS, ShaderType.VertexShader);
            LoadShaderFromstring(Properties.Resources.SSAOPassFS, ShaderType.FragmentShader);

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
            AddAttribyte("aPosition_");
            AddAttribyte("aTexCoord_");
        }

        protected override void BindUniforms()
        {
            AddUniform("gPosition");
            AddUniform("gNormal");
            AddUniform("noizeMap");
            AddUniform("screenWidth");
            AddUniform("screenHeight");
            AddUniformBlock("SSAOBuffer");
            AddUniformBlock("CameraData");
        }
    }
}
