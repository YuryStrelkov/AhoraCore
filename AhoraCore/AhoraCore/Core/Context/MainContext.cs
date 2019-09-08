using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.CES;
using AhoraCore.Core.DataManaging;
using AhoraProject.Ahora.Core.Display;


namespace AhoraCore.Core.Context
{
    public static class MainContext
    {
        private static DisplayDevice FrameDisplay;
        
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

            TextureStorrage.Initilaze();

            MaterialStorrage.Initilaze();

            ShaderStorrage.Initilaze();

            GeometryStorageManager.Initialize();

            GameEntityStorrage.Initialize();
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
