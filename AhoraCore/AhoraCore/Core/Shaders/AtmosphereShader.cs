using OpenTK;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Utils;

namespace AhoraCore.Core.Shaders
{
    public class AtmosphereShader : AShader
    {
        public AtmosphereShader() 
            : base(Properties.Resources.SkyDomeVS, Properties.Resources.SkyDomeFS, false)
        {

            EnableBuffering();
            MarkBuffer(new string[]{/*"localTransform", "worldTransform",*/ "projectionMatrix", "viewMatrix" , "DomeColor" },new int[]{ 16, 16, 16, 16 ,4});
            ConfirmBuffer();///Создаёт один размеченный выше буфер для материала 


            UniformBuffer.UpdateBufferIteam("viewMatrix",       MathUtils.ToArray(Matrix4.Identity));
         //   UniformBuffer.UpdateBufferIteam("localTransform",   MathUtils.ToArray(Matrix4.Identity));
          //  UniformBuffer.UpdateBufferIteam("worldTransform",   MathUtils.ToArray(Matrix4.Identity));
            UniformBuffer.UpdateBufferIteam("projectionMatrix", MathUtils.ToArray(Matrix4.Identity));
            UniformBuffer.UpdateBufferIteam("DomeColor",        new float[4] { 0.18f, 0.27f, 0.47f ,1f});
        }





           public override void UpdateUniforms()
        { 
            UniformBuffer.UpdateBufferIteam("viewMatrix",      MathUtils.ToArray(Cameras.CameraInstance.Get().ViewMatrix));
          //  UniformBuffer.UpdateBufferIteam("localTransform",   MathUtils.ToArray(Matrix4.Identity));
           // UniformBuffer.UpdateBufferIteam("worldTransform",   MathUtils.ToArray(Matrix4.Identity));
            UniformBuffer.UpdateBufferIteam("projectionMatrix", MathUtils.ToArray(Cameras.CameraInstance.Get().PespectiveMatrix));
            //SetUniform("viewMatrix", Cameras.CameraInstance.Get().ViewMatrix);
            //SetUniform("transformationMatrix", Matrix4.Identity);
            //SetUniform("projectionMatrix", Cameras.CameraInstance.Get().PespectiveMatrix);
        }

        public override void UpdateUniforms(IGameEntity e)
        {

            UniformBuffer.UpdateBufferIteam("viewMatrix",       MathUtils.ToArray(Cameras.CameraInstance.Get().ViewMatrix));
       ///     UniformBuffer.UpdateBufferIteam("localTransform",   MathUtils.ToArray(Matrix4.Identity));
       //     UniformBuffer.UpdateBufferIteam("worldTransform",   MathUtils.ToArray(e.GetWorldTransMat()));
            UniformBuffer.UpdateBufferIteam("projectionMatrix", MathUtils.ToArray(Cameras.CameraInstance.Get().PespectiveMatrix));
            e.UpdateUniforms(this);
            //SetUniform("viewMatrix", Cameras.CameraInstance.Get().ViewMatrix);
            //SetUniform("transformationMatrix", e.GetWorldTransform().GetTransformMat());
            //SetUniform("projectionMatrix", Cameras.CameraInstance.Get().PespectiveMatrix);
        }

        protected override void BindAttributes()
        {
            AddAttribyte("p_position");
            AddAttribyte("p_normal");
            AddAttribyte("p_texcoord");
        }

        protected override void BindUniforms()
        {
            /*
            AddUniform("transformationMatrix");
            AddUniform("projectionMatrix");
            AddUniform("viewMatrix");
            */
      ///   AddUniform("defTexture");

            AddUniform("diffuseMap");
            AddUniform("normalMap");
            AddUniform("specularMap");
            AddUniform("heightMap");
            AddUniform("reflectGlossMap");
            AddUniform("transparencyMap");
        }
    }
}
