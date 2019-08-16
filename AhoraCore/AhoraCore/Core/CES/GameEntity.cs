using AhoraCore.Core.CES.ICES;
using OpenTK;
using System.Collections.Generic;
using AhoraCore.Core.Shaders;

namespace AhoraCore.Core.CES
{
    public class GameEntity : Node, IGameEntity, IUniformBufferedObject
    {
        private Dictionary<string, AComponent<IGameEntity>> components;

        protected UniformBufferedObject uniformBuffer;

        public string BufferName
        {
            get
            {
                return uniformBuffer.BufferName;
            }
        }

        public bool IsBuffered
        {
            get
            {
                return uniformBuffer.IsBuffered;
            }
        }

        public GameEntity() : base()
        {
            components = new Dictionary<string, AComponent<IGameEntity>>();
            uniformBuffer = new UniformBufferedObject();
        }
        public GameEntity(string  prefix) : base()
        {
            components = new Dictionary<string, AComponent<IGameEntity>>();
            uniformBuffer = new UniformBufferedObject();
        }
        public GameEntity(Vector3 wordPos, Vector3 wordOrient) : base()
        {
            WorldTransform.SetRotation(wordOrient);

            WorldTransform.SetTranslation(wordPos);

            components = new Dictionary<string, AComponent<IGameEntity>>();

            uniformBuffer = new UniformBufferedObject();

        }

        public void AddComponent(string Key, AComponent<IGameEntity> component)
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
            foreach (string k in components.Keys)
            {
                components[k].Update();
            }
            //    base.Update();
        }

        public void Input()
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

        public void Render()
        {
            foreach (string k in components.Keys)
            {
                components[k].Render();
            }
            //  base.Render();
        }

        public void Disable()
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

        public void UpdateUniforms(AShader shader)
        {
            transformUniformBuffer.Bind(shader);
            if (IsBuffered)
            {
              uniformBuffer.Bind(shader);
            }
        }

        public void ConfirmBuffer()
        {
            uniformBuffer.ConfirmBuffer();
        }

        public void ConfirmBuffer(int capacity)
        {
            uniformBuffer.ConfirmBuffer(capacity);
        }

        public void EnableBuffering(string bufferName)
        {
            uniformBuffer.EnableBuffering(bufferName);
        }

        public void MarkBuffer(string[] itemNames, int[] itemLengths)
        {
            uniformBuffer.MarkBuffer(itemNames, itemLengths);
        }

        public void MarkBufferItem(string itemName, int itemLength)
        {
            uniformBuffer.MarkBufferItem(itemName, itemLength);
        }

        public void SetBindigLocation(UniformBindingsLocations location)
        {
            uniformBuffer.SetBindigLocation(location);
        }

        public void UpdateBufferIteam(string ItemName, float[] data)
        {
            uniformBuffer.UpdateBufferIteam(ItemName, data);
        }
    }
}