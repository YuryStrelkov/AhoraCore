using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.CES.ICES
{
    public interface IComponent:ITransformable,IBehavoir
    {
        void SetParent(IComponent parent);

        IComponent GetParent();

        T GetParent<T>()where T: IComponent;

        void SetParent<T>(T parent) where T : IBehavoir;
    }
}
