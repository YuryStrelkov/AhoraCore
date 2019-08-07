using System;
using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Transformations;
using OpenTK;

namespace AhoraCore.Core.CES 
{
    public class Node: IFrustumCulled
    {
        protected Transform WorldTransform;

        protected Transform LocalTransform;

        private  float frustumR;

        public float FrustumRadius
        {
            get
            {
                return frustumR;
            }

            set
            {
                frustumR = value;
            }
        }

        public Node()
        {
            WorldTransform = new Transform(0, 0, 0);
            LocalTransform = new Transform(0, 0, 0);
        }

        public Node(float x, float y, float z)
        {
            WorldTransform = new Transform(x, y, z);
            LocalTransform = new Transform(0, 0, 0);
        }

        public Transform GetLocalTransform()
        {
            return LocalTransform;
        }

        public Transform GetWorldTransform()
        {
            return WorldTransform;
        }

        public bool FrustumCulled(Camera frustumcam)
        {
            return Vector3.Dot(WorldTransform.Position - frustumcam.GetWorldTransform().Position, -frustumcam.LookAt) > 0;
        }
    }
}
