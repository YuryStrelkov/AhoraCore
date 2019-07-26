using AhoraCore.Core.CES.ICES;
using OpenTK;
using System.Collections.Generic;

namespace AhoraCore.Core.CES
{
    public class GameEntity : Node, IGameEntity
    {
        private Dictionary<string, AComponent> components;

        public GameEntity():base()
        {
            components = new Dictionary<string, AComponent>();
        }

        public GameEntity(Vector3 wordPos, Vector3 wordOrient) : base()
        {
            WorldTransform.SetRotation(wordOrient);

            WorldTransform.SetTranslation(wordPos);

            components = new Dictionary<string, AComponent>();
        }

        public void AddComponent(string Key, AComponent component)
        {
            if (!components.ContainsKey(Key))
            {
                components.Add(Key, component);
                components[Key].SetParent(this);
            }
            else
            {
                components[Key].Delete();
                components[Key] = component;
                components[Key].SetParent(this);

            }
        }

        public  void Update()
        {
            foreach (string k in components.Keys)
            {
                components[k].Update();
            }
        //    base.Update();
        }

        public  void Input()
        {
            foreach (string k in components.Keys)
            {
                components[k].Input();
            }
        //    base.Input();
        }


        public void Enable()
        {
            foreach (string k in components.Keys)
            {
                components[k].Enable();
            }
            //    base.Input();
        }

        public  void Render()
        {
            foreach (string k in components.Keys)
            {
                 components[k].Render();
            }
          //  base.Render();
        }

        public  void Disable()
        {
            foreach (string k in components.Keys)
            {
                components[k].Disable();
            }
         //   base.Disable();
        }


        public void Delete()
        {
            foreach (string k in components.Keys)
            {
                components[k].Delete();
            }
            //   base.Delete();
        }


        public void Clear()
        {
            foreach (string k in components.Keys)
            {
                components[k].Clear();
            }
            //   base.Delete();
        }
    }
}
