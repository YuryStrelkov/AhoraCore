using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.StandartBuffers;
using AhoraCore.Core.Models.ProceduralModels;
using AhoraProject.Ahora.Core.Display;
using System;

namespace AhoraCore
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            DisplayDevice dd = new DisplayDevice(800,600);

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

            Icosphere.Create(2, out vIco,out iIco);

            ///TODO отследить расширение буфера в случчае, если мы пытаемся добьваить вершин больше чем влезает 

            dd.Scene.AddGeometry("1", VericesAttribytes.V_POSITION , vertices, indices);

            dd.Scene.AddGeometry("ico", VericesAttribytes.V_POSITION| VericesAttribytes.V_UVS, vIco, iIco);

            dd.Scene.AddGeometry("2", VericesAttribytes.V_POSITION, vertices1, indices1);

            dd.Scene.AddGeometry("3", VericesAttribytes.V_POSITION, vertices2, indices2);

            dd.Run();
        }
    }
}
