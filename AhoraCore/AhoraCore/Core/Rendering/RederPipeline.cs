using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.SpecificBuffers;
using AhoraCore.Core.DataManaging;
using System.Collections.Generic;

namespace AhoraCore.Core.Rendering
{
    public enum RenderMethods
    {
        Forward=0,
        Deffered=1
    }

    public abstract class RederPipeline
    {

        protected Dictionary<string, RenderPass> Passes;

        public RederPipeline()
        {
            Passes = new Dictionary<string, RenderPass>();
        }

        public abstract void BeforeRender();

        public abstract void Render();

        public abstract void AfterRender();

        public RenderPass getRenderPassBuffer(string passID)
        {
           return Passes[passID];
        }


    }
}
