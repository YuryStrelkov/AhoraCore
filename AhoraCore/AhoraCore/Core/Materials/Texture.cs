﻿using AhoraCore.Core.Buffers.IBuffers;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AhoraCore.Core.Materials
{
   public class Texture: ABindableObject<TextureTarget>
    {
        public static Texture defaultDiffuse = new Texture(0.5f, 0.5f, 0.5f);

        public static Texture ErrorTexture = new Texture(1f, 0.0f, 0.0f);

        public static Texture defaultNormal = new Texture(0.0f, 0.0f, 0.0f);

        string textureName = "";

        public int Width { get; protected set; }

        public int Height { get; protected set; }

        public Texture(Vector3 c)
        {
            LoadColor(c);
            BindingTarget = TextureTarget.Texture2D;
        }

        public Texture(string pathToTexture)
        {
            try
            {
                int width, height;
                ID = TextureFromImage(pathToTexture, out width, out height);
                Width = width; Height = height;
                BindingTarget = TextureTarget.Texture2D;
            }
            catch (Exception e)
            {
                Console.WriteLine("Texture : " + pathToTexture + " was not found");
                LoadColor(1, 0, 0);
                BindingTarget = TextureTarget.Texture2D;
            }

        }

        public Texture()
        {
            Create();
            BindingTarget = TextureTarget.Texture2D;
        }

        public Texture(float r, float g, float b)
        {
            LoadColor(r, g, b);
            BindingTarget = TextureTarget.Texture2D;
        }


        public Texture(Bitmap image)
        {
            BindingTarget = TextureTarget.Texture2D;
            Create();
            ReloadImage(image);
        }

        public override void Bind()
        {
            GL.BindTexture(BindingTarget, ID);
        }

        public override void Create()
        {
            if (ID==-1)
            {
                ID = GL.GenTexture(); 
            }
           
        }

        public override void Delete()
        {
            GL.DeleteTexture(ID);
            ID = -1;
        }

        public override void Unbind()
        {
            GL.BindTexture(BindingTarget, 0);
        }

        public override void Bind(TextureTarget bindTarget)
        {
            BindingTarget = bindTarget;
            GL.BindTexture(bindTarget, ID);
        }

        public void BiuldEmptyCubeMap(int size)
        {
            Bind(TextureTarget.TextureCubeMap);

            TextureLoader.setClamp2Edge(TextureTarget.TextureCubeMap);
            TextureLoader.setMagMinFilter(TextureTarget.TextureCubeMap);
            for (int i = 0; i < 6; i++)
            {
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgba8, size, size, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            }
            Unbind();
        }

        void LoadColor(Vector3 color)
        {
             LoadColor(color.X, color.Y, color.Z);
        }

        void LoadColor(float r, float g, float b)
        {
            Width = 2;
            Height = 2;
            Create();
            Bind();
            float[] TexColor = new float[16] { r, g, b, 1f, r, g, b, 1f,
                                               r, g, b, 1f, r, g, b, 1f};
            GL.TexImage2D(BindingTarget, 0, PixelInternalFormat.Rgba32f, 2, 2, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.Float, TexColor);

            TextureLoader.setMagMinFilter(BindingTarget);
            TextureLoader.setClamp2Edge(BindingTarget);
            Unbind();
            
        }

        public void ReloadImage(Bitmap image)
        {
            Width = image.Width;
            Height = image.Height;

            Bind();

            BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(BindingTarget, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            GL.TexParameterI(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, ref TextureLoader.LINEAR);
            GL.TexParameterI(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, ref TextureLoader.LINEAR_MIP_MAP);
            GL.TexParameterI(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, ref TextureLoader.CLAMP_TO_EDGE);
            GL.TexParameterI(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, ref TextureLoader.CLAMP_TO_EDGE);

            image.UnlockBits(data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            Unbind();
            //   return 0;

        }


        public void NoFilter()
        {

            if (TextureTarget.Texture2D == BindingTarget)
            {
                GL.TexParameterI(BindingTarget, TextureParameterName.TextureMagFilter, ref TextureLoader.NEAREST);

                GL.TexParameterI(BindingTarget, TextureParameterName.TextureMinFilter, ref TextureLoader.NEAREST);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }

            if (TextureTarget.TextureCubeMap == BindingTarget)
            {
                GL.TexParameter(BindingTarget, TextureParameterName.TextureMinFilter,   TextureLoader.NEAREST);

                GL.TexParameter(BindingTarget, TextureParameterName.TextureMagFilter,   TextureLoader.NEAREST);

                GL.TexParameter(BindingTarget, TextureParameterName.GenerateMipmap, (int)All.True);
            }
        }

        public void BilinearFilter()
        {
            GL.TexParameterI(BindingTarget, TextureParameterName.TextureMagFilter, ref TextureLoader.LINEAR);

            GL.TexParameterI(BindingTarget, TextureParameterName.TextureMinFilter, ref TextureLoader.LINEAR);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void TrilinearFilter()
        {
            GL.TexParameterI(BindingTarget, TextureParameterName.TextureMagFilter, ref TextureLoader.LINEAR);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.TexParameterI(BindingTarget, TextureParameterName.TextureMinFilter, ref TextureLoader.LINEAR_MIP_MAP);

          
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
                ID = TextureLoader.loadTextureFromImage(textureBitmap);
                Bind();
                BilinearFilter();
                return ID;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Something goes wrong with texture " + filename);
                return -1;
            }
        }

        public override void Clear()
        {
            Delete();
            LoadColor(0.5f, 0.5f, 0.5f);
        }
    }
}
