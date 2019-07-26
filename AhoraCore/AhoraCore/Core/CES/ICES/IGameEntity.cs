namespace AhoraCore.Core.CES.ICES
{
    public interface IGameEntity:IBehavoir,ITransformable
    {
        void AddComponent(string Key, AComponent component);
    }
}