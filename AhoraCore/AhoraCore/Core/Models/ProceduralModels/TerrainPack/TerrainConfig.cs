using AhoraCore.Core.Materials;
using System;
using System.IO;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.Materials.GpuGpu;

namespace AhoraCore.Core.Models.ProceduralModels.TerranPack
{
    public class TerrainConfig
    {

        public Texture HeightMap { get; private set; }

        public Texture NormalMap { get; private set; }

        private NormalMapRendererShader normalMapRendererShader;

        private int N;

        private float Strength;

        public int TessellationFactor { get; private set; }

        public float TessellationSlope { get; private set; }

        public float TessellationShift { get; private set; }

        public float ScaleY { get; private set; }

        public float ScaleXZ { get; private set; }

        public int[] LodRanges = new int[8];

        public int[] LodMorphingArea = new int[8];

        public int UpdateMorphingArea(int lod)
        {
            return (int)(ScaleXZ/TerrainQuadTree.GetRootNodesNumber()* Math.Pow(2,lod));
        }

        private void setLodRange(int  index,int range )
        {
            LodRanges[index] = range;
            LodMorphingArea[index] = range - UpdateMorphingArea(index + 1);
        }

        private void RenderNormalMap()
        {
            NormalMap = new Texture();
            NormalMap.Bind();
            NormalMap.BilinearFilter();
            GL.TexStorage2D(TextureTarget2d.Texture2D,(int)(Math.Log(N)/ Math.Log(2)),SizedInternalFormat.Rgba32f,N,N);
            normalMapRendererShader.Bind();
            normalMapRendererShader.UpdateUniforms(HeightMap, N, Strength);
            GL.BindImageTexture(0, NormalMap.ID,0,false,0,TextureAccess.WriteOnly, SizedInternalFormat.Rgba32f);
            GL.DispatchCompute(N/16,N/16,1);
            GL.Finish();
            NormalMap.Bind();
            NormalMap.BilinearFilter();

        }

        private void LoadConfig(string[] lines)
        {
            foreach (string line in lines)
            {
                string[] lineTokens = line.Split(' ');

                if (lineTokens.Length==0)
                {
                    continue;
                }

                switch (lineTokens[0])
                {
                    case "ScaleY": ScaleY = float.Parse(lineTokens[1]); break;

                    case "ScaleXZ": ScaleXZ = float.Parse(lineTokens[1]); break;

                    case "TessellationFactor": TessellationFactor = int.Parse(lineTokens[1]); break;

                    case "TessellationSlope": TessellationSlope = float.Parse(lineTokens[1]); break;

                    case "TessellationShift": TessellationShift = float.Parse(lineTokens[1]); break;

                    case "HeigthMap":
                        if (lineTokens[1].Equals("true"))
                        {
                            HeightMap = new Texture(lineTokens[2]);
                        }
                        else
                        {
                            HeightMap = new Texture(Properties.Resources.hm0);
                        }
                        HeightMap.Bind();
                        HeightMap.BilinearFilter();
                        Strength = 8;
                        N = HeightMap.Width;
                        RenderNormalMap();

                        ; break;

                    case "LodRanges":
                        for (int i = 0; i < 8; i++)
                        {
                            int val = (int)( float.Parse(lineTokens[i + 1]));
                            if (val == 0)
                            {
                                LodRanges[i] = 0;
                                LodMorphingArea[i] = 0;
                            }
                            else
                            {
                                setLodRange(i, val);
                            }

                        }
                        ; break;
                }
            }
        }

        public void LoadConfigFromString (string strings)
        {
            string[] lines = strings.Split('\n');
            LoadConfig(lines);
        }

        public void LoadConfigFromFile(string filname)
        {
            try
            {
                string[] lines = File.ReadAllLines(filname);
                LoadConfig(lines);

            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.StackTrace);
            }
        }

        public TerrainConfig()
        {
            normalMapRendererShader = NormalMapRendererShader.getInstance();
        }

    }
}
