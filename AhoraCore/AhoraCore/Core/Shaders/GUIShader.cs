
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Context;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Shaders
{
    public class GUIShader : AShader
    {
        public GUIShader():base()
        {
            LoadShaderFromstring(Properties.Resources.GUI_VS, ShaderType.VertexShader);
            LoadShaderFromstring(Properties.Resources.GUI_GS, ShaderType.GeometryShader);
            LoadShaderFromstring(Properties.Resources.GUI_FS, ShaderType.FragmentShader);

            Link();
            Validate();
            BindAttributes();
            BindUniforms();
        }

        public override void UpdateUniforms()
        {
            SetUniformf("Aspect", MainContext.ScreenAspectRatio);
        }

        public override void UpdateUniforms(IGameEntity e)
        {
        }

        protected override void BindAttributes()
        {
            AddAttribyte("Position");
        }

        protected override void BindUniforms()
        {
            AddUniformBlock("TransformData");
          //  AddUniformBlock("CharactersData");
           // AddUniformBlock("Characters");
            AddUniform("Aspect");
            AddUniform("texture1");
            AddUniform("texture2");
   ///         AddUniform("TextSamplerTex");
        }
    }
}
