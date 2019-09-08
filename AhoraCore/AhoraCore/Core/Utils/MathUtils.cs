using OpenTK;
using System;
using System.Collections.Generic;

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


        public static float[] ToArray(Vector2[] vectors)
        {
            float[] array = new float[vectors.Length * 2];

            for (int i = 0; i < array.Length; i += 2)
            {
                array[i    ] = vectors[i / 2].X;
                array[i + 1] = vectors[i / 2].Y;
            }

            return array;
        }


        public static float[] ToArray(Vector3[] vectors)
        {
            float[] array = new float[vectors.Length * 4];
            for (int i = 0; i < array.Length; i += 4)
            {
                array[i    ] = vectors[i / 4].X;
                array[i + 1] = vectors[i / 4].Y;
                array[i + 2] = vectors[i / 4].Z;
                array[i + 3] = 0;
            }

            return array;
        }


        public static float[] ToArray(Vector4 [] vectors)
        {
            float [] array=new float [vectors.Length*4];
            for (int i = 0; i< array.Length;i+=4)
            {
                array[i    ] = vectors[i / 4].X;
                array[i + 1] = vectors[i / 4].Y;
                array[i + 2] = vectors[i / 4].Z;
                array[i + 3] = vectors[i / 4].W;
            }

            return array;
        }



        public static float[] ToArray(Matrix3 m)
        {
            return new float[9] { m.M11, m.M12, m.M13,
                                  m.M21, m.M22, m.M23,
                                  m.M31, m.M32, m.M33};
        }

        /// <summary>
        /// Row major packing of matrix in array of floats
        /// </summary>
        /// <param name="matrices"></param>
        /// <returns></returns>
        public static float[] ToArray(Matrix4 m)
        {
            return new float[16] { m.M11, m.M21, m.M31, m.M41,
                                   m.M12, m.M22, m.M32, m.M42,
                                   m.M13, m.M23, m.M33, m.M43,
                                   m.M14, m.M24, m.M34, m.M44};
        }

        /// <summary>
        /// Row major packing array of matrices in array of floats
        /// </summary>
        /// <param name="matrices"></param>
        /// <returns></returns>
        public static float[] ToArray(Matrix4 [] matrices)
        {
    
            float[] array = new float[16 * matrices.Length];
            int m_id = 0;
            for (int i=0;i<array.Length ;i+=16)
            {
                m_id = i / 16;
                array[i]     = matrices[m_id].M11;
                array[i + 1] = matrices[m_id].M21;
                array[i + 2] = matrices[m_id].M31;
                array[i + 3] = matrices[m_id].M41;

                array[i + 4] = matrices[m_id].M12;
                array[i + 5] = matrices[m_id].M22;
                array[i + 6] = matrices[m_id].M32;
                array[i + 7] = matrices[m_id].M42;

                array[i + 8]  = matrices[m_id].M13;
                array[i + 9]  = matrices[m_id].M23;
                array[i + 10] = matrices[m_id].M33;
                array[i + 11] = matrices[m_id].M43;

                array[i + 12] = matrices[m_id].M14;
                array[i + 13] = matrices[m_id].M24;
                array[i + 14] = matrices[m_id].M34;
                array[i + 15] = matrices[m_id].M44;
            }
            return array;
        }



        public static float ToRads(float angle)
        {
            return MathHelper.Pi * angle / 180.0f;
        }


        public static Vector4 NormalizePlane(Vector4 plane)
        {
            float mag;

            mag = (float)Math.Sqrt(plane.X * plane.X + plane.Y * plane.Y + plane.Z * plane.Z);
            plane.X = plane.X / mag;
            plane.Y = plane.Y / mag;
            plane.Z = plane.Z / mag;
            plane.W = plane.W / mag;

            return plane;
        }

        public static Vector3[] GenerateRandomKernel3D(int kernelSize)
        {

            Vector3[] kernel = new Vector3[kernelSize];

            Random rnd = new Random();

            for (int i = 0; i < kernelSize; i++)
            {
                kernel[i] = new Vector3((float)rnd.NextDouble() * 2 - 1,
                                      (float)rnd.NextDouble() * 2 - 1,
                                      (float)rnd.NextDouble());
                kernel[i].Normalize();

                float scale = (float)i / (float)kernelSize;

                scale = (float)Math.Min(Math.Max(0.01, scale * scale), 1.0);

                kernel[i] = kernel[i]*(scale)*(-1);
            }

            return kernel;
        }

        public static Vector4[] GenerateRandomKernel4D(int kernelSize)
        {

            Vector4[] kernel = new Vector4[kernelSize];

            Random rnd = new Random();

            for (int i = 0; i < kernelSize; i++)
            {
                kernel[i] = new Vector4((float)rnd.NextDouble() * 2 - 1,
                                        (float)rnd.NextDouble() * 2 - 1,
                                        (float)rnd.NextDouble(),
                                        0);
                kernel[i].Normalize();

                float scale = (float)i / (float)kernelSize;

                scale = (float)Math.Min(Math.Max(0.01, scale * scale), 1.0);

                kernel[i] = kernel[i]*(scale)*(-1);
            }

            return kernel;
        }

    }
}
