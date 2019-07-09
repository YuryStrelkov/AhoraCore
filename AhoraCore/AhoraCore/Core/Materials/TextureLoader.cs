using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace AhoraCore.Core.Materials
{

     public static class TextureLoader
    {
        public static int NEAREST = (int)All.Nearest;

        public static int REPEAT = (int)All.Repeat;

        public static int LINEAR = (int)All.Linear;

        public static int LINEAR_MIP_MAP = (int)All.LinearMipmapLinear;

        public static int CLAMP_TO_EDGE = (int)All.ClampToEdge;

        public static int loadTextureFromImage(Bitmap image)
        {
            int texID;

            GL.GenTextures(1, out texID);

            GL.BindTexture(TextureTarget.Texture2D, texID);

            BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            image.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, ref LINEAR_MIP_MAP);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureLodBias, 0);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            return texID;
        }

        public static void setClamp2Edge(TextureTarget t_target)
        {
            if (t_target == TextureTarget.Texture2D)
            {
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, ref CLAMP_TO_EDGE);
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, ref CLAMP_TO_EDGE);
            }
            if (t_target == TextureTarget.TextureCubeMap)
            {
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            }
        }

        public static void setRepeat(TextureTarget t_target)
        {
            if (t_target == TextureTarget.Texture2D)
            {
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, ref REPEAT);
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, ref REPEAT);
            }
            if (t_target == TextureTarget.TextureCubeMap)
            {
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            }
        }

        public static void setMagMinFilter(TextureTarget t_target)
        {
            if (TextureTarget.Texture2D == t_target)
            {
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, ref LINEAR);
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, ref LINEAR_MIP_MAP);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }

            if (TextureTarget.TextureCubeMap == t_target)
            {
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.GenerateMipmap, (int)All.True);
            }
        }

        public static void setNearestFilter(TextureTarget t_target)
        {
            if (TextureTarget.Texture2D == t_target)
            {
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, ref NEAREST);
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, ref NEAREST);
            }

            if (TextureTarget.TextureCubeMap == t_target)
            {
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.GenerateMipmap, (int)All.True);
            }
        }
        
        public static Texture loadEmptyCubeMap(int dim)
        {
            Texture cube = new Texture();
            cube.BindingTarget = TextureTarget.TextureCubeMap;

            GL.BindTexture(TextureTarget.TextureCubeMap, cube.ID);

            setClamp2Edge(TextureTarget.TextureCubeMap);
            setMagMinFilter(TextureTarget.TextureCubeMap);

            for (int i = 0; i < 6; i++)
            {
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgba8, dim, dim, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            }
            GL.BindTexture(TextureTarget.TextureCubeMap, 0);
            return cube;
        }
        #region loading 6th sides cubemap from 6th image files
        
        public static Texture loadCubeMap(string[] cubeMapSides)
        {
            Texture cube = new Texture();
            cube.BindingTarget = TextureTarget.TextureCubeMap;

            GL.BindTexture(TextureTarget.TextureCubeMap, cube.ID);
            if (cubeMapSides.Length == 6)
            {
                for (int i = 0; i < 6; i++)
                {
                    Bitmap image;
                    try
                    {
                        image = new Bitmap(cubeMapSides[i]);
                        BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                        ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                        OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                        image.UnlockBits(data);
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }

                }
                setClamp2Edge(TextureTarget.TextureCubeMap);
                setMagMinFilter(TextureTarget.TextureCubeMap);
                GL.GenerateMipmap(GenerateMipmapTarget.TextureCubeMap);
                GL.BindTexture(TextureTarget.TextureCubeMap, 0);

            }
            else
            {
                Console.WriteLine("Warning: Incorrect number of cube map sides!");
            }

            return cube;
        }
        #endregion
        public static Texture getNoizeTexture(int w, int h)
        {
            Random rnd = new Random();
            Texture noize = new Texture();
            float[] ssaoNoizetexture = new float[w * h * 3];
            for (int i = 0; i < w * h * 3; i += 3)
            {
                ssaoNoizetexture[i] = (float)(rnd.NextDouble() * 2.0f - 1.0f);
                ssaoNoizetexture[i + 1] = (float)(rnd.NextDouble() * 2.0f - 1.0f);
                ssaoNoizetexture[i + 2] = 0;
            }
            noize.LoadData2Texture(w, h, ssaoNoizetexture);
            noize.Bind();
            setNearestFilter(TextureTarget.Texture2D);
            setRepeat(TextureTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            return noize;
        }
    }
}
