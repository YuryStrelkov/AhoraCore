﻿using AhoraCore.Core.Buffers;
using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.CES.Components;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Materials;
using AhoraCore.Core.Materials.AbstractMaterial;
using AhoraCore.Core.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;


namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
    // Сделать компонентом
    public class TerrainQuadTree : AComponent<IGameEntity>
    {
        public AMaterial Terrain_Material { get; private set; }

        public AShader TerrainShader { get; private set; }

        public AShader TerrainGrassShader { get; private set; }

        public PatchBuffer NodePachModel { get; private set; }

        TransformComponent ParentTransform;

        private static int rootNodes = 8;

        List<TerrainNode> terrainNodes;

        private TerrainConfig config;

        private delegate void renderer();

        float t = 0;

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
           //drawGrass();
           drawTrees();
        }


        private void drawTerrain()
        {
            TerrainShader.Bind();

            CameraInstance.Get().Bind(TerrainShader);

            Bind(TerrainShader);

            Terrain_Material.Bind(TerrainShader);

            ParentTransform.Bind(TerrainShader);

            TerrainShader.SetUniform("cameraPosition", CameraInstance.Get().GetWorldPos());

            GL.ActiveTexture(TextureUnit.Texture0 + Terrain_Material.Textures.Count);
            config.HeightMap.Bind();
            TerrainShader.SetUniformi("heightMap", Terrain_Material.Textures.Count);

            GL.ActiveTexture(TextureUnit.Texture1 + Terrain_Material.Textures.Count);
            config.NormalMap.Bind();
            TerrainShader.SetUniformi("normalMap", Terrain_Material.Textures.Count + 1);

            GL.ActiveTexture(TextureUnit.Texture2 + Terrain_Material.Textures.Count);
            config.BlendingMap.Bind();
            TerrainShader.SetUniformi("blendMap", Terrain_Material.Textures.Count + 2);


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

            Bind(TerrainGrassShader);

            CameraInstance.Get().Bind(TerrainGrassShader);

            ParentTransform.Bind(TerrainGrassShader);

            // TerrainMaterial.Bind(TerrainGrassShader);


            GL.ActiveTexture(TextureUnit.Texture0);

            config.HeightMap.Bind();

            TerrainGrassShader.SetUniformi("heightMap", 0);


            GL.ActiveTexture(TextureUnit.Texture1);

            TextureStorrage.Textures.GetItem("GrassTexture").Bind();

            TerrainGrassShader.SetUniformi("grassMap", 1);


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
            Component = "TerrainQTree";
            
            #region Terrain settings unifrom buffer
            EnableBuffering("TerrainSettings");
   
            MarkBufferItem("ScaleY", 1);
            MarkBufferItem("ScaleXZ", 1);
            MarkBufferItem("tessellationFactor", 1);
            MarkBufferItem("tessellationSlope", 1);
            MarkBufferItem("tessellationShift", 1);
            MarkBufferItem("TBNrange", 1);
            MarkBufferItem("lod_morph_area", 8);
            //MarkBufferItem("lod_morph_area[" + 0 + "]", 1);
            //MarkBufferItem("lod_morph_area[" + 1 + "]", 1);
            //MarkBufferItem("lod_morph_area[" + 2 + "]", 1);
            //MarkBufferItem("lod_morph_area[" + 3 + "]", 1);
            //MarkBufferItem("lod_morph_area[" + 4 + "]", 1);
            //MarkBufferItem("lod_morph_area[" + 5 + "]", 1);
            //MarkBufferItem("lod_morph_area[" + 6 + "]", 1);
            //MarkBufferItem("lod_morph_area[" + 7 + "]", 1);

            ConfirmBuffer();

            SetBindigLocation(UniformBindingsLocations.TerrainSettings);

            UniformBuffer.Bind();

            UniformBuffer.UpdateBufferIteam("ScaleY",  config.ScaleY);

            UniformBuffer.UpdateBufferIteam("ScaleXZ", config.ScaleXZ);

            UniformBuffer.UpdateBufferIteam("tessellationFactor", config.TessellationFactor);

            UniformBuffer.UpdateBufferIteam("tessellationSlope",  config.TessellationSlope);

            UniformBuffer.UpdateBufferIteam("tessellationShift",  config.TessellationShift);

            UniformBuffer.UpdateBufferIteam("TBNrange", config.TBNRange);

            UniformBuffer.UpdateBufferIteam("lod_morph_area", config.LodRanges);

            //DebugBuffers.displayBufferData(UniformBuffer);
            //UniformBuffer.UpdateBufferIteam("lod_morph_area[" + 0 + "]", config.LodRanges[0]);
            //UniformBuffer.UpdateBufferIteam("lod_morph_area[" + 1 + "]", config.LodRanges[1]);
            //UniformBuffer.UpdateBufferIteam("lod_morph_area[" + 2 + "]", config.LodRanges[2]);
            //UniformBuffer.UpdateBufferIteam("lod_morph_area[" + 3 + "]", config.LodRanges[3]);
            //UniformBuffer.UpdateBufferIteam("lod_morph_area[" + 4 + "]", config.LodRanges[4]);
            //UniformBuffer.UpdateBufferIteam("lod_morph_area[" + 5 + "]", config.LodRanges[5]);
            //UniformBuffer.UpdateBufferIteam("lod_morph_area[" + 6 + "]", config.LodRanges[6]);
            //UniformBuffer.UpdateBufferIteam("lod_morph_area[" + 7 + "]", config.LodRanges[7]);
            UniformBuffer.Unbind();

            #endregion

            this.config = config;

            SetParent(parent);

            Terrain_Material = GetParent().GetComponent<MaterialComponent>(ComponentsTypes.MaterialComponent).MateriaL;

            TerrainShader = GetParent().GetComponent<ShaderComponent>(ComponentsTypes.TerrainShader).Shader;

            TerrainGrassShader = GetParent().GetComponent<ShaderComponent>(ComponentsTypes.TerrainFloraShader).Shader;

            ParentTransform = GetParent().GetComponent<TransformComponent>(ComponentsTypes.TransformComponent);


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

            GetParent().SetWorldScale(config.ScaleXZ, config.ScaleY, config.ScaleXZ);

            GetParent().SetWorldTranslation(-config.ScaleXZ / 2f, 0, -config.ScaleXZ / 2f);

       
        }
    }
}