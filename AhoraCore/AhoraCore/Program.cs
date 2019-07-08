using AhoraCore.Core.Buffers.IBuffers;
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
                -0.1f+0.5f,  0.1f+0.5f, 0f,//v0
				-0.1f+0.5f, -0.1f+0.5f, 0f,//v1
			     0.1f+0.5f, -0.1f+0.5f, 0f,//v2
				 0.1f+0.5f,  0.1f+0.5f, 0f,//v3
		};


            float[] vertices2 = {
                -0.1f+0.75f,  0.1f+0.75f, 0f,//v0
				-0.1f+0.75f, -0.1f+0.75f, 0f,//v1
			     0.1f+0.75f, -0.1f+0.75f, 0f,//v2
				 0.1f+0.75f,  0.1f+0.75f, 0f,//v3
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

            dd.Scene.AddGeometry("1", VericesAttribytes.V_POSITION , vertices, indices);

            dd.Scene.AddGeometry("2", VericesAttribytes.V_POSITION, vertices1, indices1);

            dd.Scene.AddGeometry("3", VericesAttribytes.V_POSITION, vertices2, indices2);

            dd.Run();
        }
    }
}
