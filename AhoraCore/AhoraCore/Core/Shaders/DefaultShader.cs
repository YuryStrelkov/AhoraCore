using AhoraCore.Core.CES;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Materials;
using OpenTK;

namespace AhoraCore.Core.Shaders
{
    class DefaultShader : AShader
    {
        public DefaultShader() 
            : base(Properties.Resources.VSdefault, Properties.Resources.FSdefault, false)
        {
 
        }
        
         public override void UpdateUniforms()
        {
            //SetUniform("viewMatrix", Cameras.CameraInstance.Get().ViewMatrix);
            //SetUniform("transformationMatrix", Matrix4.Identity);
            //SetUniform("projectionMatrix", Cameras.CameraInstance.Get().PespectiveMatrix);
        }

        protected override void BindAttributes()
        {
            AddAttribyte("p_position");
            AddAttribyte("p_normal");
            AddAttribyte("p_texcoord");
        }


        public override void UpdateUniforms(IGameEntity e)
        {
       //     SetUniform("viewMatrix", Cameras.CameraInstance.Get().ViewMatrix);
       //     SetUniform("transformationMatrix", e.GetWorldTransMat());
       //     SetUniform("projectionMatrix", Cameras.CameraInstance.Get().PespectiveMatrix);
        }
        
        protected override void BindUniforms()
        {
            
            //AddUniform("transformationMatrix");
            //AddUniform("projectionMatrix");
            //AddUniform("viewMatrix");

          ///  AddUniform("defTexture");
            AddUniform("diffuseMap");
            AddUniform("normalMap");
            AddUniform("specularMap");
            AddUniform("heightMap");
            AddUniform("reflectGlossMap");
            AddUniform("transparencyMap");

           /* AddUniform("albedoColor");
            AddUniform("ambientColor");
            AddUniform("reflectionColor");

            AddUniform("reflectivity");
            AddUniform("metallness");
            AddUniform("roughness");
            AddUniform("transparency");

            for (int i = 0; i < Material.MAX_TEXTURE_CHANNELS_NUMBER; i++)
            {
                AddUniform("channel[" + i + "].tileUV");
                AddUniform("channel[" + i + "].offsetUV");
                AddUniform("channel[" + i + "].multRGBA");
            }
            */
     


        }
    }
}
