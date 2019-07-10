using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Transformations;
using System.Collections.Generic;

namespace AhoraCore.Core.CES 
{
    public class Node: IComponent<Node>, ITransformable
    {
        Node Parent;

        List<Node> Children;

        Transform WorldTransform;

        Transform LocalTransform;

        public Node()
        {
            WorldTransform = new Transform(0, 0, 0);
            LocalTransform = new Transform(0, 0, 0);
            Children = new List<Node>();
        }

        public Node(float x, float y, float z)
        {
            WorldTransform = new Transform(x, y, z);
            LocalTransform = new Transform(0, 0, 0);
            Children = new List<Node>();
        }

        public void Input()
        {
            foreach (Node n in Children)
            {
                n.Input();
            }
        }

        public void Update()
        {
            foreach (Node n in Children)
            {
                n.Update();
            }

        }

        public void Render()
        {
            foreach (Node n in Children)
            {
                n.Render();
            }
        }

        public void Disable()
        {
            foreach (Node n in Children)
            {
                n.Disable();
            }
        }

        public void Delete()
        {
            foreach (Node n in Children)
            {
                n.Delete();
            }
        }

        public void AddChild(Node Child)
        {
            Child.SetParent(this);
            Children.Add(Child);
        }

        public void SetParent(Node Parent)
        {
            this.Parent = Parent;
        }

        public Node GetParent()
        {
            return Parent;
        }

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
