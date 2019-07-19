namespace AhoraCore.Core.CES.ICES
{
    interface IComponent
    {
        void Update();
        void Input();
        void Render();
        void Disable();
        void Delete();
    }
}
