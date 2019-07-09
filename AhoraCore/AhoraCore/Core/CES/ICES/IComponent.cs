namespace AhoraCore.Core.CES.ICES
{
    interface IComponent<T>
    {
        void Update();
        void Input();
        void Render();
        void Disable();
        void Delete();
        void SetParent(T parent);
        T GetParent();
    }
}
