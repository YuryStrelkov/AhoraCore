using AhoraCore.Core.Buffers.SpecificBuffers;
using System;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.Buffers.IBuffres;

namespace AhoraCore.Core.Buffers
{
   public static class DebugBuffers
    {

        public static FramebufferErrorCode DisplayFrameBufferErrors(string bufferName, FramebufferErrorCode e)
        {
            ///FramebufferErrorCode e = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            switch (e)
            {
                case FramebufferErrorCode.FramebufferComplete:
                    {
                        Console.WriteLine(bufferName + " : The framebuffer is complete and valid for rendering.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferIncompleteAttachment:
                    {
                        Console.WriteLine(bufferName + " : One or more attachment points are not framebuffer attachment complete. This could mean there’s no texture attached or the format isn’t renderable. For color textures this means the base format must be RGB or RGBA and for depth textures it must be a DEPTH_COMPONENT format. Other causes of this error are that the width or height is zero or the z-offset is out of range in case of render to volume.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferIncompleteMissingAttachmentExt:
                    {
                        Console.WriteLine(bufferName + " : There are no attachments.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferIncompleteDimensionsExt:
                    {
                        Console.WriteLine(bufferName + " : Attachments are of different size. All attachments must have the same width and height.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferIncompleteFormatsExt:
                    {
                        Console.WriteLine(bufferName + " : The color attachments have different format. All color attachments must have the same format.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferIncompleteDrawBuffer:
                    {
                        Console.WriteLine(bufferName + " : An attachment point referenced by GL.DrawBuffers() doesn’t have an attachment.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferIncompleteReadBuffer:
                    {
                        Console.WriteLine(bufferName + " : The attachment point referenced by GL.ReadBuffers() doesn’t have an attachment.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferUnsupported:
                    {
                        Console.WriteLine(bufferName + " : This particular FBO configuration is not supported by the implementation.");
                        break;
                    }
                case (FramebufferErrorCode)All.FramebufferIncompleteLayerTargetsExt:
                    {
                        Console.WriteLine(bufferName + " : Framebuffer Incomplete Layer Targets.");
                        break;
                    }
                case (FramebufferErrorCode)All.FramebufferIncompleteLayerCount:
                    {
                        Console.WriteLine(bufferName + " : Framebuffer Incomplete Layer Count.");
                        break;
                    }
                default:
                    {
                        Console.WriteLine(bufferName + " : Status unknown. (yes, this is really bad.)");
                        break;
                    }
            }
            return e;
        }





        public static float[] getBufferData(ArrayBuffer buffer)
        {
            float[] b_data = new float[buffer.VBO.Fillnes];
            buffer.Bind();
            ///buffer.VBO.BindBuffer();
            GL.GetBufferSubData(buffer.BindingTarget, (IntPtr)0, buffer.VBO.Fillnes * sizeof(float), b_data);
            return b_data;

        }


        public static void displayBufferData(ABuffer Buffer)
        {
            float[] b_data = new float[Buffer.Fillnes];
            Buffer.Bind();
            GL.GetBufferSubData(Buffer.BindingTarget, (IntPtr)0, Buffer.Fillnes * sizeof(float), b_data);
            foreach (float e in b_data) Console.WriteLine(e + " ");
        }


        public static void displayBufferData(ArrayBuffer buffer)
        {
            float[] data = getBufferData(buffer);
            foreach (float e in data) Console.WriteLine(e + " ");
        }

        public static int[] getBufferDataIBO(ArrayBuffer buffer)
        {
            int[] b_data = new int[buffer.IBO.Fillnes];
            buffer.Bind();
            buffer.IBO.Bind();
            GL.GetBufferSubData(buffer.BindingTarget, (IntPtr)0, buffer.IBO.Fillnes * sizeof(int), b_data);
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
            buffer.Bind();
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
            buffer.Bind();
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
