using OpenTK;

namespace AhoraCore.Core.CES.ICES
{
 public  interface ITransformable
    {
        ///Transform GetLocalTransform();
        ///Transform GetWorldTransform();

        void SetLocalTranslation(float x, float  y, float z);
        void SetWorldTranslation(float x, float y, float z);

        void SetLocalTranslation(Vector3 translation);
        void SetWorldTranslation(Vector3 translation);

        void SetLocalScale(float x, float y, float z);
        void SetWorldScale(float x, float y, float z);

        void SetLocalScale(Vector3 scale);
        void SetWorldScale(Vector3 scale);

        void SetLocalRotation(float x, float y, float z);
        void SetWorldRotation(float x, float y, float z);

        void SetLocalRotation(Vector3 rotation);
        void SetWorldRotation(Vector3 rotation);

        Matrix4 GetLocalTransMat();
        Matrix4 GetWorldTransMat();

        Vector3 GetLocalPos();
        Vector3 GetWorldPos();

        Vector3 GetWorldScl();
        Vector3 GetLocalScl();

        Vector3 GetWorldRot();
        Vector3 GetLocalRot();



    }
}
