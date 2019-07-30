using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES;
using AhoraCore.Core.Models.ProceduralModels.TerranPack;
using AhoraCore.Core.Transformations;
using OpenTK;
using System;
using System.Collections.Generic;


namespace AhoraCore.Core.Models.ProceduralModels.TerrainPack
{
    public abstract class ATerrainNode : AComponent<TerrainQuadTree>
    {
        protected List<TerrainNode> childsNodes;

        protected Transform localTransform, worldTransform;

        public Transform GetNodeLoclTrans()
        {
            return localTransform;
        }

        public Transform GetNodeWorldTrans()
        {
            return worldTransform;
        }

        protected TerrainConfig config;

        protected bool isLeaf;

        protected int lod;

        protected Vector2 location;

        protected Vector3 worldPosition;

        protected Vector2 index;

        protected float gap;

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

        protected void UpdateChildsNodes()
        {
            float distance = (CameraInstance.Get().GetLocalTransform().Position - worldPosition).Length;

            if (distance < config.LodRanges[lod])
            {
                AddChildNodes(lod + 1);
            }
            else if (distance < config.LodRanges[lod])
            {
                RemoveChildNodes();
            }
        }

        protected abstract void AddChildNodes(int lod);

        public void ComputeWorldPosition()
        {
            worldPosition = new Vector3((location.X + gap / 2) * config.ScaleXZ - config.ScaleXZ / 2, 0, (location.Y + gap / 2) * config.ScaleXZ - config.ScaleXZ / 2);
        }

        protected void RemoveChildNodes()
        {
            if (!IsLeaf)
            {
                IsLeaf = true;
            }
            if (childsNodes.Count != 0)
            {
                childsNodes.Clear();
            }

        }

        public ATerrainNode(TerrainConfig config, Vector2 location, int lod, Vector2 index)
        {
            childsNodes = new List<TerrainNode>(4);
            this.config = config;
            this.location = location;
            this.lod = lod;
            this.index = index;

            localTransform = new Transform(0, 0, 0);

            worldTransform = new Transform(0, 0, 0);

            gap = 1.0f / (TerrainQuadTree.GetRootNodesNumber() * (float)Math.Pow(2, lod));

            GetNodeLoclTrans().SetScaling(gap, 0, gap);

            GetNodeLoclTrans().SetTranslation(location.X, 0, location.Y);

            GetNodeWorldTrans().SetScaling(config.ScaleXZ, config.ScaleY, config.ScaleXZ);

            GetNodeWorldTrans().SetTranslation(-config.ScaleXZ / 2, 0, -config.ScaleXZ / 2);

            ComputeWorldPosition();
        }
    }
}
