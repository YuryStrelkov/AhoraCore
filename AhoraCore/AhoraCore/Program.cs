using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.CES;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Models.ProceduralModels;
using AhoraProject.Ahora.Core.Display;
using System;

namespace AhoraCore
{
    class Program
    {
        private static DisplayDevice FrameDisplay;

        private static void Begin()
        {
            FrameDisplay = new DisplayDevice(1024, 840);

            TextureStorrage.Initilaze();

            MaterialStorrage.Initilaze();

            ShaderStorrage.Initilaze();

            GeometryStorageManager.Initialize();

            GameEntityStorrage.Initialize();
        }

        private static void End()
        {
            TextureStorrage.Textures.DeleteStorrage();

            MaterialStorrage.Materials.DeleteStorrage();

            ShaderStorrage.Sahaders.DeleteStorrage();

            GeometryStorageManager.Data.DeleteManager();

            GameEntityStorrage.Entities.DeleteStorrage();

            FrameDisplay.Dispose();
        }

        [STAThread]
        static void Main(string[] args)
        {
            Begin();
            float[] vertices = {
                -0.5f, 0.5f, 0f,//v0
				-0.5f, -0.5f, 0f,//v1
				0.5f, -0.5f, 0f,//v2
				0.5f, 0.5f, 0f,//v3
		};


            float[] vertices1 = {
                -100f,-1,  100f,0,1,0,//v0
				-100f,-1, -100f,0,1,0,//v1
			     100f,-1, -100f,0,1,0,//v2
				 100f,-1,  100f,0,1,0,//v3
		};


            float[] vertices2 = {
                -0.1f+0.75f,  0.1f+0.75f, 2f,//v0
				-0.1f+0.75f, -0.1f+0.75f, 2f,//v1
			     0.1f+0.75f, -0.1f+0.75f, 2f,//v2
				 0.1f+0.75f,  0.1f+0.75f, 2f,//v3
		};

            int[] indices = {
                0,1,3,//top left triangle (v0, v1, v3)
				3,1,2//bottom right triangle (v3, v1, v2)
		};
            int[] indices1 = {//plus 4 because 4 vertices
                4,5,7,//top left triangle (v0, v1, v3)
				7,5,6 //bottom right triangle (v3, v1, v2)
		};

            int[] indices2 = {//plus 4 because 4 vertices
                8,9,11,//top left triangle (v0, v1, v3)
				11,9,10 //bottom right triangle (v3, v1, v2)
		};

            Icosphere.CreateSkyDome(2);

         //   Terrain.CreateTerrain();

            FrameDisplay.Run();

            End();

            Console.ReadKey();
        }
    }
}
