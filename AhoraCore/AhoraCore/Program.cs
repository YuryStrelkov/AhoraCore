using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.StandartBuffers;
using AhoraCore.Core.CES;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Models;
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
            FrameDisplay = new DisplayDevice(800, 600);

            TextureStorrage.Initilaze();

            MaterialStorrage.Initilaze();

            ShaderStorrage.Initilaze();

            GeometryStorrageManager.Initialize();

            GameEntityStorrage.Initialize();
        }

        private static void End()
        {
            TextureStorrage.Textures.DeleteStorrage();

            MaterialStorrage.Materials.DeleteStorrage();

            ShaderStorrage.Sahaders.DeleteStorrage();

            GeometryStorrageManager.Data.DeleteManager();

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
                -10f,-1,  10f,//v0
				-10f,-1, -10f,//v1
			     10f,-1, -10f,//v2
				 10f,-1,  10f,//v3
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


            FloatBuffer vIco; IntegerBuffer iIco;
            
            int AttributesMask;

            Icosphere.Create(3,1, out vIco, out iIco, out AttributesMask);
            ///TODO отследить расширение буфера в случчае, если мы пытаемся добьваить вершин больше чем влезает 
           //// GeometryStorrageManager.Data.AddGeometry(VericesAttribytes.V_POSITION,"1",  vertices, indices);

            GeometryStorrageManager.Data.AddGeometry(AttributesMask, "ico", vIco, iIco);
            GameEntityStorrage.Entities.AddItem("ico",new GameEntity());
            GameEntityStorrage.Entities.GetItem("ico").AddComponent("ico_model", new Model("ico", "DefaultMaterial", "DefaultShader"));

            GameEntityStorrage.Entities.GetItem("ico").GetWorldTransform().SetWorldScaling(10, 10, 10);
           //    GeometryStorrageManager.Data.AddGeometry(VericesAttribytes.V_POSITION, "2", vertices1, indices1);

           ///  GeometryStorrageManager.Data.AddGeometry(VericesAttribytes.V_POSITION, "3", vertices2, indices2);

           FrameDisplay.Run();

            End();

            Console.ReadKey();
        }
    }
}
