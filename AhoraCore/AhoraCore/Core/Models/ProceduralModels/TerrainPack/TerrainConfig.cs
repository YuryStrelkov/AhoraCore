using AhoraCore.Core.Materials;
using System;
using System.IO;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.Materials.GpuGpu;
using AhoraCore.Core.Buffers.DataStorraging;

namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
    public class TerrainConfig
    {

        public Texture HeightMap { get; private set; }

        public Texture NormalMap { get; private set; }

        public float TBNRange { get; private set; }

        private NormalMapRendererShader normalMapRendererShader;

        ////TerrainMaterial TMaterial;
        
        private int N;

        private float Strength;

        public int TessellationFactor { get; private set; }

        public float TessellationSlope { get; private set; }

        public float TessellationShift { get; private set; }

        public float ScaleY { get; private set; }

        public float ScaleXZ { get; private set; }

        public float[] LodRanges = new float[8];

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

           

            int i = 0;
            for (i = 0; i< lines.Length; i++ )
            {
                string[] lineTokens = lines[i].Split(' ');

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

                    case "TBNRange": TBNRange = float.Parse(lineTokens[1]); break;

                    case "material":
                        TerrainMaterial TMaterial = new TerrainMaterial();
                        i = i + TMaterial.ReadMaterial(i,ref lines);
                        MaterialStorrage.Materials.AddItem("TerrainMaterial", TMaterial);
                        break;

                    case "texture":
                        TextureStorrage.Textures.AddItem(lineTokens[1], new Texture(lineTokens[2]));//TessellationShift = float.Parse(lineTokens[1]);
                          break;

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
                        Strength = 60;
                        N = HeightMap.Width;
                        RenderNormalMap();

                        ; break;

                    case "LodRanges":
                        for (int k = 0; k < 8; k++)
                        {
                            int val = (int)( float.Parse(lineTokens[k + 1]));
                            if (val == 0)
                            {
                                LodRanges[k] = 0;
                                LodMorphingArea[k] = 0;
                            }
                            else
                            {
                                setLodRange(k, val);
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
