using System;
using AhoraCore.Core.CES.ICES;
using OpenTK;

namespace AhoraCore.Core.CES
{
    public abstract class AComponent<ParentType> : ITransformable, IBehavoir where ParentType : ITransformable
    {
        protected ParentType parent;

        public abstract void Delete();

        public abstract void Enable();

        public abstract void Disable();

        public abstract void Input();

        public abstract void Render();

        public abstract void Update();

        public abstract void Clear();

        public void SetParent(ParentType parent)
        {
            this.parent = parent;
        }

        public ParentType GetParent()
        {
            return parent;
        }

        public void SetLocalTranslation(float x, float y, float z)
        {
            parent.SetLocalTranslation(x, y, z);
        }

        public void SetWorldTranslation(float x, float y, float z)
        {
            parent.SetWorldTranslation(x, y, z);
        }

        public void SetLocalTranslation(Vector3 translation)
        {
            parent.SetLocalTranslation(translation);
        }

        public void SetWorldTranslation(Vector3 translation)
        {
            parent.SetWorldTranslation(translation);
        }

        public void SetLocalScale(float x, float y, float z)
        {
            parent.SetLocalScale(x, y, z);
        }

        public void SetWorldScale(float x, float y, float z)
        {
            parent.SetWorldScale(x, y, z);
        }

        public void SetLocalScale(Vector3 scale)
        {
            parent.SetLocalScale(scale);
        }

        public void SetWorldScale(Vector3 scale)
        {
            parent.SetWorldScale(scale);
        }

        public void SetLocalRotation(float x, float y, float z)
        {
            parent.SetLocalRotation(x, y, z);
        }

        public void SetWorldRotation(float x, float y, float z)
        {
            parent.SetWorldRotation(x, y, z);
        }

        public void SetLocalRotation(Vector3 rotation)
        {
            parent.SetLocalRotation(rotation);
        }

        public void SetWorldRotation(Vector3 rotation)
        {
            parent.SetWorldRotation(rotation);
        }

        public Matrix4 GetLocalTransMat()
        {
            return parent.GetLocalTransMat();
        }

        public Matrix4 GetWorldTransMat()
        {
            return parent.GetWorldTransMat();

        }

        public Vector3 GetLocalPos()
        {
            return parent.GetLocalPos();

        }

        public Vector3 GetWorldPos()
        {
            return parent.GetWorldPos();
        }

        public Vector3 GetWorldScl()
        {
            return parent.GetWorldScl();
        }

        public Vector3 GetLocalScl()
        {
            return parent.GetLocalScl();
        }

        public Vector3 GetWorldRot()
        {
            return parent.GetWorldRot();
        }

        public Vector3 GetLocalRot()
        {
            return parent.GetLocalRot();
        }
        /*
public Transform GetLocalTransform()
{
   return parent.GetLocalTransform();
}

public Transform GetWorldTransform()
{
   return parent.GetWorldTransform();
}*/



    }
}