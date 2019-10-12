namespace AhoraCore.Core.Rendering
{
    public enum RenderMethods
    {
        Forward=0,
        Deffered=1
    }

    public interface RederPipeline
    {

        void BeforeRender();

        void Render();

        void AfterRender();

        /* Dictionary<string, RenderPass> renderPasses;

         List<string> renderOrder;

         public void Render()
         {

         }*/
    }
}
