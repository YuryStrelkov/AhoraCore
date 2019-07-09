using AhoraCore.Core.Transformations;

namespace AhoraCore.Core.CES.ICES
{
    interface ITransformable
    {
        Transform GetLocalTransform();
        Transform GetWorldTransform();
    }
}
