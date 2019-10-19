using AhoraCore.Core.Buffers.StandartBuffers;
using AhoraCore.Core.Context;
using AhoraCore.Core.GUI;
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

            IntegerBuffer b = new IntegerBuffer(5);

        /*    b.PutDirect(0, 1);

            b.PutDirect(1, 2);

            b.PutDirect(2, 3);

            b.PutDirect(3, 4);

            b.PutDirect(4, 5);

            b.PutDirect(5, 1);

            b.PutDirect(1, 15);
            b.PutDirect(2, 15);
            b.PutDirect(3, 15);
            b.PutDirect(4, 15);

            b.PutDirect(10, 1);

            b.Execept(0, 5);*/
             MainContext.InitMainContext();

             MainContext.UseDefferedRenderer();

            ///GUICreator.CreateUIpannel("root","UI_ID", "root", "root");

            //GUICreator.CreateUItext("root","UI_ID",2,2,"ABCD");

             SceneLoader.LoadScene(Properties.Resources.Scene);

             Terrain.CreateTerrain();

             MainContext.RunContext();

             MainContext.DisposeMainContext(); 

            Console.ReadKey();
        }
    }
}
