using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.CES.ICES
{
    public interface IGameEntity : IBehavoir, ITransformable
    {
        void AddComponent(string Key, AComponent<IGameEntity> component);
        void UpdateUniforms(AShader shader);
    }
}