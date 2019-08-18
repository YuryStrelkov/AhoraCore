using OpenTK;
using AhoraCore.Core.CES.ICES;

namespace AhoraCore.Core.Shaders
{
    public class AtmosphereShader : AShader
    {
        public AtmosphereShader() 
            : base(Properties.Resources.SkyDomeVS, Properties.Resources.SkyDomeFS, false)
        {
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
