using AhoraCore.Core.Context;
using AhoraCore.Core.Models.ProceduralModels;
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

            Icosphere.CreateSkyDome(2);

            Terrain.CreateTerrain();

            MainContext.RunContext();

            MainContext.DisposeMainContext();

            Console.ReadKey();
        }
    }
}
