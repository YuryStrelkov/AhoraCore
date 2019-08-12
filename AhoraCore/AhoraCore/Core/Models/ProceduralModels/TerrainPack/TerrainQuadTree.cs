using AhoraCore.Core.Buffers;
using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Materials;
using AhoraCore.Core.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;


namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
    // Сделать компонентом
    public class TerrainQuadTree : AComponent<IGameEntity>
    {
        public Material TerrainMaterial { get; private set; }

        public AShader TerrainShader { get; private set; }

        public AShader TerrainGrassShader { get; private set; }

        public PatchBuffer NodePachModel { get; private set; }

        private static int rootNodes = 8;

        List<TerrainNode> terrainNodes;

        private TerrainConfig config;

        private delegate void renderer();

        public float[] GeneratePath()
        {
            return new float[] {
             0,0,
             0.333f, 0,
             0.666f, 0,
             1, 0,

             0,      0.333f,
             0.333f, 0.333f,
             0.666f, 0.333f,
             1,      0.333f,



             0,      0.666f,
             0.333f, 0.666f,
             0.666f, 0.666f,
             1,      0.666f,


             0,      1,
             0.333f, 1,
             0.666f, 1,
             1,      1};    
        }

        public static int GetRootNodesNumber()
        {
            return rootNodes;
        }

        public override void Delete()
        {
            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Delete();
            }
        }

        public override void Enable()
        {
            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Enable();
            }
        }

        public override void Disable()
        {
            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Disable();
            }
        }

        public override void Input()
        {
            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Input();
            }
        }

        public override void Render()
        {
           drawTerrain();
           drawGrass();
         //   drawTrees();
        }


        private void drawTerrain()
        {
            TerrainShader.Bind();

            ///   TerrainMaterial.Bind(TerrainShader);

            TerrainShader.SetUniform("viewMatrix", CameraInstance.Get().ViewMatrix);

            TerrainShader.SetUniform("projectionMatrix", CameraInstance.Get().PespectiveMatrix);

            TerrainShader.SetUniformf("ScaleY", config.ScaleY);

            TerrainShader.SetUniform("cameraPosition", CameraInstance.Get().GetWorldTransform().Position);

            TerrainShader.SetUniformf("tessellationFactor", config.TessellationFactor);

            TerrainShader.SetUniformf("tessellationSlope", config.TessellationSlope);

            TerrainShader.SetUniformf("tessellationShift", config.TessellationShift);


            TerrainShader.SetUniformi("lod_morph_area[" + 0 + "]", config.LodRanges[0]);
            TerrainShader.SetUniformi("lod_morph_area[" + 1 + "]", config.LodRanges[1]);
            TerrainShader.SetUniformi("lod_morph_area[" + 2 + "]", config.LodRanges[2]);
            TerrainShader.SetUniformi("lod_morph_area[" + 3 + "]", config.LodRanges[3]);
            TerrainShader.SetUniformi("lod_morph_area[" + 4 + "]", config.LodRanges[4]);
            TerrainShader.SetUniformi("lod_morph_area[" + 5 + "]", config.LodRanges[5]);
            TerrainShader.SetUniformi("lod_morph_area[" + 6 + "]", config.LodRanges[6]);
            TerrainShader.SetUniformi("lod_morph_area[" + 7 + "]", config.LodRanges[7]);

            GL.ActiveTexture(TextureUnit.Texture0);
            config.HeightMap.Bind();
            TerrainShader.SetUniformi("heightMap", 0);

            GL.ActiveTexture(TextureUnit.Texture1);
            config.NormalMap.Bind();
            TerrainShader.SetUniformi("normalMap", 1);

            GL.BindVertexArray(NodePachModel.ID);
            NodePachModel.EnableAttribytes();

            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Render();
            }

            NodePachModel.DisableAttribytes();
            GL.BindVertexArray(0);
            TerrainShader.Unbind();
        }

        private void drawGrass()
        {
            TerrainGrassShader.Bind();

            TerrainGrassShader.SetUniform("viewMatrix", CameraInstance.Get().ViewMatrix);

            TerrainGrassShader.SetUniform("projectionMatrix", CameraInstance.Get().PespectiveMatrix);

            TerrainGrassShader.SetUniformf("ScaleY", config.ScaleY);


            TerrainGrassShader.SetUniformf("ScaleXZ", config.ScaleXZ);
            //    TerrainGrassShader.SetUniform("cameraPosition", CameraInstance.Get().GetWorldTransform().Position);

            GL.ActiveTexture(TextureUnit.Texture0);

            config.HeightMap.Bind();

            TerrainGrassShader.SetUniformi("heightMap", 0);

           for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].RenderGrass();
            }

            TerrainGrassShader.Unbind();
        }

        private void drawTrees()
        {

        }

        public override void Update()
        {
            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Update();
            }
        }

        public override void Clear()
        {
            for (int i = 0; i < terrainNodes.Count; i++)
            {
                terrainNodes[i].Clear();
            }
        }

        public TerrainQuadTree(IGameEntity parent, TerrainConfig config) : base()
        {

            this.config = config;
            SetParent(parent);

            TerrainMaterial = MaterialStorrage.Materials.GetItem("DefaultMaterial");

            TerrainShader = ShaderStorrage.Sahaders.GetItem("TerrainShader");

            TerrainGrassShader = ShaderStorrage.Sahaders.GetItem("TerrainGrassShader");

            terrainNodes = new List<TerrainNode>(rootNodes * rootNodes);

            for (int i = 0; i < rootNodes; i++)
            {
                for (int j = 0; j < rootNodes; j++)
                {
                    terrainNodes.Add(new TerrainNode(config, new Vector2(i * 1.0f / rootNodes, j * 1.0f / rootNodes), 0, new Vector2(i, j)));
                    terrainNodes[i * rootNodes + j].SetParent(this);
                }
            }

            NodePachModel = new PatchBuffer(GeneratePath(), 2);

            GetWorldTransform().SetScaling(config.ScaleXZ, config.ScaleY, config.ScaleXZ);

            GetWorldTransform().SetTranslation(-config.ScaleXZ / 2f, 0, -config.ScaleXZ / 2f);
        }
    }
}