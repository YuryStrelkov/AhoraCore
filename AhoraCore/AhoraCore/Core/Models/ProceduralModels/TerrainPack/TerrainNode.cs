﻿using AhoraCore.Core.Cameras;
using AhoraCore.Core.DataManaging;
using AhoraCore.Core.Models.ProceduralModels.TerrainPack;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
    /// <summary>
    /// TODO Трансформациии увязать нормальным образом 
    /// </summary>

    public  class TerrainNode: ATerrainNode
    {
        

        public string grassLodName{ get;private set;}


        public override void Render()
        {
           
            if (isLeaf)
            {

                GetParent().TerrainShader.SetUniform("index", Index);

                GetParent().TerrainShader.SetUniformf("gap", Gap);

                GetParent().TerrainShader.SetUniformi("lod", Lod);

                GetParent().TerrainShader.SetUniform("location", Location);

                GetParent().TerrainShader.SetUniform("WorldTransMatrix", GetParent().GetWorldTransMat());

                GetParent().TerrainShader.SetUniform("LocTransMatrix", GetNodeLoclTrans().GetTransformMat());

                GL.DrawArrays(PrimitiveType.Patches, 0, GetParent().NodePachModel.VerticesNumber);
            }
            else
            {

                if (FrustumCulled(CameraInstance.Get()))
                {
                    childsNodes[0].Render();
                    childsNodes[1].Render();
                    childsNodes[2].Render();
                    childsNodes[3].Render();
                }

              
            }
        }

        public void RenderGrass()
        {

            if (isLeaf)
            {

                GetParent().TerrainGrassShader.SetUniformi("lod", Lod);

                GetParent().TerrainGrassShader.SetUniformf("gap", Gap);

                GetParent().TerrainGrassShader.SetUniform("WorldTransMatrix", GetParent().GetWorldTransMat());

                GetParent().TerrainGrassShader.SetUniform("LocTransMatrix", GetNodeLoclTrans().GetTransformMat());


                GeometryStorageManager.Data.RenderIteam(grassLodName);
            }
            else
            {

                if (FrustumCulled(CameraInstance.Get()))
                {
                    childsNodes[0].RenderGrass();
                    childsNodes[1].RenderGrass();
                    childsNodes[2].RenderGrass();
                    childsNodes[3].RenderGrass();
                }


            }
        }

        public override void Update()
        {
           if (CameraInstance.Get().GetWorldPos().Y > config.ScaleY)
            {
                worldPosition.Y = config.ScaleY;
            }
            else
            {
                worldPosition.Y = CameraInstance.Get().GetWorldPos().Y;
            }
            UpdateChildsNodes();
        }


        public override void Delete()
        {
        }

        public override void Enable()
        {
        }

        public override void Disable()
        {
        }

        public override void Input()
        {
        }

        public override void Clear()
        {
        }

        public TerrainNode(TerrainConfig config, Vector2 location, int lod, Vector2 index) : base(config, location, lod, index)
        {
            grassLodName= lod == 6? "grass_lod_0" : "grass_lod_2";
            grassLodName = lod == 5 ? "grass_lod_1" : "grass_lod_2";
        }

    }
}
