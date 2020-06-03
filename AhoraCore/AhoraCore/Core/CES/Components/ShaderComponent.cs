using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Shaders;
using AhoraCore.Core.Buffers.DataStorraging;
using System;

namespace AhoraCore.Core.CES.Components
{
    public class ShaderComponent : AComponent<IGameEntity>
    {

        private string shaderID;

        public string ShaderID
        {
            get
            {
                return shaderID;
            }
            set
            {
                shaderID = value;
                Shader = ShaderStorrage.Sahaders.GetItem(shaderID);
            }
        }

        public AShader Shader{ get; private set;}

        public override void Clear()
        {
        }

        public override void Delete()
        {
        }

        public override void Disable()
        {
            Shader.Bind();
        }

        public override void Enable()
        {
            Shader.Unbind();
        }

        public override void Input()
        {
           
        }

        public override void Render()
        {
            Shader.Bind();
            Cameras.CameraInstance.Get().Bind(Shader);
        }

        public override void Update()
        {
        }

        public override void Render(AShader shader)
        {
            throw new NotImplementedException();
        }

        public ShaderComponent(string ShaderID) :base()
        {
            Component = "ShaderData";
            this.ShaderID = ShaderID;
            ComponentType = ComponentsTypes.ShaderComponent;
        }
    }
}
