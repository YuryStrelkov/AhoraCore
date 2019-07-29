using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.Transformations;
using OpenTK;
using System;
using System.Collections.Generic;

namespace AhoraCore.Core.Models.ProceduralModels.TerranPack
{

    /// <summary>
    /// Сделать компанентом!!!
    /// </summary>
    public  class TerrainNode:AComponent
    {
        List<TerrainNode> childsNodes;

        private Transform localTransform, worldTransform;

        public Transform GetNodeLoclTrans()
        {
            return localTransform;
        }

        public Transform GetNodeWorldTrans()
        {
            return worldTransform;
        }

        private TerrainConfig config;

        private bool isLeaf;

        private int lod;

        private Vector2 location;

        private Vector3 worldPosition;

        private Vector2 index;

        private float gap;

        public TerrainConfig Config
        {
            get
            {
                return config;
            }

            set
            {
                config = value;
            }
        }

        public bool IsLeaf
        {
            get
            {
                return isLeaf;
            }

            set
            {
                isLeaf = value;
            }
        }

        public int Lod
        {
            get
            {
                return lod;
            }

            set
            {
                lod = value;
            }
        }

        public Vector2 Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        public Vector3 WorldPosition
        {
            get
            {
                return worldPosition;
            }

            set
            {
                worldPosition = value;
            }
        }

        public Vector2 Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }

        public float Gap
        {
            get
            {
                return gap;
            }

            set
            {
                gap = value;
            }
        }

        public override void Render()
        {
            if (isLeaf)
            {
              //  base.Render();
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
      //      base.Update();
        }

        private void UpdateChildsNodes()
        {
            float distance = (CameraInstance.Get().GetLocalTransform().Position - worldPosition).Length;

            if (distance < config.LodRanges[lod])
            {
                AddChildNodes(lod + 1);
            }
            else if (distance< config.LodRanges[lod])
            {
                RemoveChildNodes();
            }
        }

        private void AddChildNodes(int lod)
        {
            if (IsLeaf)
            {
                IsLeaf = false;
            }
            if (childsNodes.Count == 0)
            {
                for (int i = 0 ; i < 2 ; i++ )
                {
                    for (int j = 0; j < 2; j++)
                    {
                        childsNodes[i * 2 + j] = new TerrainNode(config, new Vector2(i * gap / 2, j * gap / 2), lod, new Vector2(i, j));
                    }
                }
            }
        }

        public void ComputeWorldPosition()
        {
            worldPosition = new Vector3((location.X + gap / 2)*config.ScaleXZ-config.ScaleXZ/2,0, (location.Y + gap / 2) * config.ScaleXZ - config.ScaleXZ / 2);
        }

        private void RemoveChildNodes()
        {
            if (!IsLeaf)
            {
                IsLeaf = true;
            }
            if (childsNodes.Count!=0)
            {
                childsNodes.Clear();
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

        public TerrainNode(TerrainConfig config, Vector2 location, int lod, Vector2 index)
        {
            childsNodes = new List<TerrainNode>(4);
            this.config = config;
            this.location = location;
            this.lod = lod;
            this.index = index;

            localTransform = new Transform(0, 0, 0);

            worldTransform = new Transform(0, 0, 0);
            
            gap = 1.0f / (TerrainQuadTree.GetRootNodesNumber() * (float)Math.Pow(2, lod));

            GetNodeLoclTrans().SetScaling(gap, 0,gap);

            GetNodeLoclTrans().SetTranslation(location.X,0, location.Y);

            GetNodeWorldTrans().SetScaling(config.ScaleXZ, config.ScaleY, config.ScaleXZ);

            GetNodeWorldTrans().SetTranslation(-config.ScaleXZ / 2, 0, -config.ScaleXZ / 2);

            ComputeWorldPosition();
        }

    }
}
