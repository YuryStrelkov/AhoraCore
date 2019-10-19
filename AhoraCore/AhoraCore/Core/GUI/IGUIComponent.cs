using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.GUI
{
    public interface IGUIComponent
    {
        bool HasFocus();

        void OnFocus();

        void OnClick();

        void OnRelease();

    }
}
