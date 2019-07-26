using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Transformations;

namespace AhoraCore.Core.CES 
{
    public class Node
    {
        protected Transform WorldTransform;

        protected Transform LocalTransform;

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

        //public void Input()
        //{
           
        //}

        //public void Update()
        //{
        //}

        //public void Render()
        //{
        //}

        //public void Disable()
        //{
        //}

        //public void Delete()
        //{
        //}

        public Transform GetLocalTransform()
        {
            return LocalTransform;
        }

        public Transform GetWorldTransform()
        {
            return WorldTransform;
        }
    }
}
