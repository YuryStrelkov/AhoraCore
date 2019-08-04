using AhoraCore.Core.Shaders;
using AhoraCore.Core.CES.ICES;
using OpenTK.Graphics.OpenGL;

namespace AhoraCore.Core.Materials.GpuGpu
{
    public class NormalMapRendererShader : AShader
    {
        static NormalMapRendererShader instance;

        public static NormalMapRendererShader getInstance()
        {
            if (instance == null)
            {
                instance = new NormalMapRendererShader();
            }
            return instance;
        }

        private NormalMapRendererShader() : base()
        {
                LoadShaderFromstring(Properties.Resources.NormalMapRenderer, ShaderType.ComputeShader);
                Link();
                Validate();
                BindAttributes();
                BindUniforms();
        }


        public  void UpdateUniforms(Texture h_m, int n, float strength)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            h_m.Bind();
            SetUniformi("displacementmap", 0);
            SetUniformi("N", n);
            SetUniformf("normalStrength", strength);

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
            AddUniform("displacementmap");
            AddUniform("N");
            AddUniform("normalStrength");

        }
    }
}
