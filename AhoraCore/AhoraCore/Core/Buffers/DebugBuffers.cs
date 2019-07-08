using AhoraCore.Core.Buffers.SpecificBuffers;
using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.Buffers
{
   public static class DebugBuffers
    {
        public static float[] getBufferDataVBO(ArrayBuffer buffer)
        {
            float[] b_data = new float[buffer.VBO.Fillnes];
            buffer.BindBuffer();
            ///buffer.VBO.BindBuffer();
            GL.GetBufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, buffer.VBO.Fillnes * sizeof(float), b_data);
            return b_data;

        }

        public static void displayBufferDataVBO(ArrayBuffer buffer)
        {
            float[] data = getBufferDataVBO(buffer);
            foreach (float e in data) Console.WriteLine(e + " ");
        }

        public static int[] getBufferDataIBO(ArrayBuffer buffer)
        {
            int[] b_data = new int[buffer.IBO.Fillnes];
            buffer.BindBuffer();
            buffer.IBO.BindBuffer();
            GL.GetBufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)0, buffer.IBO.Fillnes * sizeof(int), b_data);
            return b_data;

        }

        public static void displayBufferDataIBO(ArrayBuffer buffer)
        {
            int[] data = getBufferDataIBO(buffer);
            foreach (int e in data) Console.WriteLine(e + " ");
        }




        public static float[] getBufferDataVBO(VerticesBuffer buffer)
        {
            float[] b_data = new float[buffer.Fillnes];
            buffer.BindBuffer();
            ///buffer.VBO.BindBuffer();
            GL.GetBufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, buffer.Fillnes * sizeof(float), b_data);
            return b_data;

        }

        public static void displayBufferDataVBO(VerticesBuffer buffer)
        {
            float[] data = getBufferDataVBO(buffer);
            foreach (float e in data) Console.WriteLine(e + " ");
        }

        public static int[] getBufferDataIBO(IndecesBuffer buffer)
        {
            int[] b_data = new int[buffer.Fillnes];
            buffer.BindBuffer();
            GL.GetBufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)0, buffer.Fillnes * sizeof(int), b_data);
            return b_data;

        }

        public static void displayBufferDataIBO(IndecesBuffer buffer)
        {
            int[] data = getBufferDataIBO(buffer);
            foreach (int e in data) Console.WriteLine(e + " ");
        }

    }
}
