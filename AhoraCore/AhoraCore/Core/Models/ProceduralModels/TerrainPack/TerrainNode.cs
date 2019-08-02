﻿using AhoraCore.Core.Cameras;
using AhoraCore.Core.Models.ProceduralModels.TerrainPack;
using OpenTK;
using System;

namespace AhoraCore.Core.Models.ProceduralModels.TerranPack
{
    /// <summary>
    /// TODO Трансформациии увязать нормальным образом 
    /// </summary>

    public  class TerrainNode: ATerrainNode
    {
         public override void Render()
        {

          //  Console.WriteLine(index);
            if (isLeaf)
            {
                GetParent().TerrainShader.SetUniform("WorldTransMatrix", GetParent().GetWorldTransform().GetTransformMat());
                GetParent().TerrainShader.SetUniform("LocTransMatrix",   GetNodeLoclTrans().GetTransformMat());
                GetParent().NodePachModel.DrawPatch();
           }
            else
            {
                childsNodes[0].Render();
                childsNodes[1].Render();
                childsNodes[2].Render();
                childsNodes[3].Render();
            }
        }

        public override void Update()
        {
            if (CameraInstance.Get().GetLocalTransform().Position.Y > config.ScaleY)
            {
                worldPosition.Y = config.ScaleY;
            }
            else
            {
                worldPosition.Y = CameraInstance.Get().GetLocalTransform().Position.Y;
                UpdateChildsNodes();
            }
        }

        protected override void AddChildNodes(int lod)
        {
            if (IsLeaf)
            {
                IsLeaf = false;
            }
            if (childsNodes.Count == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        childsNodes.Add(new TerrainNode(config, new Vector2(i * gap / 2, j * gap / 2), lod, new Vector2(i, j)));
                        childsNodes[i * 2 + j].SetParent(GetParent());
                    }
                }
            }
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

        public TerrainNode(TerrainConfig config, Vector2 location, int lod, Vector2 index):base( config,  location,  lod,  index)
        {
            
        }

    }
}
