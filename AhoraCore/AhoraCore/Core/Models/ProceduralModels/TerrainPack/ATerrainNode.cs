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

        protected Transform localTransform;//, worldTransform;

        public Transform GetNodeLoclTrans()
        {
            return localTransform;
        }



        //public Transform GetNodeWorldTrans()
        //{
        //    return worldTransform;
        //}

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
            float distance = (CameraInstance.Get().GetWorldTransform().Position - worldPosition).Length;

            if (distance < config.LodRanges[lod])
            {
                AddChildNodes(lod + 1);
                childsNodes[0].Update();
                childsNodes[1].Update();
                childsNodes[2].Update();
                childsNodes[3].Update();
            }
            else if (distance > config.LodRanges[lod])
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
            if (childsNodes.Count == 0)//цыклы для педиков
            {
                childsNodes.Add(new TerrainNode(config, new Vector2(GetNodeLoclTrans().Position.X, GetNodeLoclTrans().Position.Z ), lod, new Vector2(0,0)));
                childsNodes[0].SetParent(GetParent());
                childsNodes.Add(new TerrainNode(config, new Vector2(GetNodeLoclTrans().Position.X , GetNodeLoclTrans().Position.Z +  gap / 2), lod, new Vector2(0, 1)));
                childsNodes[1].SetParent(GetParent());
                childsNodes.Add(new TerrainNode(config, new Vector2(GetNodeLoclTrans().Position.X +  gap / 2, GetNodeLoclTrans().Position.Z ), lod, new Vector2(1, 0)));
                childsNodes[2].SetParent(GetParent());
                childsNodes.Add(new TerrainNode(config, new Vector2(GetNodeLoclTrans().Position.X  + gap / 2, GetNodeLoclTrans().Position.Z +  gap / 2), lod, new Vector2(1, 1)));
                childsNodes[3].SetParent(GetParent());
            }
        }

        public void ComputeWorldPosition()
        {
            worldPosition = new Vector3((localTransform.Position.X + gap / 2) * config.ScaleXZ - config.ScaleXZ / 2, 0,
                                        (localTransform.Position.Z + gap / 2) * config.ScaleXZ - config.ScaleXZ / 2);
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

            this.lod = lod;

            this.index = index;

            isLeaf = true;

            this.location = location;

            gap = 1.0f / (TerrainQuadTree.GetRootNodesNumber() * (float)Math.Pow(2, lod));

            localTransform = new Transform(location.X, 0, location.Y);

            GetNodeLoclTrans().SetScaling(gap, 0, gap);

           //// location = index * gap;

            ComputeWorldPosition();
        }
    }
}