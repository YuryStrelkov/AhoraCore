using AhoraCore.Core.Buffers;
using AhoraCore.Core.Buffers.IBuffres;
using AhoraCore.Core.Context;
using AhoraCore.Core.Models.ProceduralModels;
using AhoraCore.Core.Scene3D;
using OpenTK.Graphics.OpenGL;
using System;

namespace AhoraCore
{
    class Program
    {

 ///       [STAThread]
        static void Main(string[] args)
        {
              MainContext.InitMainContext();

             MainContext.UseDefferedRenderer();
 
             SceneLoader.LoadScene(Properties.Resources.Scene);

             Terrain.CreateTerrain();

             MainContext.RunContext();

             MainContext.DisposeMainContext(); 

           /* EditableBuffer<float> buffer = new EditableBuffer<float>();
            buffer.BindingTarget = BufferTarget.ArrayBuffer;
            buffer.Create(5);
            buffer.LoadBufferData(new float[] { 2, 2, 2, 2, 2 });
            DebugBuffers.displayBufferData(buffer);

            Console.WriteLine("__________________________________");

            EditableBuffer<float> buffer1 = new EditableBuffer<float>();
            buffer1.BindingTarget = BufferTarget.ArrayBuffer;
            buffer1.Create(3);
            buffer1.LoadBufferData(new float[] { 3, 3, 3 });

            EditableBuffer<float> buffer2 = new EditableBuffer<float>();
            buffer2.BindingTarget = BufferTarget.ArrayBuffer;
            buffer2.Create(3);
            buffer.MergeBuffers(buffer2);
            buffer.MergeBuffers(buffer1);
            DebugBuffers.displayBufferData(buffer, false);
            Console.WriteLine("__________________________________");
            buffer.MergeBuffers(buffer);
            DebugBuffers.displayBufferData(buffer, false);
            Console.ReadKey();*/
        }
    }
}
