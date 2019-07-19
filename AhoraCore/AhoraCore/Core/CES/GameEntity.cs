using OpenTK;
using System.Collections.Generic;

namespace AhoraCore.Core.CES
{
    public class GameEntity:Node
    {
        private Dictionary<string, AComponent> components;

        public GameEntity():base()
        {
            components = new Dictionary<string, AComponent>();
        }

        public GameEntity(Vector3 wordPos, Vector3 wordOrient) : base()
        {
            WorldTransform.SetWorldRotation(wordOrient);

            WorldTransform.SetWorldTranslation(wordPos);

            components = new Dictionary<string, AComponent>();
        }

        public void AddComponent(string Key, AComponent component)
        {
            if (!components.ContainsKey(Key))
            {
                components.Add(Key, component);
            }
            else
            {
                components[Key].Delete();
                components[Key] = component;
            }
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
