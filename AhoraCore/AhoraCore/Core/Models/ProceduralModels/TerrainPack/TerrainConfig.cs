using AhoraCore.Core.Materials;
using System;
using System.IO;
using OpenTK.Graphics.OpenGL;
using AhoraCore.Core.Materials.GpuGpu;
using AhoraCore.Core.Buffers.DataStorraging;
using Newtonsoft.Json;

namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
    public class TerrainConfig
    {

        public Texture HeightMap { get; private set; }

        public Texture NormalMap { get; private set; }

        public Texture BlendingMap { get; private set; }

        public float TBNRange { get; private set; }

        private NormalMapRendererShader normalMapRendererShader;

        private SplatMapRendererShader BlendMapRendererShader;



        ////TerrainMaterial TMaterial;

        private int N;

        ///private float Strength;

        public int TessellationFactor { get; private set; }

        public float TessellationSlope { get; private set; }

        public float TessellationShift { get; private set; }

        public float ScaleY { get; private set; }

        public float NormalStrength { get; private set; }

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

        private void RenderSplatMap()
        {
            BlendingMap = new Texture();
            BlendingMap.Bind();
            BlendingMap.BilinearFilter();
            GL.TexStorage2D(TextureTarget2d.Texture2D, (int)(Math.Log(N) / Math.Log(2)), SizedInternalFormat.Rgba16, N, N);
            BlendMapRendererShader.Bind();
            BlendMapRendererShader.UpdateUniforms(NormalMap, N);
            GL.BindImageTexture(0, BlendingMap.ID, 0, false, 0, TextureAccess.WriteOnly, SizedInternalFormat.Rgba16);
            GL.DispatchCompute(N / 16, N / 16, 1);
            GL.Finish();
            BlendingMap.Bind();
            BlendingMap.BilinearFilter();
        }

        private void RenderNormalMap()
        {
            NormalMap = new Texture();
            NormalMap.Bind();
            NormalMap.BilinearFilter();
            GL.TexStorage2D(TextureTarget2d.Texture2D,(int)(Math.Log(N)/ Math.Log(2)),SizedInternalFormat.Rgba32f,N,N);
            normalMapRendererShader.Bind();
            normalMapRendererShader.UpdateUniforms(HeightMap, N, NormalStrength);
            GL.BindImageTexture(0, NormalMap.ID,0,false,0,TextureAccess.WriteOnly, SizedInternalFormat.Rgba32f);
            GL.DispatchCompute(N/16,N/16,1);
            GL.Finish();
            NormalMap.Bind();
            NormalMap.BilinearFilter();
        }

        private void LoadConfig(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(json));

            

            TerrainMaterial TMaterial = null;
 
            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        switch (reader.Value.ToString())
                        {
                            case "ScaleY":
                                {
                                    reader.Read();
                                    ScaleY = float.Parse(reader.Value.ToString());
                                    break;
                                }
                            case "NormalStrength":
                                {
                                    reader.Read();
                                    NormalStrength = float.Parse(reader.Value.ToString());
                                    break;
                                }
                            case "ScaleXZ":
                                {
                                    reader.Read();
                                    ScaleXZ = float.Parse(reader.Value.ToString());
                                    break;
                                }
                            case "TessellationFactor":
                                {
                                    reader.Read();
                                    TessellationFactor = int.Parse(reader.Value.ToString());
                                    break;
                                }
                            case "TessellationSlope":
                                {
                                    reader.Read();
                                    TessellationSlope = float.Parse(reader.Value.ToString());
                                    break;
                                }
                            case "TessellationShift":
                                {
                                    reader.Read();
                                    TessellationShift = float.Parse(reader.Value.ToString());
                                    break;
                                }
                            case "TBNRange":
                                {
                                    reader.Read();
                                    TBNRange = float.Parse(reader.Value.ToString());
                                    break;
                                }
                            case "LodRanges":
                                {
                                    reader.Read();
                                    if (reader.TokenType == JsonToken.StartArray)
                                    {
                                        int k = 0;
                                        reader.Read();
                                        while (reader.TokenType != JsonToken.EndArray)
                                        {
                                            int val = (int)(float.Parse(reader.Value.ToString()));
                                            if (val == 0)
                                            {
                                                LodRanges[k] = 0;
                                                LodMorphingArea[k] = 0;
                                            }
                                            else
                                            {
                                                setLodRange(k, val);
                                            }
                                            k++;
                                            reader.Read();
                                        }
                                    }
                                    break;
                                }
                            case "material":
                                {
                                    reader.Read();
                                    if (reader.TokenType == JsonToken.StartArray)
                                    {
                                        while (reader.TokenType != JsonToken.EndArray)
                                        {
                                            if (TMaterial == null)
                                            {
                                                TMaterial = new TerrainMaterial();
                                            }
                                            TMaterial.ReadMaterial(reader);
                                        }
                                       
                                    }
                                    break;
                                }
                            case "texture":
                                {
                                    reader.Read();
                                    if (reader.TokenType == JsonToken.StartArray)
                                    {
                                        while (reader.TokenType != JsonToken.EndArray)
                                        {
                                            reader.Read();
                                            if (reader.TokenType == JsonToken.StartObject)
                                            {
                                                reader.Read(); reader.Read();
                                                string name = reader.Value.ToString();
                                                reader.Read(); reader.Read();
                                                string path = reader.Value.ToString();
                                                TextureStorrage.Textures.AddItem(name, new Texture(path));
                                            }
                                        }
                                    }
                                    break;
                                }

                            case "HeigthMap":
                                {
                                    reader.Read();
                                    if (reader.TokenType == JsonToken.StartArray)
                                    {
                                        reader.Read();
                                        if (reader.Value.ToString().Equals("true"))
                                        {
                                            reader.Read();
                                            HeightMap = new Texture(reader.Value.ToString());
                                        }
                                        else
                                        {
                                            HeightMap = new Texture(Properties.Resources.hm0);
                                        }
                                        HeightMap.Bind();
                                        HeightMap.TrilinearFilter();
                                        N = HeightMap.Width;
                                        RenderNormalMap();
                                        RenderSplatMap();
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Token: {0}", reader.TokenType);
                }
            }

            MaterialStorrage.Materials.AddItem("TerrainMaterial", TMaterial);

            Console.WriteLine("Stop");
        }
        

        /*public void LoadConfigFromString (string strings)
        {
            string[] lines = strings.Split('\n');
            LoadConfig(lines);
        }*/

        public void LoadConfigFromFile(string filname)
        {
            try
            {
                //string[] lines = File.ReadAllLines(filname);
                LoadConfig(filname);

            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.StackTrace);
            }
        }

        public TerrainConfig()
        {
            normalMapRendererShader = NormalMapRendererShader.getInstance();

            BlendMapRendererShader = SplatMapRendererShader.getInstance();
        }

    }
}
