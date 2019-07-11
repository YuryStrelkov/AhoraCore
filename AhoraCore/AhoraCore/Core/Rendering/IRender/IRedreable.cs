using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraProject.Ahora.Core.IRender
{
   public interface IRedreable<T>
    {
       void BeforeRender();
       void Render();
       void RenderIteam(T iteamID);
       void PostRender();
    }
}
