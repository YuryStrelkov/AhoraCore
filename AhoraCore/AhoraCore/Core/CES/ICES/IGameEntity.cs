using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.CES.ICES
{
    public interface IGameEntity : IBehavoir, ITransformable
    {
        void AddComponent(ComponentsTypes Key, AComponent<IGameEntity> component);
        AComponent<IGameEntity> GetComponent(ComponentsTypes Key);
        T GetComponent<T>(ComponentsTypes Key) where T : AComponent<IGameEntity> ;
      ///  void UpdateUniforms(AShader shader);
    }
}