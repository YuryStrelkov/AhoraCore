using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Buffers.IBuffers;
using AhoraCore.Core.Buffers.StandartBuffers;
using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Materials;
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
     
            float v = 0.707f;
            float p = v / 2;


            float[] vertices = {
                  -0.5f, 0f, 1f, /*v0*/ 0, 0, /*uv0*/ 0, 0, 1, /*n0*/
	              -0.5f, 0f, 0f, /*v1*/ 0, 1, /*uv1*/ 0, 0, 1, /*n1*/
			       0.5f, 0f,  0f, /*v2*/ 1, 1, /*uv2*/ 0, 0, 1, /*n2*/
                   0.5f, 0f, 1f, /*v3*/ 1, 0, /*uv0*/ 0, 0, 1,  /*n3*/

                 -v, v, 1f, /*v0*/ 0, 0, /*uv0*/ v, v, 0, /*n0*/
				 -v,  v, 0f, /*v1*/ 0, 1, /*uv1*/ v, v, 0, /*n1*/
				  v, -v, 0f, /*v2*/ 1, 1, /*uv2*/ v, v, 0, /*n2*/
                  v,-v, 1f, /*v3*/ 1, 0, /*uv0*/ v, v, 0,  /*n3*/
                  
 
                  
                  
                  v, 1f, -v, /*v0*/ 0, 0, /*uv0*/ -v, -v, 0, /*n0*/
				  v, 0f, -v, /*v1*/ 0, 1, /*uv1*/ -v, -v, 0, /*n1*/
				 -v, 0f,  v, /*v2*/ 1, 1, /*uv2*/ -v, -v, 0, /*n2*/
                 -v, 1f,  v, /*v3*/ 1, 0, /*uv0*/ -v, -v, 0 /*n3*/
             };

            int[] faces = {

                0,1,2,//top left triangle (v0, v1, v3)
				2,3,1,//bottom right triangle (v3, v1, v2)

                4,5,6,//top left triangle (v0, v1, v3)
				6,7,5,//bottom right triangle (v3, v1, v2)

                8,9,10,//top left triangle (v0, v1, v3)
				10,11,9,//bottom right triangle (v3, v1, v2)

            };

            FloatBuffer vbuffer = new FloatBuffer(vertices.Length * 16);
            IntegerBuffer ibuffer = new IntegerBuffer(faces.Length * 16);

            float l = (1 - 0.25f)*0.5f;

            float x = 0, y = 0;

            float dx = 0.25f, dy = 0.25f;

            int idxShift = 0;

            for (int i=0;i<4;i++)
            {
                x =0.125f + dx * i;
                for (int j=0;j<4;j++)
                {
                 y = 0.125f + dy * j;

                    idxShift = (i * 4 + j)* vertices.Length/5;

                    for (int k = 0; k < vertices.Length - 8; k += 8)
                    {
                        vbuffer.Put(vertices[k ] + x);
                        vbuffer.Put(vertices[k + 1]);
                        vbuffer.Put(vertices[k + 2] + y);
                        vbuffer.Put(vertices[k + 3]);
                        vbuffer.Put(vertices[k + 4]);
                        vbuffer.Put(vertices[k + 5]);
                        vbuffer.Put(vertices[k + 6]);
                        vbuffer.Put(vertices[k + 7]);
                    }
                    /* ibuffer.Put(faces[0] + idxShift);
                     ibuffer.Put(faces[1] + idxShift);
                     ibuffer.Put(faces[2] + idxShift);
                     ibuffer.Put(faces[3] + idxShift);
                     ibuffer.Put(faces[4] + idxShift);
                     ibuffer.Put(faces[5] + idxShift);
                     ibuffer.Put(faces[6] + idxShift);
                    ;*/
                    for (int k = 0;k< faces.Length; k++ )
                    {
                        ibuffer.Put(faces[k]);
                    }

                }
            }



            ///vbuffer.Put(vertices);

          int AttributesMask = VericesAttribytes.V_POSITION | VericesAttribytes.V_UVS| VericesAttribytes.V_NORMAL;

         /////   GeometryStorrageManager.Data.AddGeometry(AttributesMask, "SkyDomeModel", vIco, iIco);

            GeometryStorrageManager.Data.AddGeometry(AttributesMask, "grass_lod_0", vbuffer, ibuffer);

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


            TextureStorrage.Textures.AddItem("GrassTexture", new Texture(Properties.Resources.grass));
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
