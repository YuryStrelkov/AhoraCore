using OpenTK;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Context;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Shaders
{
    public class AtmosphereShader : AShader
    {
        public AtmosphereShader(): base()
        {
            if (MainContext.GetRenderMethod()==Rendering.RenderMethods.Forward)
            {
                LoadShaderFromstring(Properties.Resources.SkyDomeVS, ShaderType.VertexShader);
                LoadShaderFromstring(Properties.Resources.SkyDomeFS, ShaderType.FragmentShader);
            }

            if (MainContext.GetRenderMethod() == Rendering.RenderMethods.Deffered)
            {
                LoadShaderFromstring(Properties.Resources.DefferedSkyDomeVS, ShaderType.VertexShader);
                LoadShaderFromstring(Properties.Resources.DefferedSkyDomeFS, ShaderType.FragmentShader);
            }

            Link();
            Validate();
            BindAttributes();
            BindUniforms();
        }

        public override void UpdateUniforms()
        {
            SetUniform("DomeColor", new Vector4(0.18f, 0.27f, 0.47f, 1f));
        }

        public override void UpdateUniforms(IGameEntity e)
        {
            SetUniform("DomeColor",new Vector4(0.18f, 0.27f, 0.47f, 1f));
        }

        protected override void BindAttributes()
        {
            AddAttribyte("p_position");
            AddAttribyte("p_normal");
            AddAttribyte("p_texcoord");
        }

        protected override void BindUniforms()
        {

            AddUniformBlock("CameraData");
            AddUniformBlock("MaterialData");
            AddUniformBlock("TransformData");

            AddUniform("DomeColor");
            AddUniform("diffuseMap");
            AddUniform("normalMap");
            AddUniform("specularMap");
            AddUniform("heightMap");
            AddUniform("reflectGlossMap");
            AddUniform("transparencyMap");
        }
    }
}
