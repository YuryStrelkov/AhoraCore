using System;
using System.IO;

namespace AhoraCore.Core.Models.ProceduralModels.TerranPack
{
    public class TerrainConfig
    {
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

        private void LoadConfig(string[] lines)
        {
            foreach (string line in lines)
            {
                string[] lineTokens = line.Split(' ');
                switch (lineTokens[0])
                {
                    case "ScaleY": ScaleY = float.Parse(lineTokens[1]); break;

                    case "ScaleXZ": ScaleXZ = float.Parse(lineTokens[1]); break;

                    case "LodRanges":
                        for (int i = 2; i < lineTokens.Length; i++)
                        {
                            int val = int.Parse(lineTokens[i]);
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

        public void LoadConfigFromFile(string strings)
        {
            string[] lines = strings.Split('\n');
            LoadConfig(lines);
        }

        public void LoadConfigFromString(string filname)
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
       //     LoadConfig(path);
        }

    }
}
