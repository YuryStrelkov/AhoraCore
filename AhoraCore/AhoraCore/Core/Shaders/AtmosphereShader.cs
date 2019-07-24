using System;
using AhoraCore.Core.CES;
using OpenTK;

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
            SetUniform("viewMatrix", Cameras.CameraInstance.Get().ViewMatrix);
            SetUniform("transformationMatrix", Matrix4.Identity);
            SetUniform("projectionMatrix", Cameras.CameraInstance.Get().PespectiveMatrix);
        }

        public override void UpdateUniforms(GameEntity e)
        {
            SetUniform("viewMatrix", Cameras.CameraInstance.Get().ViewMatrix);
            SetUniform("transformationMatrix", e.GetWorldTransform().GetTransformMat());
            SetUniform("projectionMatrix", Cameras.CameraInstance.Get().PespectiveMatrix);
        }

        protected override void BindAttributes()
        {
            AddAttribyte("p_position");
            AddAttribyte("p_normal");
            AddAttribyte("p_texcoord");
        }

        protected override void BindUniforms()
        {
            AddUniform("transformationMatrix");
            AddUniform("projectionMatrix");
            AddUniform("viewMatrix");

      ///      AddUniform("defTexture");
            AddUniform("diffuseMap");
            AddUniform("normalMap");
            AddUniform("specularMap");
            AddUniform("heightMap");
            AddUniform("reflectGlossMap");
            AddUniform("transparencyMap");
        }
    }
}
