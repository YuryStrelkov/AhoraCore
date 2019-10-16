using AhoraCore.Core.Shaders;
using OpenTK;

namespace AhoraCore.Core.CES.ICES
{
    public interface IGameEntity : IBehavoir, ITransformable
    {
        void AddComponent(ComponentsTypes Key, AComponent<IGameEntity> component);
        AComponent<IGameEntity> GetComponent(ComponentsTypes Key);
        T GetComponent<T>(ComponentsTypes Key) where T : AComponent<IGameEntity> ;
        Matrix4 GetParentTransform();
      ///  void UpdateUniforms(AShader shader);
    }
}