using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.Shaders
{
    class DefaultShader : AShader
    {
        public DefaultShader(string vshader, string fshader, bool fromFile = true) : base(vshader, fshader, fromFile)
        {
        }
        
        public override void BindAttribytes()
        {
            BindAttributeLocation(0, "position");
        }

        public override void UpdateUniforms()
        {
            
        }
    }
}
