using AhoraCore.Core.Transformations;

namespace AhoraCore.Core.CES.ICES
{
 public  interface ITransformable
    {
        Transform GetLocalTransform();
        Transform GetWorldTransform();
    }
}
