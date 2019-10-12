using AhoraCore.Core.CES;
using OpenTK.Graphics.OpenGL;
namespace AhoraCore.Core.Rendering
{
    public class GeometryRenderPass:RenderPass
    {
        public GeometryRenderPass(RederPipeline parent, int w, int h):base(parent, w, h)
        {
            
        }

        public override void Create()
        {
            AddTextureAttacment("PositionBuffer");
            AddTextureAttacment("NormalBuffer");
            AddTextureAttacment("ColorBuffer");
            AddTextureAttacment("FresnelBuffer");
        }

        public override void DisableSettings()
        {
        }

        public override void DrawPass()
        {
            EnableSettings();
        /*        while (GameEntityStorrage.Entities.HasElementsInRenderQue())
                {
                    GameEntityStorrage.Entities.Render(GameEntityStorrage.Entities.GetNextRenderID());
                } */

            GameEntityStorrage.Entities.Render(GameEntityStorrage.Entities.RootID);
        }

        public override void EnableSettings()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
