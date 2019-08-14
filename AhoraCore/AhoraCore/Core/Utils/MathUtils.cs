using OpenTK;

namespace AhoraCore.Core.Utils
{
    public static  class MathUtils
    {
        public static float[] ToArray(Vector2 v)
        {
            return new float[2] { v.X, v.Y };
        }

        public static float[] ToArray(Vector3 v)
        {
            return new float[3] { v.X, v.Y, v.Z };
        }

        public static float[] ToArray(Vector4 v)
        {
            return new float[4] { v.X, v.Y, v.Z, v.W };
        }

        public static float[] ToArray(Matrix3 m)
        {
            return new float[9] { m.M11, m.M12, m.M13,
                                  m.M21, m.M22, m.M23,
                                  m.M31, m.M32, m.M33};
        }

        //public static float[] ToArray(Matrix4 m)
        //{
        //    return new float[16] { m.M11, m.M12, m.M13, m.M14,
        //                           m.M21, m.M22, m.M23, m.M24,
        //                           m.M31, m.M32, m.M33, m.M34,
        //                           m.M41, m.M42, m.M43, m.M44};
        //}
        public static float[] ToArray(Matrix4 m)
        {
            return new float[16] { m.M11, m.M21, m.M31, m.M41,
                                   m.M12, m.M22, m.M32, m.M42,
                                   m.M13, m.M23, m.M33, m.M43,
                                   m.M14, m.M24, m.M34, m.M44};
        }
    }
}
