using AhoraCore.Core.Shaders;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.CES.ICES;

namespace AhoraCore.Core.Materials.GpuGpu
{
    class SplatMapRendererShader : AShader
    {
        static SplatMapRendererShader instance;

        public static SplatMapRendererShader getInstance()
        {
            if (instance == null)
            {
                instance = new SplatMapRendererShader();
            }
            return instance;
        }

        private SplatMapRendererShader() : base()
        {
            LoadShaderFromstring(Properties.Resources.SplatMapRenderer, ShaderType.ComputeShader);
            Link();
            Validate();
            BindAttributes();
            BindUniforms();
        }


        public void UpdateUniforms(Texture normalMap, int n)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            normalMap.Bind();
            SetUniformi("normalMap", 0);
            SetUniformi("N", n);
        }


        public override void UpdateUniforms()
        {

        }

        public override void UpdateUniforms(IGameEntity e)
        {

        }

        protected override void BindAttributes()
        {

        }

        protected override void BindUniforms()
        {
            AddUniform("normalMap");
            AddUniform("N");
        }
    }
}
