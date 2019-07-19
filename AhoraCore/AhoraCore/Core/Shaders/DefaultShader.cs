using OpenTK;

namespace AhoraCore.Core.Shaders
{
    class DefaultShader : AShader
    {
        public DefaultShader() 
            : base(Properties.Resources.VSdefault, Properties.Resources.FSdefault, false)
        {
            AddUniform("transformationMatrix");
            AddUniform("projectionMatrix");
            AddUniform("viewMatrix");
            BindAttribytes();
        }
        
        public override void BindAttribytes()
        {
         BindAttributeLocation(0, "p_position");
         BindAttributeLocation(1, "p_TexCoord");
         BindAttributeLocation(2, "p_normal");
        }

        public override void UpdateUniforms()
        {
            SetUniform("viewMatrix", Cameras.CameraInstance.Get().ViewMatrix);
            SetUniform("transformationMatrix", Matrix4.Identity);
            SetUniform("projectionMatrix", Cameras.CameraInstance.Get().PespectiveMatrix);
        }
    }
}
