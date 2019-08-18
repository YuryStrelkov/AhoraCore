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

        public Transform GetLocalTransform()
        {
           return GetComponent<TransformComponent>(ComponentsTypes.TransformComponent).LocalTransform;
        }

        public Transform GetWorldTransform()
        {
            return GetComponent<TransformComponent>(ComponentsTypes.TransformComponent).WorldTransform;
        }

        public void SetLocalTranslation(float x, float y, float z)
        {
            GetLocalTransform().SetTranslation(x, y, z);
        }

        public void SetWorldTranslation(float x, float y, float z)
        {
            GetWorldTransform().SetTranslation(x, y, z);
        }

        public void SetLocalTranslation(Vector3 translation)
        {
            GetLocalTransform().SetTranslation(translation);
        }

        public void SetWorldTranslation(Vector3 translation)
        {
            GetWorldTransform().SetTranslation(translation);
        }
        public void SetLocalScale(float x, float y, float z)
        {
            GetLocalTransform().SetScaling(x, y, z);
        }

        public void SetWorldScale(float x, float y, float z)
        {
            GetWorldTransform().SetScaling(x, y, z);
        }

        public void SetLocalScale(Vector3 scale)
        {
            GetLocalTransform().SetScaling(scale);
        }

        public void SetWorldScale(Vector3 scale)
        {
            GetWorldTransform().SetScaling(scale);
        }

        public void SetLocalRotation(float x, float y, float z)
        {
            GetLocalTransform().SetRotation(x, y, z);
        }

        public void SetWorldRotation(float x, float y, float z)
        {
            GetWorldTransform().SetRotation(x, y, z);
        }

        public void SetLocalRotation(Vector3 rotation)
        {
            GetLocalTransform().SetRotation(rotation);
        }

        public void SetWorldRotation(Vector3 rotation)
        {
            GetWorldTransform().SetRotation(rotation);

        }

        public Matrix4 GetLocalTransMat()
        {
            return GetLocalTransform().GetTransformMat();
        }

        public Matrix4 GetWorldTransMat()
        {
            return GetWorldTransform().GetTransformMat();

        }

        public Vector3 GetLocalPos()
        {
            return GetLocalTransform().Position;

        }

        public Vector3 GetWorldPos()
        {
            return GetWorldTransform().Position;
        }

        public Vector3 GetWorldScl()
        {
            return GetWorldTransform().Scale;
        }

        public Vector3 GetLocalScl()
        {
            return GetLocalTransform().Scale;
        }

        public Vector3 GetWorldRot()
        {
            return GetWorldTransform().Rotation;
        }

        public Vector3 GetLocalRot()
        {
            return GetLocalTransform().Rotation;
        }

    }
}