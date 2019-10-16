using AhoraCore.Core.CES.ICES;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.Context;

namespace AhoraCore.Core.Shaders
{
    public class MaterialShader : AShader
    {
        public MaterialShader() : base()
        {
            if (MainContext.GetRenderMethod() == Rendering.RenderMethods.Forward)
            {
                LoadShaderFromstring(Properties.Resources.MaterialVS, ShaderType.VertexShader);
                LoadShaderFromstring(Properties.Resources.MaterialFS, ShaderType.FragmentShader);
            }

            if (MainContext.GetRenderMethod() == Rendering.RenderMethods.Deffered)
            {
                LoadShaderFromstring(Properties.Resources.DefferedMaterialVS, ShaderType.VertexShader);
                LoadShaderFromstring(Properties.Resources.DefferedMaterialFS, ShaderType.FragmentShader);
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
            AddAttribyte("Position");
            AddAttribyte("TexCoord");
            AddAttribyte("Normal");
            AddAttribyte("Tangent");
            AddAttribyte("biTangent");
        }

        protected override void BindUniforms()
        {

            AddUniformBlock("CameraData");
            AddUniformBlock("MaterialData");
            AddUniformBlock("TransformData");

            AddUniform("diffuseMap");
            AddUniform("normalMap");
            AddUniform("specularMap");
            AddUniform("heightMap");
            AddUniform("reflectGlossMap");
            AddUniform("transparencyMap");
        }
    }
}
