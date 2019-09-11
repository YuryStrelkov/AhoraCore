using AhoraCore.Core.Cameras;
using AhoraCore.Core.CES.Components;
using AhoraCore.Core.CES.ICES;
using OpenTK;

namespace AhoraCore.Core.CES 
{
    public class Node: IFrustumCulled 
    {
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
        
        public Node() : base()
        {
        }

        public Node(float x, float y, float z)//:base(new Vector3(x,y,z), Vector3.Zero)
        {
        }

        public bool FrustumCulled(Camera frustumcam)
        {
            return false;/// frustumcam.GetComponent<CameraComponent>(ComponentsTypes.CameraComponent).IsSphereInFrustum(, radius);// Vector3.Dot(WorldTransform.Position - frustumcam.WorldTransform.Position, -frustumcam.LookAt) > 0;
        }

       
    }
}
