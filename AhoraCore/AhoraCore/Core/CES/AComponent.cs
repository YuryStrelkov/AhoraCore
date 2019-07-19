using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Transformations;

namespace AhoraCore.Core.CES
{
    public abstract class AComponent : IComponent, ITransformable
    {
        GameEntity parent;

        public abstract void Delete();

        public abstract void Disable();

        public abstract void Input();

        public abstract void Render();

        public abstract void Update();

        public void SetParent(GameEntity parent)
        {
            this.parent = parent;
        }

        public GameEntity GetParent()
        {
            return parent;
        }

        public Transform GetLocalTransform()
        {
          return  parent.GetLocalTransform();
        }

        public Transform GetWorldTransform()
        {
            return parent.GetWorldTransform();
        }

    }
}
