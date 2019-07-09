using System.Collections.Generic;

namespace AhoraCore.Core.CES
{
    public class GameEntity:Node
    {
        private Dictionary<string, AComponent> components;

        public GameEntity()
        {
            components = new Dictionary<string, AComponent>();
        }

        public void AddComponent(string Key, AComponent component)
        {

        }

        public new void Update()
        {
            foreach (string k in components.Keys)
            {
                components[k].Update();
            }
            base.Update();

        }

        public new void Input()
        {
            foreach (string k in components.Keys)
            {
                components[k].Input();
            }
            base.Input();

        }

        public new void Render()
        {
            foreach (string k in components.Keys)
            {
                 components[k].Render();
            }
            base.Render();

        }
    }
}
