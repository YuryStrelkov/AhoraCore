using AhoraCore.Core.Context;
using AhoraCore.Core.Models.ProceduralModels;
using AhoraCore.Core.Scene3D;
using System;

namespace AhoraCore
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            MainContext.InitMainContext();

            MainContext.UseDefferedRenderer();

            SceneLoader.LoadScene(Properties.Resources.Scene);

            Icosphere.CreateSkyDome(2);

            Terrain.CreateTerrain();
            
            MainContext.RunContext();

            MainContext.DisposeMainContext();

            Console.ReadKey();
        }
    }
}
