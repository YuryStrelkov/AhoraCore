
using OpenTK;

namespace AhoraCore.Core.CES.ICES
{
    public interface IGameEntity : IBehavoir, ITransformable
    {
        string EntityID { get; set; }

        void AddComponent(ComponentsTypes Key, AComponent<IGameEntity> component);
        
        AComponent<IGameEntity> GetComponent(ComponentsTypes Key);

        T GetComponent<T>(ComponentsTypes Key) where T : AComponent<IGameEntity> ;
   
    }
}