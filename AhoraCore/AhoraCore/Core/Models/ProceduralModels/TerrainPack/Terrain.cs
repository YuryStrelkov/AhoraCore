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
/*
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
            float[] vertices = {
                -0.5f, 1f, 0f, /*v0*/ 0, 0, /*uv0*/ 0, 0, 1, /*n0*/
				-0.5f, 0f, 0f, /*v1*/ 0, 1, /*uv1*/ 0, 0, 1, /*n1*/
				 0.5f, 0f, 0f, /*v2*/ 1, 1, /*uv2*/ 0, 0, 1, /*n2*/
                 0.5f, 1f, 0f, /*v3*/ 1, 0, /*uv0*/ 0, 0, 1  /*n3*/
        };

            int[] f = {
                0,1,3,//top left triangle (v0, v1, v3)
				3,1,2//bottom right triangle (v3, v1, v2)
		    };
         

          int AttributesMask = VericesAttribytes.V_POSITION | VericesAttribytes.V_UVS| VericesAttribytes.V_NORMAL;

         /////   GeometryStorrageManager.Data.AddGeometry(AttributesMask, "SkyDomeModel", vIco, iIco);

            GeometryStorrageManager.Data.AddGeometry(AttributesMask, "grass_lod_0", vertices, f);

        }

        public void Init(string config, bool fromfile = true)
        {
            configuration = new TerrainConfig();

            if (fromfile)
            {
                configuration.LoadConfigFromFile(config);
            }
            else
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
