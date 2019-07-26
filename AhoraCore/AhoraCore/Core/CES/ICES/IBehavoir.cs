namespace AhoraCore.Core.CES.ICES
{

    /// <summary>
    /// Стандартный набор функций, как для компанента,так и для сущности 
    /// </summary>
    public interface IBehavoir
    {
        void Update();
        void Input();
        void Render();
        void Disable();
        void Enable();
        void Delete();
        void Clear();
    }
}
