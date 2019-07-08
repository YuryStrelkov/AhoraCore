namespace AhoraCore.Core.Buffers.IBuffers
{
        public static class VericesAttribytes
        {
        public static int V_POSITION = 0;
        public static int V_UVS = 1;
        public static int V_NORMAL = 2;
        public static int V_TANGENT = 4;
        public static int V_BITANGENT = 8;
        public static int V_BONES = 16;
        public static int V_BONES_WEIGHTS = 32;
        public static int V_COLOR_RGB = 64;
        public static int V_COLOR_RGBA = 128;
        }

        public interface IAttribyteable
        {
             void EnableAttribytes();
            void DisableAttribytes();

        }
}
