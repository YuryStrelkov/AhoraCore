using AhoraCore.Core.CES.ICES;
using OpenTK;
using System.Collections.Generic;
using AhoraCore.Core.CES.Components;
using AhoraCore.Core.Transformations;

namespace AhoraCore.Core.CES
{
    public class GameEntity : Node, IGameEntity
    {
        private Dictionary<ComponentsTypes, AComponent<IGameEntity>> components;

        public GameEntity() : base()
        {
            components = new Dictionary<ComponentsTypes, AComponent<IGameEntity>>();

            AddComponent(ComponentsTypes.ShaderComponent,new ShaderComponent("DefaultShader"));

            AddComponent(ComponentsTypes.TransformComponent, new TransformComponent());

            AddComponent(ComponentsTypes.MaterialComponent, new MaterialComponent("DefaultMaterial"));

            AddComponent(ComponentsTypes.GeometryComponent, new GeometryComponent("DefaultModel"));
        }

        public void AddComponent(ComponentsTypes Key, AComponent<IGameEntity> component)
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


        public void RemoveComponent(ComponentsTypes Key)
        {
            components.Remove(Key);
        }

        public void Update()
        {
            foreach (ComponentsTypes k in components.Keys)
            {
                components[k].Update();
            }
            //    base.Update();
        }

        public void Input()
        {
            foreach (ComponentsTypes k in components.Keys)
            {
                components[k].Input();
            }
            //    base.Input();
        }

        public void Enable()
        {
            foreach (ComponentsTypes k in components.Keys)
            {
                components[k].Enable();
            }
            //    base.Input();
        }

        public void Render()
        {
            foreach (ComponentsTypes k in components.Keys)
            {
                components[k].Render();
            }
            //  base.Render();
        }

        public void Disable()
        {
            foreach (ComponentsTypes k in components.Keys)
            {
                components[k].Disable();
            }
            //   base.Disable();
        }

        public void Delete()
        {
            foreach (ComponentsTypes k in components.Keys)
            {
                components[k].Delete();
            }
            //   base.Delete();
        }

        public void Clear()
        {
            foreach (ComponentsTypes k in components.Keys)
            {
                components[k].Clear();
            }
            //   base.Delete();
        }

         

        public AComponent<IGameEntity> GetComponent(ComponentsTypes Key)
        {
            return components[Key];
        }

        public T GetComponent<T>(ComponentsTypes Key) where T : AComponent<IGameEntity>
        {
           return (T)components[Key];
        }

        public TransformComponent GetLocalTransform()
        {
           return GetComponent<TransformComponent>(ComponentsTypes.TransformComponent);
        }

        public TransformComponent GetWorldTransform()
        {
            return GetComponent<TransformComponent>(ComponentsTypes.TransformComponent);
        }

        public void SetLocalTranslation(float x, float y, float z)
        {
            GetLocalTransform().SetLocalTranslation(x, y, z);
        }

        public void SetWorldTranslation(float x, float y, float z)
        {
            GetWorldTransform().SetWorldTranslation(x, y, z);
        }

        public void SetLocalTranslation(Vector3 translation)
        {
            GetLocalTransform().SetLocalTranslation(translation);
        }

        public void SetWorldTranslation(Vector3 translation)
        {
            GetWorldTransform().SetWorldTranslation(translation);
        }
        public void SetLocalScale(float x, float y, float z)
        {
            GetLocalTransform().SetLocalScale(x, y, z);
        }

        public void SetWorldScale(float x, float y, float z)
        {
            GetWorldTransform().SetWorldScale(x, y, z);
        }

        public void SetLocalScale(Vector3 scale)
        {
            GetLocalTransform().SetLocalScale(scale);
        }

        public void SetWorldScale(Vector3 scale)
        {
            GetWorldTransform().SetWorldScale(scale);
        }

        public void SetLocalRotation(float x, float y, float z)
        {
            GetLocalTransform().SetLocalRotation(x, y, z);
        }

        public void SetWorldRotation(float x, float y, float z)
        {
            GetWorldTransform().SetWorldRotation(x, y, z);
        }

        public void SetLocalRotation(Vector3 rotation)
        {
            GetLocalTransform().SetLocalRotation(rotation);
        }

        public void SetWorldRotation(Vector3 rotation)
        {
            GetWorldTransform().SetWorldRotation(rotation);

        }

        public Matrix4 GetLocalTransMat()
        {
            return GetLocalTransform().GetLocalTransMat();
        }

        public Matrix4 GetWorldTransMat()
        {
            return GetWorldTransform().GetWorldTransMat();

        }

        public Vector3 GetLocalPos()
        {
            return GetLocalTransform().GetLocalPos();

        }

        public Vector3 GetWorldPos()
        {
            return GetWorldTransform().GetWorldPos();
        }

        public Vector3 GetWorldScl()
        {
            return GetWorldTransform().GetWorldScl();
        }

        public Vector3 GetLocalScl()
        {
            return GetLocalTransform().GetLocalScl();
        }

        public Vector3 GetWorldRot()
        {
            return GetWorldTransform().GetWorldRot();
        }

        public Vector3 GetLocalRot()
        {
            return GetLocalTransform().GetLocalRot();
        }

    }
}