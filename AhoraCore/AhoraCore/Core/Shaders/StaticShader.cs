
using System;

namespace AhoraCore.Core.Shaders
{

    public class StaticShader : AShader
    {
        private static string VertexShaderCode = "#version 150 \n in vec3 position; \n out vec3 colour; \n void main(void) \n { \n gl_Position = vec4(position, 1.0);\n colour = vec3(position.x + 0.5, 0.0, position.y + 0.5);\n}";
        private static string FragmentShaderCode = "#version 150 \n in vec3 colour;\n out vec4 out_Color; \n void main(void) \n { \n out_Color = vec4(colour, 1.0);\n }";


        public StaticShader() : base(VertexShaderCode, FragmentShaderCode, false)
        {
        }

        public override void BindAttribytes()
        {
            BindAttributeLocation(0,"position");
        }

        public override void UpdateUniforms()
        {

        }
    }
}
