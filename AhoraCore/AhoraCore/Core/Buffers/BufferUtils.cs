
using OpenTK;
using System.Collections.Generic;

namespace AhoraCore.Core.Buffers
{
    public static class BufferUtils
    {
        public static float[] Matrix2Array(Matrix4 Matrix)
        {///only model matrices;
            return new float[] {
                                    Matrix.M11,
                                    Matrix.M12,
                                    Matrix.M13,
                                    Matrix.M14,
                                    Matrix.M21,
                                    Matrix.M22,
                                    Matrix.M23,
                                    Matrix.M24,
                                    Matrix.M31,
                                    Matrix.M32,
                                    Matrix.M33,
                                    Matrix.M34,
                                    Matrix.M41,
                                    Matrix.M42,
                                    Matrix.M43,
                                    Matrix.M44 };
        }

        public static double[] Matrix2DoubleArray(Matrix4 Matrix)
        {///only model matrices;
            return new double[] {
                                    Matrix.M11,
                                    Matrix.M12,
                                    Matrix.M13,
                                    Matrix.M14,
                                    Matrix.M21,
                                    Matrix.M22,
                                    Matrix.M23,
                                    Matrix.M24,
                                    Matrix.M31,
                                    Matrix.M32,
                                    Matrix.M33,
                                    Matrix.M34,
                                    Matrix.M41,
                                    Matrix.M42,
                                    Matrix.M43,
                                    Matrix.M44 };
        }
        public static float[] CreateFlippedBuffer(Matrix4 value)
        {
            float[] buffer = new float[16];

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    buffer[i * 4 + j] = value[i, j];
            return buffer;
        }

        public static string[] RemoveEmptyStrings(string[] data)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < data.Length; i++)
                if (!data[i].Equals(""))
                    result.Add(data[i]);

            string[] res = result.ToArray();

            return res;
        }

    }
}
