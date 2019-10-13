using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.CES;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Rendering;
using AhoraProject.Ahora.Core.Display;


namespace AhoraCore.Core.Context
{
    public static class MainContext
    {
        private static DisplayDevice FrameDisplay;

        private static RenderMethods renderPipeline;

        public static void UseForwardRenderer()
        {
            renderPipeline = RenderMethods.Forward;
            FrameDisplay.UseForward();
        }

        public static void UseDefferedRenderer()
        {
            renderPipeline = RenderMethods.Deffered;
            FrameDisplay.UseDeffered();
        }

        public static RenderMethods GetRenderMethod()
        {
            return renderPipeline;
        }

        private static float zfar  =15000, znear = 0.5f;

        public static float ZFar { get { return zfar; } set { if (value > znear) { zfar = value; } } }

        public static float ZNear { get { return znear; } set { if (value < zfar) { znear = value; } } }

        public static int ScreenWidth
        {
            get { return FrameDisplay.Width; }
        }

        public static int ScreenHeight
        {
            get { return FrameDisplay.Height; }
        }

        public static float ScreenAspectRatio
        {
            get { return (float)ScreenWidth / (float)ScreenHeight; }
        }

        public static void InitMainContext()
        {
         
            FrameDisplay = new DisplayDevice(1900, 1000);

            UseForwardRenderer();

            TextureStorrage.Initilaze();

            MaterialStorrage.Initilaze();

            ShaderStorrage.Initilaze();

            GeometryStorageManager.Initialize();

            GameEntityStorrage.Initialize();

            GeometryStorageManager.Data.AddGeometry(VericesAttribytes.V_POSITION | VericesAttribytes.V_UVS , "Canvas",
                                                                                new float[]{-1,  1, 0, 0, 1,
                                                                                            -1, -1, 0, 0, 0,
                                                                                             1,  1, 0, 1, 1,
                                                                                             1, -1, 0, 1, 0},
                                                                                  new int[] { 0, 1, 2, 2, 1, 3 });


        }

        public static void DisposeMainContext()
        {
            TextureStorrage.Textures.DeleteStorrage();

            MaterialStorrage.Materials.DeleteStorrage();

            ShaderStorrage.Sahaders.DeleteStorrage();

            GeometryStorageManager.Data.DeleteManager();

            GameEntityStorrage.Entities.DeleteStorrage();

            FrameDisplay.Dispose();
        }

        public static void RunContext()
        {
            FrameDisplay.Run();
        }
    }
}
