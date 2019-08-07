using AhoraCore.Core.Cameras;

namespace AhoraCore.Core.CES.ICES
{
    interface IFrustumCulled
    {
        bool FrustumCulled(Camera frustumcam);

        float FrustumRadius { get; set; }
    }
}
