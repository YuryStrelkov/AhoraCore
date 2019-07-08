using AhoraCore.Core.Buffers.IBuffers;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AhoraCore.Core.Materials
{
   public class Texture: ABindableObject
    {
        public static Texture defaultDiffuse = new Texture(0.5f, 0.5f, 0.5f);

        public static Texture defaultNormal = new Texture(0.0f, 0.0f, 0.0f);

        string textureName = "";

        public int Width { get; protected set; }

        public int Height { get; protected set; }

        public TextureTarget TextureType { get;  set; }

        public Texture(Vector3 c)
        {
            ID = LoadColor(c);
        }

        public Texture(string pathToTexture)
        {
            int width,  height;
            ID = TextureFromImage(pathToTexture, out width, out height);
            Width = width;  Height = height;
        }

        public Texture()
        {
            Create();
            TextureType = TextureTarget.Texture2D;
        }

        public Texture(float r, float g, float b)
        {
            ID = LoadColor(r, g, b);
        }

        public override void Bind()
        {
            GL.BindTexture(TextureType, ID);
        }

        public override void Create()
        {
            int id; GL.GenTextures(1, out id);
            ID = id;
        }

        public override void Delete()
        {
            GL.DeleteTexture(ID);
        }

        public override void Unbind()
        {
            GL.BindTexture(TextureType, 0);
        }

        public void BiuldEmptyCubeMap(int size)
        {
            GL.BindTexture(TextureType, ID);

            TextureLoader.setClamp2Edge(TextureTarget.TextureCubeMap);
            TextureLoader.setMagMinFilter(TextureTarget.TextureCubeMap);
            for (int i = 0; i < 6; i++)
            {
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgba8, size, size, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            }
            GL.BindTexture(TextureTarget.TextureCubeMap, 0);
        }

        int LoadColor(Vector3 color)
        {
            return LoadColor(color.X, color.Y, color.Z);
        }

        int LoadColor(float r, float g, float b)
        {
            int texID;
            Width = 2;
            Height = 2;
            GL.GenTextures(1, out texID);

            GL.BindTexture(TextureType, texID);
            float[] TexColor = new float[16] { r, g, b, 1f, r, g, b, 1f,
                                               r, g, b, 1f, r, g, b, 1f};
            GL.TexImage2D(TextureType, 0, PixelInternalFormat.Rgba32f, 2, 2, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.Float, TexColor);

            TextureLoader.setMagMinFilter(TextureType);
            TextureLoader.setClamp2Edge(TextureType);
            GL.BindTexture(TextureType, 0);
            return texID;
        }

        public void ReloadImage(Bitmap image)
        {
            Width = image.Width;
            Height = image.Height;

            GL.BindTexture(TextureType, ID);

            BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureType, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            int linear = (int)All.Linear;
            int linearMipMap = (int)All.LinearMipmapLinear;
            int clamp2Edge = (int)All.ClampToEdge;
            GL.TexParameterI(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, ref linear);
            GL.TexParameterI(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, ref linearMipMap);
            GL.TexParameterI(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, ref clamp2Edge);
            GL.TexParameterI(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, ref clamp2Edge);

            image.UnlockBits(data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureType, 0);
            //   return 0;

        }

        public void LoadData2Texture(int w, int h, float[] data)
        {
            Bind();
            Width = w;
            Height = h;
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb32f, w, h, 0,
                          OpenTK.Graphics.OpenGL.PixelFormat.Rgb, PixelType.Float, data);
            Unbind();
        }

        private int TextureFromImage(string filename, out int w, out int h)
        {
            w = 0;
            h = 0;
            try
            {
                ///    Console.WriteLine(filename);
                Bitmap textureBitmap = new Bitmap(filename);
                w = textureBitmap.Width;
                h = textureBitmap.Height;
                string[] path_split = filename.Split('\\');
                textureName = path_split[path_split.Length - 1];
                return TextureLoader.loadTextureFromImage(textureBitmap);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                return -1;
            }
        }
    }
}
