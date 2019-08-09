using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.StandartBuffers;
using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Models.ProceduralModels.TerrainPack;

using System;

namespace AhoraCore.Core.Models.ProceduralModels
{
    public class Terrain:GameEntity
    {

        private TerrainConfig configuration;

        public static void CreateTerrain()
        {
            if (GameEntityStorrage.Entities.Iteams.ContainsKey("terrain"))
            {
                Console.WriteLine("Terrain is allready created");
                return;
            }

            Terrain t = new Terrain();

            GameEntityStorrage.Entities.AddItem("terrain", t);
        }


        private Terrain():base()
        {
            Init(Properties.Resources.TerrainSettings, false);
        }

        public TerrainConfig Configuration
        {
            get
            {
                return configuration;
            }
        }

        private void CreateGrassLods()
        {
            //x,y,z,u,v,nx,ny,nz

            //FloatBuffer buffer = new FloatBuffer();

            //buffer.Put(-0.5f); buffer.Put(0); buffer.Put(0);  buffer.Put(0f); buffer.Put(0); buffer.Put(0); buffer.Put(1); buffer.Put(0);
            //buffer.Put( 0.5f); buffer.Put(0); buffer.Put(0);  buffer.Put(1f); buffer.Put(0); buffer.Put(0); buffer.Put(1); buffer.Put(0);
            //buffer.Put( 0.5f); buffer.Put(0); buffer.Put(-1); buffer.Put(1f); buffer.Put(1); buffer.Put(0); buffer.Put(1); buffer.Put(0);
            //buffer.Put(-0.5f); buffer.Put(0); buffer.Put(-1); buffer.Put(0f); buffer.Put(1); buffer.Put(0); buffer.Put(1); buffer.Put(0);

            float[] v = {
                -0.5000f,0.0000f,-0.0000f ,0.0f,0.00f, 0.0f,1.0000f,-0.0000f,
                 0.5000f,0.0000f,-0.0000f, 1.0f,0.00f, 0.0f,1.0000f,-0.0000f,
                 0.5000f,0.0000f,-1.0000f, 1.0f,1.00f, 0.0f,1.0000f,-0.0000f,
                -0.5000f,0.0000f,-1.0000f, 0.0f,1.00f, 0.0f,1.0000f,-0.0000f,
            };/*
                -0.3536f,0.3536f,-0.0000f, 0.0000f,0.0000f,0.7071f,0.7071f,-0.0000f,
                0.3536f,-0.3536f,-0.0000f, 1.0000f,0.0000f,0.7071f,0.7071f,-0.0000f,
                0.3536f,-0.3536f,-1.0000f, 1.0000f,1.0000f,0.7071f,0.7071f,-0.0000f,
                -0.3536f,0.3536f,-1.0000f, 0.0000f,1.0000f,0.7071f,0.7071f,-0.0000f,
                0.0000f,0.5000f,-0.0000f, 0.0000f,0.0000f,1.0000f,-0.0000f,-0.0000f,
                -0.0000f,-0.5000f,-0.0000f, 1.0000f,0.0000f,1.0000f,-0.0000f,-0.0000f,
                -0.0000f,-0.5000f,-1.0000f, 1.0000f,1.0000f,1.0000f,-0.0000f,-0.0000f,
                0.0000f,0.5000f,-1.0000f, 0.0000f,1.0000f,1.0000f,-0.0000f,-0.0000f,
                0.3536f,0.3536f,-0.0000f, 0.0000f,0.0000f,0.7071f,-0.7071f,-0.0000f,
                -0.3536f,-0.3536f,-0.0000f, 1.0000f,0.0000f,0.7071f,-0.7071f,-0.0000f,
                -0.3536f,-0.3536f,-1.0000f, 1.0000f,1.0000f,0.7071f,-0.7071f,-0.0000f,
                0.3536f,0.3536f,-1.0000f, 0.0000f,1.0000f,0.7071f,-0.7071f,-0.0000f,};
*/
            int[] f =
           {    1, 2, 3,
                3, 4, 1 };/*,
                5, 6, 7,
                7, 8, 5,
                9, 10, 11,
                11,12, 9,
                13,14, 15,
                15,16, 13};*/

          int AttributesMask = VericesAttribytes.V_POSITION | VericesAttribytes.V_UVS| VericesAttribytes.V_NORMAL;

          GeometryStorrageManager.Data.AddGeometry(AttributesMask, "grass_lod_0", v, f);

        }

        public void Init(string config, bool fromfile = true)
        {
            configuration = new TerrainConfig();

            if (fromfile)
            {
                configuration.LoadConfigFromFile(config);
            } else
            {
                configuration.LoadConfigFromString(config);
            }

            ShaderStorrage.Sahaders.AddItem("TerrainShader", new TerrainShader());

            ShaderStorrage.Sahaders.AddItem("TerrainGrassShader", new TerrainGrassShader());

            CreateGrassLods();

            AddComponent("terrain_qt",new TerrainQuadTree(this,configuration));
        }

        public new void Update()
        {           
             if (CameraInstance.Get().IsUpdated)///only when moved
            {
                  base.Update();
            }
        }
    }
}
